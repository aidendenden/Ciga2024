using UnityEngine;

public class PingPongRotate : MonoBehaviour
{
    public float rotationAngle = 45f; // 最大旋转角度
    public float rotationSpeed = 30f; // 旋转速度

    private float startAngle; // 初始角度

    void Start()
    {
        // 记录初始角度
        startAngle = transform.eulerAngles.z;
    }

    void Update()
    {
        // 计算新的旋转角度
        float angle = startAngle + Mathf.PingPong(Time.time * rotationSpeed, rotationAngle * 2) - rotationAngle;
        // 设置旋转角度
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angle);
    }
}