using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public bool canControl=true;
    
    private Animator anim;
    private Rigidbody2D rb;

    //移动速度
    [Header("Move info")]
    [SerializeField]private float speed = 5;
    [SerializeField]private float jumpForce=12;

    private bool canMove = true;
    
    private bool canDubbleJump;
    private bool canWallSlide=true;
    private bool isWallSliding;

    private bool facingRight=true;
    private float movingInput;
    private int facingDirection=1;
    [SerializeField] private Vector2 wallJumpDirection;

    //检测地面
    [Header("Collision info")]
   
    public float groundCheckDistance;
    public float wallCheckDistance;
    public LayerMask whatIsGround;

    private bool isGround;
    private bool isWallDetected;
    
    private static readonly int YVelocity = Animator.StringToHash("yVelocity");
    private static readonly int IsGrounded = Animator.StringToHash("isGrounded");
    private static readonly int IsMoving = Animator.StringToHash("isMoving");
    private static readonly int IsWallSliding = Animator.StringToHash("isWallSliding");
    private static readonly int IsWallDetected = Animator.StringToHash("isWallDetected");

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!canControl)
            return;
        
        CollisionCheck();
        FlipController();
        AnimatorController();
        CheckInput();

        if (isGround)
        {
            canMove = true;
            canDubbleJump = true;
        }
       
        if (canWallSlide)
        {
            isWallSliding = true;
            var velocity = rb.velocity;
            velocity = new Vector2(velocity.x, velocity.y * 0.1f);
            rb.velocity = velocity;
        }
        
        Move();
    }

   
    
    private void Move()
    {
        if(canMove)
            rb.velocity = new Vector2(movingInput * speed, rb.velocity.y);
    }


    private void CheckInput()
    {
        movingInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetAxis("Vertical")<0) 
            canWallSlide = false;
        
        //跳跃函数&二段跳
        if (Input.GetKeyDown(KeyCode.Space))
            JumpButton();
        //横向速度
    }
    private void JumpButton()
    {
        if(isWallSliding )
        {
            WallJump();
        }
        else if (isGround)
        {
            Jump();
        }
        else if (canDubbleJump)
        {
            canMove = true;
            canDubbleJump = false;
            Jump();
        }
        
        canWallSlide = false;
    }


    private void Jump()
    {
            canMove = true;

        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void WallJump()
    {
        Debug.Log("触墙跳跃");
        canDubbleJump = true;
        canMove = false;

        rb.velocity= new Vector2(wallJumpDirection.x * -facingDirection, wallJumpDirection.y);
        Debug.Log("velocity="+ rb.velocity);
    }
    private void FlipController()
    {
        if (isGround&& isWallDetected)
        {
            if (facingRight && movingInput < 0)
                Flip();
            else if (!facingRight && movingInput > 0)
                Flip();
            
        }
        //反转函数
        if (rb.velocity.x > 0 && !facingRight)
            Flip();
        else if (rb.velocity.x < 0 && facingRight)
            Flip();

    }
    private void Flip()
    {
        facingDirection = facingDirection * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void AnimatorController() 
    {
        //将动画参数与代码构筑联系
        var velocity = rb.velocity;
        bool isMoving = velocity.x != 0;
        anim.SetFloat(YVelocity, velocity.y);
        anim.SetBool(IsGrounded, isGround);
        anim.SetBool(IsMoving, isMoving);
        anim.SetBool(IsWallSliding,isWallSliding);
        anim.SetBool(IsWallDetected, isWallDetected);
    }
    
    //碰撞系统
    private void CollisionCheck()
    {
        var position = transform.position;
        isGround = Physics2D.Raycast(position,Vector2.down, groundCheckDistance, whatIsGround);
        isWallDetected = Physics2D.Raycast(position,Vector2.right*facingDirection,wallCheckDistance,whatIsGround);
        if (isWallDetected&& rb.velocity.y < 0)
            canWallSlide = true;

        if (!isWallDetected)
        {
            canWallSlide = false;
            isWallSliding = false;
        }
    }

    private void OnDrawGizmos()
    {
        var position = transform.position;
        Gizmos.DrawLine(position, new Vector3(position.x + wallCheckDistance*facingDirection,position.y));
        Gizmos.DrawLine(position, new Vector3(position.x , position.y-groundCheckDistance));
    }
}





