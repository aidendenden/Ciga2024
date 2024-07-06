using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : BaseAction
{
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            transform.position = collision.transform.GetChild(0).position;
            transform.SetParent(collision.transform);
            Execute();
        }
    }
    public override void Execute()
    {
        GameManager.Instance.getKey = true;
    }
}
