using System;
using System.Collections;
using UnityEngine;

public class MoveToNextPoint : MonoBehaviour
{
    public Vector3 targetPosition; // 目标位置
    public float moveSpeed = 2f; // 移动速度

    private int isMoving = 0; // 是否正在移动
    
    public AudioSource audio;
    public AudioClip A;

    public Transform moveOBJ;
    
    public Sprite activeSprite; // 活动时的精灵图
    private SpriteRenderer spriteRenderer; // 精灵渲染器

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 如果正在移动，计算新的位置
        if (isMoving==1)
        {
            float step = moveSpeed * Time.deltaTime; // 计算移动步长
            moveOBJ.position = Vector3.MoveTowards(moveOBJ.position, targetPosition, step);

            // 检查物体是否到达目标位置
            if (Vector3.Distance(moveOBJ.position, targetPosition) < 0.001f)
            {
                isMoving++; // 停止移动
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            audio.clip = A;
            audio.Play();
            spriteRenderer.sprite = activeSprite; // 更换精灵图
            if (isMoving==0)
            {
                isMoving++;
            }
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(moveOBJ.position, 0.1f);
        Gizmos.DrawSphere(targetPosition, 0.1f);
        Gizmos.DrawLine(moveOBJ.position, targetPosition);
    }
}