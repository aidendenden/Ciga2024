using System;
using System.Collections.Generic;
using UnityEngine;

public class AvatarLittleHelper : MonoBehaviour
{
    public GameObject ragdoll;
    public GameObject model;
    public GameObject ik;
    public Animator animator;
    public Avatar myavatar;
    //public SkAliveEntity skillCmpt;
  
    // public PERagdollController ragdollCtrl;
    // public UIRevive uiRevive;
    
    public List<Transform> srcJoints = new List<Transform>();
    public List<Transform> selfJoints = new List<Transform>();
    public Quaternion srcInitRotation = new Quaternion();
    public Quaternion selfInitRotation = new Quaternion();
    public List<Quaternion> srcJointsInitRotation = new List<Quaternion>();
    public List<Quaternion> selfJointsInitRotation = new List<Quaternion>();
    public Transform srcRoot;
    public Transform selfRoot;
    public Vector3 srcInitPosition = new Vector3();
    public Vector3 selfInitPosition = new Vector3();
    
    
    private static HumanBodyBones[] bonesToUse = new[]
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

    private static string[] RagdollBonesNames = new string[]
    {
       "Bip01 Neck",
       "Bip01 Head",
       "Bip01 Pelvis",
       "Bip01 Spine2",
       "Bip01 Spine3",
       "Bip01 L Clavicle",
       "Bip01 L UpperArm",
       "Bip01 L Forearm",
       "Bip01 L Hand",
       "Bip01 R Clavicle",
       "Bip01 R UpperArm",
       "Bip01 R Forearm",
       "Bip01 R Hand",
       "Bip01 L Thigh",
       "Bip01 L Calf",
       "Bip01 L Foot",
       "Bip01 L Toe0",
       "Bip01 R Thigh",
       "Bip01 R Calf",
       "Bip01 R Foot",
       "Bip01 R Toe0",
    };


    // Start is called before the first frame update
    void Start()
    {
       // ragdoll = transform.parent.GetComponent<BiologyViewRoot>()?.ragdollController.transform.gameObject;
       // model = transform.parent.GetComponent<BiologyViewRoot>()?.modelController.transform.gameObject;
       // ik = transform.parent.GetComponent<BiologyViewRoot>()?.ikAimCtrl.transform.gameObject;
        animator =  model.transform.gameObject.GetComponent<Animator>();
        myavatar = animator.avatar;
       // skillCmpt= transform.parent.parent.GetComponent<SkAliveEntity>();
        
        // ragdollCtrl = ragdoll.GetComponent<PERagdollController>();
        // uiRevive = FindObjectOfType<UIRevive>(true);
        
        InitBones();
        SetJointsInitRotation();
        SetInitPosition();
        
        InvokeRepeating("HideBones",0.5f,0.5f);

        // skillCmpt.deathEvent += OnDeath;
        // ragdollCtrl.startReviveEvent += OnRevive;
        // uiRevive.OnRestart += OnRevive;
    }

    // void OnDeath(SkEntity entity1, SkEntity entity2)
    // {
    //     animator.avatar = null;
    //     ik.SetActive(false);
    // }
    //
    // void OnRevive()
    // {
    //     animator.avatar = myavatar;
    //     ik.SetActive(true);
    // }
    
    
    // Update is called once per frame
    void Update()
    {
      //  if (skillCmpt.isDead)
           // ||skillCmpt.GetAttribute(AttribType.Hp) <= 0
        // {
        //     animator.avatar = null;
        //     ik.SetActive(false);
        //     SetJointsRotation();
        //     SetPosition();
        // }
        // else
        // {
        //     animator.avatar = myavatar;
        //     ik.SetActive(true);
        // }
    }
    
    private void InitBones()
    {
        srcJoints.Clear();
        selfJoints.Clear();
        
        for (int i = 0; i < bonesToUse.Length; i++)
        {
            selfJoints.Add(animator.GetBoneTransform(bonesToUse[i]));
            foreach (Transform ModelChild in ragdoll.transform.GetChild(0).GetComponentsInChildren<Transform>(true))
            {
                if (String.Equals(ModelChild.name,RagdollBonesNames[i]))
                {
                    srcJoints.Add(ModelChild);
                }
            }

        }
    }
    
    private void SetJointsInitRotation()
    {
        srcInitRotation = ragdoll.transform.rotation;
        selfInitRotation = model.transform.rotation;
        for (int i = 0; i < bonesToUse.Length; i++)
        {
            if (selfJoints[i]==null)
            {
                srcJointsInitRotation.Add(Quaternion.Euler(0,0,0));
                selfJointsInitRotation.Add(Quaternion.Euler(0,0,0));
                continue;
            }
            srcJointsInitRotation.Add(srcJoints[i].rotation * Quaternion.Inverse(srcInitRotation));
            selfJointsInitRotation.Add(selfJoints[i].rotation * Quaternion.Inverse(selfInitRotation));
        }
    }
    
    private void SetInitPosition()
    {
        srcRoot = srcJoints[2];
        selfRoot = selfJoints[2];
        srcInitPosition = srcRoot.position;
        selfInitPosition = selfRoot.position;
    }
    
    
    private void SetJointsRotation()
    {
        for (int i = 0; i < bonesToUse.Length; i++)
        {
            if (selfJoints[i]==null)
            {
                continue;
            }
            selfJoints[i].rotation = selfInitRotation;
            selfJoints[i].rotation *= (srcJoints[i].rotation * Quaternion.Inverse(srcJointsInitRotation[i]));
            selfJoints[i].rotation *= selfJointsInitRotation[i];
        }
    }
    
    private void SetPosition()
    {
        selfRoot.position = (srcRoot.position - srcInitPosition) + selfInitPosition;
    }

    public void HideBones()
    {
        if (ragdoll!=null && ragdoll.GetComponent<SkinnedMeshRenderer>()!=null)
        {
            ragdoll.GetComponent<SkinnedMeshRenderer>().renderingLayerMask=0;
            CancelInvoke("HideBones");
        }
    }
}