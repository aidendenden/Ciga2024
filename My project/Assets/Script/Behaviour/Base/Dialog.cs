using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : BaseAction
{
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Execute();
        }
    }
    
    public override void Execute()
    {
        
    }
}
