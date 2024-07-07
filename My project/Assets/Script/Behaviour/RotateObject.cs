using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 100f; // 旋转速度

    void Update()
    {
        // 持续旋转物体
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}