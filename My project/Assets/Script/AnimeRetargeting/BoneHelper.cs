using System.Collections.Generic;
using UnityEngine;

public class BoneHelper : MonoBehaviour
{
    public Animator avatar;
    
    public List<Transform> allBone;
    
    public List<Transform> boneList;

    public List<Transform> importantBones;

    public List<Transform> PeBone;
    
    public List<Transform> ragdollTransforms;

    public List<string> ragdollBoneName;
    
  
    [ContextMenu("Get All Bone")]
    public void GetAllBone()
    {
        allBone.Clear();
        for (int i = 0; i < 55; i++)
        {
            allBone.Add(avatar.GetBoneTransform((HumanBodyBones) i));
        }
    }
    [ContextMenu("Get Bone")]
    public void GetBone()
    {
        boneList.Clear();
        

        for (int i = 0; i < 55; i++)
        {
            if (avatar.GetBoneTransform((HumanBodyBones) i)!=null)
            {
                //PlayerBoneList.Add(avatar.GetBoneTransform((HumanBodyBones) i).name,avatar.GetBoneTransform((HumanBodyBones) i));
                boneList.Add(avatar.GetBoneTransform((HumanBodyBones) i));
            }
        }
    }
    [ContextMenu("Get Important Bones")]
    public void GetImportantBones()
    {
        importantBones.Clear();
        
        importantBones.Add(avatar.GetBoneTransform(HumanBodyBones.Hips));
        importantBones.Add(avatar.GetBoneTransform(HumanBodyBones.LeftUpperLeg));
        importantBones.Add(avatar.GetBoneTransform(HumanBodyBones.LeftLowerLeg));
        importantBones.Add(avatar.GetBoneTransform(HumanBodyBones.LeftFoot));
        importantBones.Add(avatar.GetBoneTransform(HumanBodyBones.RightUpperLeg));
        importantBones.Add(avatar.GetBoneTransform(HumanBodyBones.RightLowerLeg));  
        importantBones.Add(avatar.GetBoneTransform(HumanBodyBones.RightFoot));
        importantBones.Add(avatar.GetBoneTransform(HumanBodyBones.LeftUpperArm));
        importantBones.Add(avatar.GetBoneTransform(HumanBodyBones.LeftLowerArm));
        importantBones.Add(avatar.GetBoneTransform(HumanBodyBones.RightUpperArm));
        importantBones.Add(avatar.GetBoneTransform(HumanBodyBones.RightLowerArm));
        importantBones.Add(avatar.GetBoneTransform(HumanBodyBones.Spine));
        importantBones.Add(avatar.GetBoneTransform(HumanBodyBones.Head));
    }
    
    [ContextMenu("Get Pe Bones")]
    public void GetPeBone()
    {
        PeBone.Clear();
        for (int i = 0; i < PeHasBones.Length; i++)
        {
            PeBone.Add(avatar.GetBoneTransform(PeHasBones[i]));
        }
    }
    
    [ContextMenu("GetRagdollBone")]
    public void GetRagdollBone()
    {
        ragdollTransforms.Clear();
        for (int i = 0; i < bonesToUse.Length; i++)
        {
            ragdollTransforms.Add(avatar.GetBoneTransform(bonesToUse[i]));
        }
    }
    [ContextMenu("GetRagdollBoneName")]
    public void GetRagdollBoneName()
    {
        ragdollBoneName.Clear();
        for (int i = 0; i < bonesToUse.Length; i++)
        {
            ragdollBoneName.Add(avatar.GetBoneTransform(bonesToUse[i]).name);
        }
    }
    
    [ContextMenu("Add Script")]
    public void AddS()
    {
        // for (int i = 0; i < importantBones.Count; i++)
        // {
        //     importantBones[i].gameObject.AddComponent<PECollision>();
        // }
    }

    
    public static HumanBodyBones[] bonesToUse = new[]
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

    public static HumanBodyBones[] PeHasBones = new[]
    {
        HumanBodyBones.Hips,
        HumanBodyBones.LeftUpperLeg,
        HumanBodyBones.RightUpperLeg,
        HumanBodyBones.LeftLowerLeg,
        HumanBodyBones.RightLowerLeg,
        HumanBodyBones.LeftFoot,
        HumanBodyBones.RightFoot,
        HumanBodyBones.Spine,
        HumanBodyBones.Chest,
        HumanBodyBones.Neck,
        HumanBodyBones.Head,
        HumanBodyBones.LeftShoulder,
        HumanBodyBones.RightShoulder,
        HumanBodyBones.LeftUpperArm,
        HumanBodyBones.RightUpperArm,
        HumanBodyBones.LeftLowerArm,
        HumanBodyBones.RightLowerArm,
        HumanBodyBones.LeftHand,
        HumanBodyBones.RightHand,
        HumanBodyBones.LeftToes,
        HumanBodyBones.RightToes,
        HumanBodyBones.Jaw,
        HumanBodyBones.LeftThumbProximal,
        HumanBodyBones.LeftThumbIntermediate,
        HumanBodyBones.LeftThumbDistal,
        HumanBodyBones.LeftIndexProximal,
        HumanBodyBones.LeftIndexIntermediate,
        HumanBodyBones.LeftIndexDistal,
        HumanBodyBones.LeftRingProximal,
        HumanBodyBones.LeftRingIntermediate,
        HumanBodyBones.LeftRingDistal,
        HumanBodyBones.RightThumbProximal,
        HumanBodyBones.RightThumbIntermediate,
        HumanBodyBones.RightThumbDistal,
        HumanBodyBones.RightIndexProximal,
        HumanBodyBones.RightIndexIntermediate,
        HumanBodyBones.RightIndexDistal,
        HumanBodyBones.RightRingProximal,
        HumanBodyBones.RightRingIntermediate,
        HumanBodyBones.RightRingDistal,
    };

}