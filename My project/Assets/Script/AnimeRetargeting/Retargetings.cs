using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Retargetings : MonoBehaviour
{
    public GameObject animator;

    // public GameObject ragdoll;


    HumanPoseHandler m_srcPoseHandler;
    HumanPoseHandler m_destPoseHandler;
    //Renderer[] renderers;

    public Vector3 BodyPosition = new Vector3(0, 1, 0);

    void Start()
    {
        //renderers = src.GetComponentsInChildren<Renderer>();

        // if (ragdoll==null)
        // {
        //     ragdoll = transform.parent.GetComponent<BiologyViewRoot>()?.ragdollController.transform.gameObject;
        // }

        // InvokeRepeating("HideBone",0.5f,0.5f);

        SetTargetFalse();
    }


    private void SetTargetFalse()
    {
        if (animator != null)
        {
            Avatar avatar = GetComponent<Animator>().avatar;
            m_srcPoseHandler = new HumanPoseHandler(animator.GetComponent<Animator>().avatar, animator.transform);
            m_destPoseHandler = new HumanPoseHandler(avatar, transform);
        }
    }


    // public Vector3 BodyRotation = new Vector3(0, 0, 0);
    // Quaternion qt = new Quaternion();
    // private void OnEnable()
    // {
    //     if (ragdoll != null && ragdoll.GetComponent<SkinnedMeshRenderer>() != null)
    //     {
    //         ragdoll.GetComponent<SkinnedMeshRenderer>().renderingLayerMask = 0;
    //     }
    // }
    void Update()
    {
        HumanPose m_humanPose = new HumanPose();
        if (m_srcPoseHandler != null && m_destPoseHandler != null && animator != null)
        {
            m_srcPoseHandler.GetHumanPose(ref m_humanPose);
            m_humanPose.bodyPosition = BodyPosition;
            //this.transform.rotation = animator.transform.rotation;
            this.transform.position = animator.transform.position;
            m_destPoseHandler.SetHumanPose(ref m_humanPose);
        }
    }


    // public void HideBone()
    // {
    //     if (ragdoll!=null && ragdoll.GetComponent<SkinnedMeshRenderer>()!=null)
    //     {
    //         ragdoll.GetComponent<SkinnedMeshRenderer>().renderingLayerMask=0;
    //         CancelInvoke("HideBone");
    //     }
    // }
}