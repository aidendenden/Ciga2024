using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Vector2 pointA;
    public Vector2 pointB;
    public float pauseDuration = 1f; // 停顿时间

    private Vector2 target;
    private bool isPaused = false;

    void Start()
    {
        target = pointB;
        StartCoroutine(MovePlatform());
    }

    IEnumerator MovePlatform()
    {
        while (true)
        {
            if (!isPaused)
            {
                // 移动平台
                transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

                // 检查平台是否到达目标点
                if (Vector2.Distance(transform.position, target) < 0.1f)
                {
                    // 切换目标点
                    target = target == pointA ? pointB : pointA;
                    isPaused = true;
                    yield return new WaitForSeconds(pauseDuration);
                    isPaused = false;
                }
            }

            yield return null;
        }
    }

    // 在编辑器中可视化端点
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(pointA, 0.1f);
        Gizmos.DrawSphere(pointB, 0.1f);
        Gizmos.DrawLine(pointA, pointB);
    }
}