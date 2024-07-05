using System.Collections.Generic;
using UnityEngine;

public class SetAvatarBone : MonoBehaviour
{
   public GameObject NewAvatar;

   private Animator AvatarAnimator;

   public List<Transform> ImportantTransforms;

   public GameObject Model;

   // [ContextMenu("Set Trigger")]
   // public void SetTriggrs()
   // {
   //    for (int i = 0; i < ImportantTransforms.Count; i++)
   //    {
   //       ImportantTransforms[i].GetComponent<Collider>().isTrigger = true;
   //    }
   // }  
   //
   // [ContextMenu("Set Collider Sale")]
   // public void SetColliderSale()
   // {
   //    for (int i = 0; i < ImportantTransforms.Count; i++)
   //    {
   //       ImportantTransforms[i].GetComponent<CapsuleCollider>().isTrigger = true;
   //       ImportantTransforms[i].GetComponent<CapsuleCollider>().radius = 0.001f;
   //       ImportantTransforms[i].GetComponent<CapsuleCollider>().height = 0;
   //    }
   // }  
   [ContextMenu("Set Ragdoll")]
   public void SetRT()
   {
    //  transform.GetComponent<BoneCollector>().BoneBone();
   }
   
   [ContextMenu("Set Trigger")]
   public void SetTrigger()
   {
      AvatarAnimator=NewAvatar.GetComponent<Animator>();
      
      ImportantTransforms.Clear();

      ImportantTransforms.Add(AvatarAnimator.GetBoneTransform(HumanBodyBones.LeftLowerLeg));
            
      ImportantTransforms.Add(AvatarAnimator.GetBoneTransform(HumanBodyBones.RightLowerLeg));
      
      ImportantTransforms.Add(AvatarAnimator.GetBoneTransform(HumanBodyBones.LeftLowerArm));
      
      ImportantTransforms.Add(AvatarAnimator.GetBoneTransform(HumanBodyBones.RightLowerArm));

      for (int i = 0; i < ImportantTransforms.Count; i++)
      {
         if (!ImportantTransforms[i].TryGetComponent<CapsuleCollider>(out CapsuleCollider  capsuleCollider))
         {
            ImportantTransforms[i].gameObject.AddComponent<CapsuleCollider>();
         } 
         ImportantTransforms[i].GetComponent<Collider>().isTrigger = true;
      }

      if (Model!=null)
      {
         // Model.GetComponent<PEModelController>().colliders[1]=AvatarAnimator.GetBoneTransform(HumanBodyBones.LeftLowerLeg).GetComponent<Collider>();
         // Model.GetComponent<PEModelController>().colliders[2]=AvatarAnimator.GetBoneTransform(HumanBodyBones.RightLowerLeg).GetComponent<Collider>();
         // Model.GetComponent<PEModelController>().colliders[3]=AvatarAnimator.GetBoneTransform(HumanBodyBones.LeftLowerArm).GetComponent<Collider>();
         // Model.GetComponent<PEModelController>().colliders[4]=AvatarAnimator.GetBoneTransform(HumanBodyBones.RightLowerArm).GetComponent<Collider>();
         //
         for (int i = 0; i < ImportantTransforms.Count; i++)
         {
            // ImportantTransforms[i].gameObject.AddComponent<PEAttackTrigger>().attackParts=new PEAttackTrigger.PEAttackPart[1];
            // ImportantTransforms[i].GetComponent<PEAttackTrigger>().attackParts[0].capsule.trans = ImportantTransforms[i];
            // ImportantTransforms[i].GetComponent<PEAttackTrigger>().attackParts[0].capsule.radius = 0.1f;
         }
      }
   }
   [ContextMenu("Set Collider Size")]
   public void SetColliderSize()
   {
      if (ImportantTransforms!=null)
      {
         for (int i = 0; i < ImportantTransforms.Count; i++)
         {
            if (!ImportantTransforms[i].TryGetComponent<CapsuleCollider>(out CapsuleCollider  capsuleCollider))
            {
               ImportantTransforms[i].gameObject.AddComponent<CapsuleCollider>();
            } 
            ImportantTransforms[i].GetComponent<Collider>().isTrigger = true;
            if (i==0)
            {
               ImportantTransforms[i].GetComponent<CapsuleCollider>().center = new Vector3(0, 0.25f, 0);
               ImportantTransforms[i].GetComponent<CapsuleCollider>().height = 0.12f;
               ImportantTransforms[i].GetComponent<CapsuleCollider>().radius = 0.7f;
            }
            if (i==1)
            {
               ImportantTransforms[i].GetComponent<CapsuleCollider>().center = new Vector3(0, -0.25f, 0);
               ImportantTransforms[i].GetComponent<CapsuleCollider>().height = 0.12f;
               ImportantTransforms[i].GetComponent<CapsuleCollider>().radius = 0.7f;
            }
            if (i==2)
            {
               ImportantTransforms[i].GetComponent<CapsuleCollider>().center = new Vector3(-0.35f, 0, 0);
               ImportantTransforms[i].GetComponent<CapsuleCollider>().height = 0.13f;
               ImportantTransforms[i].GetComponent<CapsuleCollider>().radius = 0.75f;
            }
            if (i==3)
            {
               ImportantTransforms[i].GetComponent<CapsuleCollider>().center = new Vector3(0.35f, 0, 0);
               ImportantTransforms[i].GetComponent<CapsuleCollider>().height = 0.13f;
               ImportantTransforms[i].GetComponent<CapsuleCollider>().radius = 0.75f;
            }
         
         }
      }
      else
      {
         AvatarAnimator=NewAvatar.GetComponent<Animator>();

         ImportantTransforms.Add(AvatarAnimator.GetBoneTransform(HumanBodyBones.LeftLowerLeg));
            
         ImportantTransforms.Add(AvatarAnimator.GetBoneTransform(HumanBodyBones.RightLowerLeg));
      
         ImportantTransforms.Add(AvatarAnimator.GetBoneTransform(HumanBodyBones.LeftLowerArm));
      
         ImportantTransforms.Add(AvatarAnimator.GetBoneTransform(HumanBodyBones.RightLowerArm));
         
         for (int i = 0; i < ImportantTransforms.Count; i++)
         {
            if (!ImportantTransforms[i].TryGetComponent<CapsuleCollider>(out CapsuleCollider  capsuleCollider))
            {
               ImportantTransforms[i].gameObject.AddComponent<CapsuleCollider>();
            } 
            ImportantTransforms[i].GetComponent<Collider>().isTrigger = true;
            if (i==0)
            {
               ImportantTransforms[i].GetComponent<CapsuleCollider>().center = new Vector3(0, 0.25f, 0);
               ImportantTransforms[i].GetComponent<CapsuleCollider>().height = 0.12f;
               ImportantTransforms[i].GetComponent<CapsuleCollider>().radius = 0.7f;
            }
            if (i==1)
            {
               ImportantTransforms[i].GetComponent<CapsuleCollider>().center = new Vector3(0, -0.25f, 0);
               ImportantTransforms[i].GetComponent<CapsuleCollider>().height = 0.12f;
               ImportantTransforms[i].GetComponent<CapsuleCollider>().radius = 0.7f;
            }
            if (i==2)
            {
               ImportantTransforms[i].GetComponent<CapsuleCollider>().center = new Vector3(-0.35f, 0, 0);
               ImportantTransforms[i].GetComponent<CapsuleCollider>().height = 0.13f;
               ImportantTransforms[i].GetComponent<CapsuleCollider>().radius = 0.75f;
            }
            if (i==3)
            {
               ImportantTransforms[i].GetComponent<CapsuleCollider>().center = new Vector3(0.35f, 0, 0);
               ImportantTransforms[i].GetComponent<CapsuleCollider>().height = 0.13f;
               ImportantTransforms[i].GetComponent<CapsuleCollider>().radius = 0.75f;
            }
         }
      }
   }
   
   
   [ContextMenu("Set New Avatar")]
   public void SetNewAvatar()
   
   {
      AvatarAnimator=NewAvatar.GetComponent<Animator>();
      
     // transform.GetComponent<BoneCollector>().SetBoneGroups(AvatarAnimator);
     
      foreach (Transform child in transform)
      {
         switch (child.name)
         {
            case "Param":
            //   child.GetComponent<BeatParam>().m_ApplyWentflyBone =
              //    AvatarAnimator.GetBoneTransform(HumanBodyBones.Spine);
               break;
            case "Model":
               
               child.GetComponent<Animator>().avatar=AvatarAnimator.avatar;
               child.GetComponent<SkinnedMeshRenderer>().renderingLayerMask = 0;
              // child.GetComponent<ArmorBones>().SetNodesTransform(AvatarAnimator);
               
             //  child.GetComponent<FootprintDecalMan>()._lrFoot[0]=AvatarAnimator.GetBoneTransform(HumanBodyBones.LeftToes)!=null?AvatarAnimator.GetBoneTransform(HumanBodyBones.LeftToes):AvatarAnimator.GetBoneTransform(HumanBodyBones.LeftFoot);
             //  child.GetComponent<FootprintDecalMan>()._lrFoot[1]=AvatarAnimator.GetBoneTransform(HumanBodyBones.RightToes)!=null?AvatarAnimator.GetBoneTransform(HumanBodyBones.RightToes):AvatarAnimator.GetBoneTransform(HumanBodyBones.RightFoot);
               
               // child.GetComponent<PEModelController>().colliders[1]=AvatarAnimator.GetBoneTransform(HumanBodyBones.LeftLowerLeg).GetComponent<Collider>();
               // child.GetComponent<PEModelController>().colliders[2]=AvatarAnimator.GetBoneTransform(HumanBodyBones.RightLowerLeg).GetComponent<Collider>();
               // child.GetComponent<PEModelController>().colliders[3]=AvatarAnimator.GetBoneTransform(HumanBodyBones.LeftLowerArm).GetComponent<Collider>();
               // child.GetComponent<PEModelController>().colliders[4]=AvatarAnimator.GetBoneTransform(HumanBodyBones.RightLowerArm).GetComponent<Collider>();
               
               
               foreach(Transform ModelChild in child.GetChild(0).GetComponentsInChildren<Transform>(true))
               {
                  switch (ModelChild.name)
                  {
                     case "Bone  Bow":
                        ModelChild.parent=AvatarAnimator.GetBoneTransform(HumanBodyBones.Hips);
                        break;
                     case "Bone  Long_Gun":
                        ModelChild.parent=AvatarAnimator.GetBoneTransform(HumanBodyBones.Hips);
                        break; 
                     case "Doublesword1":
                        ModelChild.parent=AvatarAnimator.GetBoneTransform(HumanBodyBones.Hips);
                        break;
                     case "Doublesword2":
                        ModelChild.parent=AvatarAnimator.GetBoneTransform(HumanBodyBones.Hips);
                        break;
                     case "Pistol":
                        ModelChild.parent=AvatarAnimator.GetBoneTransform(HumanBodyBones.Hips);
                        break;
                     case "Pistol  2":
                        ModelChild.parent=AvatarAnimator.GetBoneTransform(HumanBodyBones.Hips);
                        break;
                     // case "Bip01 L Calf":
                     //    DestroyImmediate( ModelChild.GetComponent<CapsuleCollider>()); 
                     //    DestroyImmediate( ModelChild.GetComponent<PEAttackTrigger>());
                     //    break;
                     // case "Bip01 R Calf" :
                     //    DestroyImmediate( ModelChild.GetComponent<CapsuleCollider>()); 
                     //    DestroyImmediate( ModelChild.GetComponent<PEAttackTrigger>());
                     //    break; 
                     // case "Bip01 L Forearm" :
                     //    DestroyImmediate( ModelChild.GetComponent<CapsuleCollider>()); 
                     //    DestroyImmediate( ModelChild.GetComponent<PEAttackTrigger>());
                     //    break;
                     // case "Bip01 R Forearm" :
                     //    DestroyImmediate( ModelChild.GetComponent<CapsuleCollider>()); 
                     //    DestroyImmediate( ModelChild.GetComponent<PEAttackTrigger>());
                     //    break;
                     case "mountWaist" :
                        ModelChild.parent=AvatarAnimator.GetBoneTransform(HumanBodyBones.Spine).parent.transform;
                        break;
                     case "Bow" :
                        ModelChild.parent=AvatarAnimator.GetBoneTransform(HumanBodyBones.Chest)!=null?AvatarAnimator.GetBoneTransform(HumanBodyBones.Chest):AvatarAnimator.GetBoneTransform(HumanBodyBones.Hips);
                        break;
                     case "Bow_box" :
                        ModelChild.parent=AvatarAnimator.GetBoneTransform(HumanBodyBones.Chest)!=null?AvatarAnimator.GetBoneTransform(HumanBodyBones.Chest):AvatarAnimator.GetBoneTransform(HumanBodyBones.Hips);
                        break;
                     case "Long_Gun" :
                        ModelChild.parent=AvatarAnimator.GetBoneTransform(HumanBodyBones.Chest)!=null?AvatarAnimator.GetBoneTransform(HumanBodyBones.Chest):AvatarAnimator.GetBoneTransform(HumanBodyBones.Hips);
                        break;
                     case "mountBack" :
                        ModelChild.parent=AvatarAnimator.GetBoneTransform(HumanBodyBones.Chest)!=null?AvatarAnimator.GetBoneTransform(HumanBodyBones.Chest):AvatarAnimator.GetBoneTransform(HumanBodyBones.Hips);
                        break;
                     case "Bone L Forearm" :
                        ModelChild.parent=AvatarAnimator.GetBoneTransform(HumanBodyBones.LeftLowerArm);
                        //ModelChild.GetComponent<FixBoneForearmError>().m_HandTrans=AvatarAnimator.GetBoneTransform(HumanBodyBones.LeftHand);
                        break;
                     case "Bone R Forearm" :
                        ModelChild.parent=AvatarAnimator.GetBoneTransform(HumanBodyBones.RightLowerArm);
                       // ModelChild.GetComponent<FixBoneForearmError>().m_HandTrans=AvatarAnimator.GetBoneTransform(HumanBodyBones.RightHand);
                        break;
                     case "mountShied" :
                        ModelChild.parent=AvatarAnimator.GetBoneTransform(HumanBodyBones.LeftLowerArm);
                        break;
                     case "mountOff" :
                        ModelChild.parent=AvatarAnimator.GetBoneTransform(HumanBodyBones.LeftHand);
                        break;
                     case "mountMain" :
                        ModelChild.parent=AvatarAnimator.GetBoneTransform(HumanBodyBones.RightHand);
                        break;
                  }
               }
               
               for (int i = NewAvatar.transform.childCount - 1; i >= 0; --i)
               {
                  NewAvatar.transform.GetChild(i).parent =child;
               }
               
              // DestroyImmediate(child.GetComponent<SkinnedMeshRendererHelper>());
               child.gameObject.AddComponent<AvatarLittleHelper>();
                
               foreach(Transform mChild in child.GetComponentsInChildren<Transform>(true))
               {
                  mChild.gameObject.layer = 10;
               }
               break;
            
         }
      }
   }
}