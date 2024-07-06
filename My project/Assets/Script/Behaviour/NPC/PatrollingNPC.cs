using System.Collections;
using UnityEngine;

public class PatrollingNPC : BaseAction
{
    // Components
    private Rigidbody2D rb;
    private Transform npcTransform;
    private Collider2D npcCollider;

    // Patrol settings
    public float moveSpeed = 2f;
    public float patrolDistance = 5f;
    public float pauseDuration = 1f; // Pause duration at patrol points

    // Ground detection settings
    public LayerMask groundLayer;
    public float groundDetectionDistance = 0.2f;

    // Internal variables
    private Vector2 startPosition;
    private bool movingRight = true;
    private bool isPaused;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        npcTransform = GetComponent<Transform>();
        npcCollider = GetComponent<Collider2D>();
        startPosition = npcTransform.position;
    }

    void Update()
    {
        if (!isPaused)
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (IsGrounded())
        {
            if (movingRight)
            {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

                if (npcTransform.position.x >= startPosition.x + patrolDistance)
                {
                    StartCoroutine(PauseAndFlip());
                }
            }
            else
            {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);

                if (npcTransform.position.x <= startPosition.x - patrolDistance)
                {
                    StartCoroutine(PauseAndFlip());
                }
            }
        }
        else
        {
            // Change direction if no ground detected
            StartCoroutine(PauseAndFlip());
        }
    }

    IEnumerator PauseAndFlip()
    {
        isPaused = true;
        rb.velocity = Vector2.zero; // Stop movement during pause

        yield return new WaitForSeconds(pauseDuration);

        movingRight = !movingRight;
        Flip();

        isPaused = false;
    }

    void Flip()
    {
        Vector3 scale = npcTransform.localScale;
        scale.x *= -1;
        npcTransform.localScale = scale;
    }

    bool IsGrounded()
    {
        Vector2 position = npcTransform.position;
        Vector2 direction = movingRight ? Vector2.right : Vector2.left;
        Vector2 groundCheckPos = new Vector2(position.x + direction.x * npcCollider.bounds.extents.x, position.y);

        // Draw the ray in the scene view for debugging purposes
        Debug.DrawRay(groundCheckPos, Vector2.down * groundDetectionDistance, Color.red);

        RaycastHit2D hit = Physics2D.Raycast(groundCheckPos, Vector2.down, groundDetectionDistance, groundLayer);
        return hit.collider != null;
    }

    void OnDrawGizmos()
    {
        if (npcTransform != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(new Vector2(startPosition.x - patrolDistance, npcTransform.position.y), 
                            new Vector2(startPosition.x + patrolDistance, npcTransform.position.y));
        }
    }

    public override void Execute()
    {
        
    }
}
