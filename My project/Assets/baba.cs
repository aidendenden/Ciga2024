using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baba : MonoBehaviour
{
    public Rigidbody2D babaRb ;
    float babaWalkDirection;
    public float speed = 5;
    public Animator anim;
 

    bool isMoving;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animset(); 
        babaWalkDirection = Input.GetAxisRaw("Horizontal");
        Debug.Log(babaWalkDirection);
        if (babaWalkDirection != 0)
        {
            Move();
        }
        else
        {
            isMoving = false;
        }
    }
    void Move()
    {
        isMoving = true;
        babaRb.velocity = new Vector2( speed * babaWalkDirection, babaRb.velocity.y);
        Debug.Log(babaRb.velocity);
    }
    void animset()
    {
        anim.SetBool("isMoving", isMoving);
    }
}
