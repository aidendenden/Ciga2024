using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
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
   
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;

    private bool isGround;
    private bool isWallDetected;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
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
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.1f);
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
        bool isMoving = rb.velocity.x != 0;
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isGrounded", isGround);
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isWallSliding",isWallSliding);
        anim.SetBool("isWallDetected", isWallDetected);
    }
    
    //碰撞系统
    private void CollisionCheck()
    {

        isGround = Physics2D.Raycast(transform.position,Vector2.down, groundCheckDistance, whatIsGround);
        isWallDetected = Physics2D.Raycast(transform.position,Vector2.right*facingDirection,wallCheckDistance,whatIsGround);
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
       
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + wallCheckDistance*facingDirection,transform.position.y));
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x , transform.position.y-groundCheckDistance));
    }
}





