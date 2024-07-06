using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInteractionSystem : MonoBehaviour
{
    public PlayerMovement player;

    private void Start()
    {
        player = GetComponent<PlayerMovement>();
    }

    // void OnTriggerEnter2D(Collider2D collision)
    // {
    //     IAction action = collision.GetComponent<IAction>();
    //     
    //     if(action != null)
    //     {
    //         action.Execute();
    //     }
    // }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, player.groundCheckDistance,
            player.whatIsGround);

        if (hit.collider == null) 
            return;
        var hitObject = hit.collider.gameObject;
        
         if (!hitObject.gameObject.CompareTag("Interaction")) 
            return;
        IAction action = hit.collider.GetComponent<PatrollingNPC>();

        if (action == null) 
            return;
        var distance = Vector3.Distance(transform.position, hit.point);

        Debug.Log("Hit object: " + hitObject.name);
        action.Execute();
    }
}