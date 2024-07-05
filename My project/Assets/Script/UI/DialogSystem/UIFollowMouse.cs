using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UIFollowMouse : MonoBehaviour
{  public RectTransform imageTransform;
    public float moveSpeed = 3;
    public Vector2 minPosition=new Vector2(-138,-42);
    public Vector2 maxPosition=new Vector2(138,42);
    

    private void Update()
    {
        // 获取鼠标当前位置
        Vector3 mousePosition = Input.mousePosition;

        // 将屏幕坐标转换为Canvas内的局部坐标
        RectTransformUtility.ScreenPointToLocalPointInRectangle(imageTransform.parent as RectTransform, mousePosition, null, out Vector2 localMousePosition);

        // 限制移动范围
        float clampedX = Mathf.Clamp(localMousePosition.x, minPosition.x, maxPosition.x);
        float clampedY = Mathf.Clamp(localMousePosition.y, minPosition.y, maxPosition.y);
        Vector2 clampedPosition = new Vector2(clampedX, clampedY);

        // 设置图片的局部坐标
        imageTransform.DOLocalMove(-clampedPosition,moveSpeed);
    }
    
}
