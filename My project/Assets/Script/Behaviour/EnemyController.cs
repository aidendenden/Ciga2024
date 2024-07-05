using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : BaseAction
{
    //Components

    Animator animatorController;

    CapsuleCollider2D capsuleCollider;
    float colliderOffsetX; //Collider position

    Transform enemySprite;
    public Rigidbody2D rigid2D { get; set; }

    [Header("Variables")] public float moveSpeed; //move speed
    bool isMove;
    bool isGround;


    public float returnRadius = 2; //radius for start to return when player exit
    bool isReturning;
    Vector3 starterPos;

    public bool isPatrolling;
    public float patrollRadius;

    //Patrol variables
    float patrollSin;
    float patrollTimer = 0.02f;

    bool isFollow;
    [HideInInspector] public Transform followTarget;
    float returnTimer = 3;

    public float attackRadius;

    bool canMove;


    private void Start()
    {
        canMove = true;

        starterPos = transform.position;

        rigid2D = GetComponent<Rigidbody2D>();
        enemySprite = transform.GetChild(0);

        animatorController = GetComponentInChildren<Animator>();

        capsuleCollider = GetComponent<CapsuleCollider2D>();
        colliderOffsetX = capsuleCollider.offset.x;
    }


    public void FixedUpdate()
    {
        if (!GameManager.Instance.isPause) //Check pause and gameover
        {
            if (!canMove)
                return; //Block move

            Move();
        }
    }

    private void Update()
    {
        if (!GameManager.Instance.isPause)
        {
            if (!canMove)
                return;

            Rotation();
            Animation();
        }
    }

    public void Move()
    {
        CheckGround();

        isMove = true;

        if (isPatrolling && !isFollow && !isReturning)
        {
            PatrollSin();
            Patroll();
        }
    }

    //The patrol is made using a sinusoid
    void PatrollSin()
    {
        patrollSin += patrollTimer * moveSpeed;

        if (patrollSin >= 1)
        {
            patrollTimer = -0.02f;
        }
        else if (patrollSin <= -1)
        {
            patrollTimer = 0.02f;
        }
    }


    //Follow method
    public void Follow(Transform target)
    {
        followTarget = target; //set target to follow

        if (!isFollow)
        {
            isFollow = true;
            StartCoroutine(IFollow());
        }
    }

    void Patroll()
    {
        float sin = patrollSin; //local sin varible
        float x = patrollRadius * sin + starterPos.x; //set x of enemy position

        float y = transform.position.y;
        float z = transform.position.z;

        transform.position = new Vector3(x, y, z); //Change position
    }

    public void Rotation()
    {
        if (isPatrolling)
        {
            if (patrollTimer > 0) //Side of patroll
            {
                enemySprite.transform.localScale = new Vector3(1, 1, 1);
                capsuleCollider.offset = new Vector2(colliderOffsetX, capsuleCollider.offset.y);
            }
            else //reverse
            {
                enemySprite.transform.localScale = new Vector3(-1, 1, 1);
                capsuleCollider.offset = new Vector2(-colliderOffsetX, capsuleCollider.offset.y);
            }
        }

        if (isFollow) //get target and check the difference between them
        {
            if (transform.position.x > followTarget.position.x)
            {
                enemySprite.transform.localScale = new Vector3(-1, 1, 1);
                capsuleCollider.offset = new Vector2(-colliderOffsetX, capsuleCollider.offset.y);
            }
            else
            {
                enemySprite.transform.localScale = new Vector3(1, 1, 1);
                capsuleCollider.offset = new Vector2(colliderOffsetX, capsuleCollider.offset.y);
            }
        }

        if (isReturning) //side of stater position
        {
            if (transform.position.x > starterPos.x)
            {
                enemySprite.transform.localScale = new Vector3(-1, 1, 1);
                capsuleCollider.offset = new Vector2(-colliderOffsetX, capsuleCollider.offset.y);
            }
            else
            {
                enemySprite.transform.localScale = new Vector3(1, 1, 1);
                capsuleCollider.offset = new Vector2(colliderOffsetX, capsuleCollider.offset.y);
            }
        }
    }


    public void Animation()
    {
        if (isMove)
            animatorController.SetBool("Move", true);
        else
            animatorController.SetBool("Move", false);
    }

    //Death method
    public void Death()
    {
        canMove = false; //block move

        rigid2D.simulated = false; //disable physics

        animatorController.SetTrigger("Death"); //animator death
    }

    public void CheckGround()
    {
        float rayStartXoffset = 0.14f; //Offset ray to check ground
        float rayStartX;

        if (patrollTimer > 0) //get rotate
        {
            rayStartX = transform.position.x + rayStartXoffset;
        }
        else
        {
            rayStartX = transform.position.x - rayStartXoffset;
        }


        Vector3 rayStartPos =
            new Vector3(rayStartX, transform.position.y, transform.position.z); //Raycast start position


        RaycastHit2D raycastHit2D = Physics2D.Raycast(rayStartPos, Vector2.down, 0.18f); //Raycast

        if (raycastHit2D.collider != null) //If ray hit object
        {
            if (Vector2.Distance(rayStartPos, raycastHit2D.point) <=
                raycastHit2D.distance) //if distans between object and enemy < local distance variable 
            {
                Debug.DrawLine(rayStartPos, raycastHit2D.point);
                isGround = true; //set is ground
            }

            if (!raycastHit2D.collider.gameObject.CompareTag("Ground"))
            {
                isGround = false;
                patrollTimer = -patrollTimer;
            }
        }
        else
        {
            isGround = false;
            patrollTimer = -patrollTimer; //turn enemy in the opposite direction
        }
    }

    IEnumerator IFollow()
    {
        float timer = returnTimer; //timer to return after palyer leave

        while (isFollow)
        {
            if (Vector2.Distance(transform.position, followTarget.position) > returnRadius &&
                timer <= 0) //if target leave
            {
                isFollow = false;
                followTarget = null;

                isReturning = true;

                patrollSin = 0;

                StartCoroutine(IReturnToStartPos()); //start returning
            }
            else if (!isGround) //if the target jumps over the gap  
            {
                isFollow = false;
                followTarget = null;

                isReturning = true;

                patrollSin = 0;

                StartCoroutine(IReturnToStartPos());
            }
            else
            {
                if (Vector2.Distance(transform.position, followTarget.position) > attackRadius)
                    transform.position = Vector2.MoveTowards(transform.position,
                        new Vector2(followTarget.position.x, transform.position.y),
                        moveSpeed / 2 * Time.deltaTime); //follow target


                timer -= Time.deltaTime;
            }

            yield return null;
        }

        yield return null;
    }

    IEnumerator IReturnToStartPos()
    {
        while (transform.position.x != starterPos.x)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                new Vector2(starterPos.x, transform.position.y), moveSpeed / 2 * Time.deltaTime); //move to start pos

            yield return null;
        }

        isReturning = false;
        yield return null;
    }

    //Just editor method for drawing variables
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRadius);
        Gizmos.DrawWireSphere(transform.position, returnRadius);
    }

    public override void Execute()
    {
        Death();
    }
}