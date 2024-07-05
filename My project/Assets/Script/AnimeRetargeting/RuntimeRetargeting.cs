using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeRetargeting : MonoBehaviour
{
    public Transform srcModel;
    Animator srcAnimator;
    Animator selfAnimator;

    List<Transform> srcJoints = new List<Transform>();
    List<Transform> selfJoints = new List<Transform>();
    Quaternion srcInitRotation = new Quaternion();
    Quaternion selfInitRotation = new Quaternion();
    List<Quaternion> srcJointsInitRotation = new List<Quaternion>();
    List<Quaternion> selfJointsInitRotation = new List<Quaternion>();

    Transform srcRoot;
    Transform selfRoot;
    Vector3 srcInitPosition = new Vector3();
    Vector3 selfInitPosition = new Vector3();

    static HumanBodyBones[] bonesToUse = new[]
    {
        HumanBodyBones.Neck,
        HumanBodyBones.Head,

        HumanBodyBones.Hips,
        HumanBodyBones.Spine,
        HumanBodyBones.Chest,
        //HumanBodyBones.UpperChest,
        HumanBodyBones.LeftShoulder,
        HumanBodyBones.LeftUpperArm,
        HumanBodyBones.LeftLowerArm,
        HumanBodyBones.LeftHand,

        HumanBodyBones.RightShoulder,
        HumanBodyBones.RightUpperArm,
        HumanBodyBones.RightLowerArm,
        HumanBodyBones.RightHand,

        HumanBodyBones.LeftUpperLeg,
        HumanBodyBones.LeftLowerLeg,
        HumanBodyBones.LeftFoot,
        HumanBodyBones.LeftToes,

        HumanBodyBones.RightUpperLeg,
        HumanBodyBones.RightLowerLeg,
        HumanBodyBones.RightFoot,
        HumanBodyBones.RightToes,
    };

    void Start()
    {
        srcAnimator = srcModel.GetComponent<Animator>();
        selfAnimator = gameObject.GetComponent<Animator>();

        InitBones();
        InitJointsRotation();
        InitSetPosition();
    }

    void LateUpdate()
    {
        SetJointsRotation();
        SetPosition();
    }

    private void InitBones()
    {
        for (int i = 0; i < bonesToUse.Length; i++)
        {
            srcJoints.Add(srcAnimator.GetBoneTransform(bonesToUse[i]));
            selfJoints.Add(selfAnimator.GetBoneTransform(bonesToUse[i]));
        }
    }

    private void InitJointsRotation()
    {
        if (srcModel != null)
        {
            srcInitRotation = srcModel.rotation;
            selfInitRotation = transform.rotation;
            for (int i = 0; i < bonesToUse.Length; i++)
            {
                if (srcJoints[i] != null && selfJoints[i] != null)
                {
                    srcJointsInitRotation.Add(srcJoints[i].rotation * Quaternion.Inverse(srcInitRotation));
                    selfJointsInitRotation.Add(selfJoints[i].rotation * Quaternion.Inverse(selfInitRotation));
                }
                else
                {
                    Debug.LogWarning($"Joint transform is null for bone: " + bonesToUse[i]+"1="+(srcJoints[i] != null)+"2="+(selfJoints[i] != null));
                }
            }
        }
        else
        {
            Debug.LogWarning("Source model or self model is null in SetJointsInitRotation.");
        }
    }

    private void SetJointsRotation()
    {   
        Debug.Log("selfJoints count: " + selfJoints.Count); // 添加调试信息
        Debug.Log("selfJointsInitRotation count: " + selfJointsInitRotation.Count); // 添加调试信息
        Debug.Log("bonesToUse count: " + bonesToUse.Length); // 添加调试信息

        
        for (int i = 0; i < bonesToUse.Length; i++)
        {
            if (i >= selfJoints.Count || i >= selfJointsInitRotation.Count) // 添加条件判断
            {
                Debug.LogWarning("Index out of range in selfJoints or selfJointsInitRotation list: " + i);
                continue;
            }
            
            if (selfJoints[i] != null)
            {
                selfJoints[i].rotation = selfInitRotation;
                selfJoints[i].rotation *= (srcJoints[i].rotation * Quaternion.Inverse(srcJointsInitRotation[i]));
                selfJoints[i].rotation *= selfJointsInitRotation[i];
            }
            else
            {
                Debug.LogWarning("Joint transform or initial rotation is null for bone: " + bonesToUse[i]);
            }
        }
    }

    private void InitSetPosition()
    {
        srcRoot = srcAnimator.GetBoneTransform(HumanBodyBones.Hips);
        selfRoot = selfAnimator.GetBoneTransform(HumanBodyBones.Hips);
        srcInitPosition = srcRoot.localPosition;
        selfInitPosition = selfRoot.localPosition;
    }

    private void SetPosition()
    {
        selfRoot.localPosition = (srcRoot.localPosition - srcInitPosition) + selfInitPosition;
    }
}