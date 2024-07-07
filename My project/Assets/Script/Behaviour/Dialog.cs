using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    private int currentStep = 0;

    private bool canPlay;

    // 在Inspector中设置动画片段名称
    public Transform[] dialag;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canPlay)
        {
            PlayNextAnimation();
        }
    }

    void PlayNextAnimation()
    {
        if (currentStep < dialag.Length)
        {
            if (currentStep>0)
            {
                dialag[currentStep-1].gameObject.SetActive(false);
            }
            dialag[currentStep].gameObject.SetActive(true);
            currentStep++;
        }
        else
        {
            // 动画播放完成后重置
            //dialag[currentStep].gameObject.SetActive(false);
            currentStep = 0;
            enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayNextAnimation();
            collision.GetComponent<PlayerMovement>().canControl=false;
            collision.transform.rotation= Quaternion.Euler(0f, 0f, 0f);
            canPlay = true;
        }
    }
}