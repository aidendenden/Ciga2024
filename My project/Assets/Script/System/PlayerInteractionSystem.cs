using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractionSystem : MonoBehaviour
{
    public Image highlightUI; // 用于显示高亮 UI 的 Image
    public float highlightRange = 2f; // 高亮 UI 的最大距离

    void Start()
    {
        if (highlightUI != null)
        {
            highlightUI.enabled = false; // 初始时隐藏高亮 UI
        }
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f)); // 从屏幕中心创建一条射线
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;
            IAction action = hit.collider.GetComponent<IAction>();

            if (action != null)
            {
                float distance = Vector3.Distance(transform.position, hit.point);

                if (distance <= highlightRange)
                {
                    // 高亮 UI 处理
                    if (highlightUI != null)
                    {
                        highlightUI.enabled = true; // 显示高亮 UI
                    }

                    // 检测鼠标左键点击
                    if (Input.GetMouseButtonDown(0))
                    {
                        Debug.Log("Hit object: " + hitObject.name);
                        action.Execute();
                    }
                }
                else
                {
                    // 超出范围，隐藏高亮 UI
                    if (highlightUI != null)
                    {
                        highlightUI.enabled = false;
                    }
                }
            }
            else
            {
                // 如果检测到的物体没有 IAction 组件，隐藏高亮 UI
                if (highlightUI != null)
                {
                    highlightUI.enabled = false;
                }
            }
        }
        else
        {
            // 射线没有检测到任何物体时，隐藏高亮 UI
            if (highlightUI != null)
            {
                highlightUI.enabled = false;
            }
        }
    }
}