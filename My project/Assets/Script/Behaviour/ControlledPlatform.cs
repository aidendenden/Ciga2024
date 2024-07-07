using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlledPlatform : MonoBehaviour
{
    public Vector3 startPoint; // 平台的起始位置
    public Vector3 endPoint; // 平台的终点位置
    public float moveSpeed = 2f; // 平台移动速度
    public float returnSpeed = 2f; // 平台返回速度

    public Transform platform;

    private bool isMoving = false; // 是否正在移动
    private Vector3 targetPosition; // 平台当前目标位置

    public bool isTouch;
    
    public Sprite defaultSprite; // 默认精灵图
    public Sprite activeSprite; // 活动时的精灵图

    private SpriteRenderer spriteRenderer; // 精灵渲染器
    private AudioSource audio;
    public AudioClip A;
    public AudioClip B;
    
    
    void Start()
    {
        // 初始化平台位置和目标位置为起始点
        platform.position = startPoint;
        targetPosition = startPoint;
        audio = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = defaultSprite;
    }

    void Update()
    {
        // 进入 平台移动到终点位置
        if (isTouch)
        {
            isMoving = true;
            targetPosition = endPoint;
            spriteRenderer.sprite = activeSprite; // 更换精灵图
        }
        // 松开 平台返回起始位置
        else
        {
            isMoving = false;
            targetPosition = startPoint;
            spriteRenderer.sprite = defaultSprite; // 恢复默认精灵图
        }

        // 根据是否在移动决定移动速度
        float speed = isMoving ? moveSpeed : returnSpeed;

        // 移动平台
        platform.position = Vector3.MoveTowards(platform.position, targetPosition, speed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            audio.clip = A;
            audio.Play();
            isTouch = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            audio.clip = B;
            audio.Play();
            isTouch = false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(startPoint, 0.1f);
        Gizmos.DrawSphere(endPoint, 0.1f);
        Gizmos.DrawLine(startPoint, endPoint);
    }
}