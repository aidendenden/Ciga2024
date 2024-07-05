using UnityEngine;

public class FootIK : MonoBehaviour
{
    public Animator animator;
    public CapsuleCollider characterCollider;
    public bool isIK;

    [Header("IK Trans")] public Transform leftFootTrans;
    public Transform rightFootTrans;

    [Header("Target Trans")] public Transform leftFootTarget;
    public Transform rightFootTarget;

    [Header("hasHitPoint")] public bool hasLeftFootHitPoint;
    public bool hasRightFootHitPoint;

    [Header("Is OnStair")] public bool isLeftFootOnStair;
    public bool isRightFootOnStair;
    public bool hasFootOnStair;

    [Header("Offset")] public float footOffset = 0.12f; //Foot节点到地面偏移距离
    public float centerOnStair = 1.1f; //备用1.05
    public float centerOnGround = 1.01f;

    [Header("Weight")] public float leftFootWeight;

    public float rightFootWeight;

    // Start is called before the first frame update
    void Start()
    {
        characterCollider = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
        if (animator)
        {
            leftFootTrans = animator.GetBoneTransform(HumanBodyBones.LeftFoot);
            rightFootTrans = animator.GetBoneTransform(HumanBodyBones.RightFoot);
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (!isIK && animator)
        {
            return;
        }

        //==========================UpdateFootTarget==========================
        //LeftFoot
        hasLeftFootHitPoint = UpdateFootTarget(leftFootTrans, leftFootTarget, out isLeftFootOnStair);
        //RightFoot
        hasRightFootHitPoint = UpdateFootTarget(rightFootTrans, rightFootTarget, out isRightFootOnStair);

        //==========================SetIK==========================
        hasFootOnStair = isLeftFootOnStair || isRightFootOnStair;
        //Left
        leftFootWeight = GetIKWeight(hasLeftFootHitPoint, "leftFootWeight"); //获取权重
        SetIK(AvatarIKGoal.LeftFoot, leftFootTarget, leftFootWeight);

        //Right
        rightFootWeight = GetIKWeight(hasRightFootHitPoint, "rightFootWeight");
        SetIK(AvatarIKGoal.RightFoot, rightFootTarget, rightFootWeight);

        //================改变characterController的Center，防止浮空==================
        ChangeCenter();
    }

    /// <summary>
    /// 更新脚参考点的Transform
    /// </summary>
    /// <param name="oriFootTrans">当前Foot节点</param>
    /// <param name="oriToeTrans">当前Toe节点</param>
    /// <param name="footTargetTrans">foot目标节点</param>
    /// <param name="isOriFootOnStair"></param>
    public bool UpdateFootTarget(Transform oriFootTrans, Transform footTargetTrans, out bool isOriFootOnStair)
    {
        //==========================胶囊体射线==========================
        var up = oriFootTrans.up;
        var position1 = oriFootTrans.position;
        Vector3 oriFootPoint1 = position1 + up * 0.3f; //以Foot节点为参考往上0.3f
        Vector3 oriFootPoint2 = position1 + up * 0.3f + oriFootTrans.forward * 0.15f;

        //画蓝色射线,模拟胶囊体射线范围
        Debug.DrawLine(oriFootPoint1, oriFootPoint1 - up * 0.5f, Color.blue);
        Debug.DrawLine(oriFootPoint2, oriFootPoint2 - up * 0.5f, Color.blue);

        bool hasHitPoint = Physics.CapsuleCast(oriFootPoint1, oriFootPoint2, 0.01f, -up,
            out RaycastHit oriFootHitInfo, 0.5f);

        //==========================设置参考点==========================
        if (hasHitPoint) //如果有打中东西
        {
            //rotation
            Quaternion temQ = Quaternion.FromToRotation(oriFootTrans.up, oriFootHitInfo.normal);
            footTargetTrans.rotation = temQ * oriFootTrans.rotation;

            //position
            var tempPlane = new Plane(oriFootHitInfo.normal, oriFootHitInfo.point); //用射线打中的点和该点的法线求脚所踩平面
            var position = oriFootTrans.position;
            var tempPointOnPlane = tempPlane.ClosestPointOnPlane(position); //求tempPlane面上离Foot节点最近的点
            footTargetTrans.position = tempPointOnPlane + Vector3.up * footOffset;
            Debug.DrawLine(position, tempPointOnPlane, Color.green);

            Debug.DrawRay(oriFootHitInfo.point, oriFootHitInfo.normal, Color.red, 0.01f);
            isOriFootOnStair = true;
        }
        else
        {
            isOriFootOnStair = false;
        }

        return hasHitPoint;
    }

    /// <summary>
    /// 从animator中获取IK权重
    /// </summary> 
    public float GetIKWeight(bool hasOriFootHitPoint, string oriFootWeight)
    {
        float tempWeight;
        if (hasOriFootHitPoint)
        {
            tempWeight = animator.GetFloat(oriFootWeight);
        }
        else
        {
            tempWeight = 0;
        }

        return tempWeight;
    }


    /// <summary>
    /// 设置IK的Position,Rotation和Weight(权重)
    /// </summary>
    /// <param name="avatarIKGoal">avatarIKGoal节点</param>
    /// <param name="goalTargetTrans">目标节点</param>
    /// <param name="weight">权重</param>
    public void SetIK(AvatarIKGoal avatarIKGoal, Transform goalTargetTrans, float weight)
    {
        animator.SetIKPosition(avatarIKGoal, goalTargetTrans.position);
        animator.SetIKPositionWeight(avatarIKGoal, weight);
        animator.SetIKRotation(avatarIKGoal, goalTargetTrans.rotation);
        animator.SetIKRotationWeight(avatarIKGoal, weight);
    }

    /// <summary>
    /// 调节characterController的中心点
    /// </summary>
    /// <param name="hasFootOnStair"></param>
    public void ChangeCenter()
    {
        if (characterCollider)
        {
            Vector3 tempPos = characterCollider.center;
            if (hasFootOnStair) //如果有脚在楼梯上
            {
                characterCollider.center = new Vector3(tempPos.x, centerOnStair, tempPos.z); //根据实际楼梯调节
            }
            else
            {
                characterCollider.center = new Vector3(tempPos.x, centerOnGround, tempPos.z);
            }
        }
    }
}