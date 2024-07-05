using UnityEngine;

public class SpecialGrabbable : Grabbable
{
    public float minHeight = 5f; // 高度范围的下限
    public float maxHeight = 10f; // 高度范围的上限
    public float minScale = 0.1f; // 大小范围的下限
    public float maxScale = 1f; // 大小范围的上限
}