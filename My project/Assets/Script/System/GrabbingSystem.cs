using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbingSystem : MonoBehaviour
{
    public Transform grabPoint; // 抓取点
    public float grabRange = 2f; // 抓取物体的最大距离
    private GameObject grabbedObject = null;
    private Rigidbody grabbedRigidbody = null;
    
    private Vector3 originalScale; // 记录物体原始大小
    private SpecialGrabbable specialGrabbable; // 当前抓取的特殊物体

    private FixedJoint grabJoint; // 用于固定抓取物体的关节
    void Start()
    {
        if (grabPoint == null)
        {
            Debug.LogError("Grab point is not assigned.");
        }
        else
        {
            grabJoint = grabPoint.GetComponent<FixedJoint>();
            grabJoint.breakForce = float.PositiveInfinity;
            grabJoint.breakTorque = float.PositiveInfinity;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 鼠标左键按下时检测抓取
        {
            if (grabbedObject == null)
            {
                TryGrabObject();
            }
            else
            {
                ReleaseObject();
            }
        }
    }

    void TryGrabObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, grabRange))
        {
            Grabbable grabbable = hit.collider.GetComponent<Grabbable>();
            if (grabbable != null)
            {
                grabbedObject = hit.collider.gameObject;
                originalScale = grabbedObject.transform.localScale;
                grabbedRigidbody = grabbedObject.GetComponent<Rigidbody>();
                specialGrabbable = grabbedObject.GetComponent<SpecialGrabbable>(); // 检查是否是特殊的可抓取物体
                if (grabbedRigidbody != null)
                {
                    if (specialGrabbable != null)
                    {
                        grabbedRigidbody.isKinematic = true;
                    }
                    else
                    {
                        grabbedRigidbody.isKinematic = false; // 确保物体在抓取时处于物理模拟状态
                        // 创建并配置 FixedJoint
                        grabJoint.connectedBody = grabbedRigidbody;
                    }
                }
               
                Debug.Log("Object grabbed: " + grabbedObject.name);
            }
        }
    }

    void ReleaseObject()
    {
        if (grabbedObject != null)
        {
            if (grabbedRigidbody != null)
            {
                grabbedRigidbody.isKinematic = false;
                grabbedRigidbody = null;
            }
            
            if (specialGrabbable != null)
            {
                specialGrabbable.transform.localScale = originalScale; // 恢复原始大小
                Debug.Log("Object released: " + specialGrabbable.name);
                specialGrabbable = null; // 重置特殊物体引用
            }
            
            if (grabJoint.connectedBody != null)
            {
                grabJoint.connectedBody = null;
            }
            
            Debug.Log("Object released: " + grabbedObject.name);
            grabbedObject = null;
        }
    }

    void FixedUpdate()
    {
        if (specialGrabbable != null)
        {
            // 通过 FixedJoint 进行物理模拟
            grabbedObject.transform.position = grabPoint.position;
            grabbedObject.transform.rotation = grabPoint.rotation;
            // 调整物体大小
            AdjustObjectSize();
        }
    }
    
    void AdjustObjectSize()
    {
        if (specialGrabbable != null)
        {
            // 如果是特殊的可抓取物体，根据高度范围调整大小
            float minHeight = specialGrabbable.minHeight;
            float maxHeight = specialGrabbable.maxHeight;
            float minScale = specialGrabbable.minScale;
            float maxScale = specialGrabbable.maxScale;

            float currentHeight = grabbedObject.transform.position.y;
            
            if (currentHeight >= minHeight && currentHeight <= maxHeight)
            {
                float t = (currentHeight - minHeight) / (maxHeight - minHeight);
                float scaleFactor = Mathf.Lerp(maxScale, minScale, t);
                grabbedObject.transform.localScale = originalScale * scaleFactor;
            }
        }
    }
}