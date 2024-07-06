using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : BaseAction
{
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (GameManager.Instance.getKey)
            {
                Destroy(collision.transform.GetChild(1));
                Execute();
            }
        }
    }

    public override void Execute()
    {
        GameManager.Instance.getKey = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}