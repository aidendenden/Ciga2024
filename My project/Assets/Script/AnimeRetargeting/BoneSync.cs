using System;
using System.Collections.Generic;
using UnityEngine;

public class BoneSync : MonoBehaviour
{
    [Serializable]
    public class Pair
    {
        public Transform a;
        public Transform b;
    }
    
    //public SkinnedMeshRenderer[] masterRenderers;
    //public SkinnedMeshRenderer[] slaveRenderers;

    public Animator masterModel;
    public Animator slaveModel;
    
    public Vector3 offset;
    [Header("debug data, don`t modify")] public List<Pair> pairList;


    private void Update()
    {
        offset = -transform.position;
        
        Vector3 cachePosition = transform.position;
        Quaternion cacheRotation = transform.rotation;
        Vector3 cacheLocalScale = transform.localScale;
        
        transform.position = Vector3.zero - offset;
        transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.one;

        if (pairList!=null)
        {
            for (int i = 0, iMax = pairList.Count; i < iMax; i++)
            {
                Pair pair = pairList[i];
    
                pair.b.transform.position = pair.a.transform.position;
                pair.b.transform.rotation = pair.a.transform.rotation;
                pair.b.transform.localScale = pair.a.transform.localScale;
            }
        }
       
    
        transform.position = cachePosition;
        transform.rotation = cacheRotation;
        transform.localScale = cacheLocalScale;
    }

    // [ContextMenu("Manual UpdateBoneMapping")]
    // public void UpdateBoneMapping()
    // {
    //     List<Transform> slaveBonesList = new List<Transform>(slaveRenderers.Length * 10);
    //     for (int i = 0; i < slaveRenderers.Length; i++)
    //     {
    //         SkinnedMeshRenderer renderer = slaveRenderers[i];
    //         for (int j = 0, jMax = renderer.bones.Length; j < jMax; j++)
    //         {
    //             Transform bone = renderer.bones[j];
    //             slaveBonesList.Add(bone);
    //         }
    //     }
    //
    //     pairList = new List<Pair>(slaveBonesList.Count);
    //     for (int i = 0; i < masterRenderers.Length; i++)
    //     {
    //         SkinnedMeshRenderer renderer = masterRenderers[i];
    //         for (int j = 0, jMax = renderer.bones.Length; j < jMax; j++)
    //         {
    //             Transform bone = renderer.bones[j];
    //
    //             Transform slaveBone = slaveBonesList.Find(m => m.name == bone.name);
    //
    //             if (slaveBone)
    //                 pairList.Add(new Pair() {a = bone, b = slaveBone});
    //         }
    //     }
    // }
    
    
    [ContextMenu("Arrange Bone")]
    public void ArrangeBone()
    {
        if (pairList!=null)
        {
            pairList.Clear();
        }
        
        for (int i = 0; i < 55; i++)
        {
            Pair pair = new Pair
            {
                a = masterModel.GetBoneTransform((HumanBodyBones) i),
                b = slaveModel.GetBoneTransform((HumanBodyBones) i),
            };

            if (pair.a==null||pair.b==null)
            {
                continue;
            }
            else
            {
                pairList.Add(pair);

            }
        }
    }
    
    
    // [ContextMenu("Set Bone")]
    // public void SetBone()
    // {
    //     
    //     if ( pairList!=null)
    //     {
    //         for (int i = 0; i < pairList.Count; i++)
    //         {
    //             pairList[i].a = pairList[i].b.parent;
    //         }
    //         pairList.Clear();
    //     }
    //
    // }
    //
    
}