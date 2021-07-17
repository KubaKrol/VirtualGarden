 using UnityEngine;
 using System.Collections.Generic;
 using System;
 using Sirenix.OdinInspector;
#if UNITY_EDITOR
 using Sirenix.OdinInspector.Editor;
 using Sirenix.Serialization;
 using UnityEditor;
 using UnityEditor.SceneManagement;
#endif
 
 /// <summary>
 /// UniqueID is GUID based component providing a unique string considered as ID. There should be no two objects having the same ID.
 /// </summary>
 [ExecuteInEditMode]
 [DisallowMultipleComponent]
 public class UniqueId : MonoBehaviour 
 {
     public static Dictionary<string, UniqueId> allGuids = new Dictionary<string, UniqueId> ();

     public string uniqueId;
     
     private void Start()
     {
         ValidateAndGenerateUniqueID();
     }
     
     void OnDestroy()
     {
         allGuids.Remove(uniqueId);
     }

     public void AddSelfToAllGuidList()
     {
         if (uniqueId != null)
         {
             if (!allGuids.ContainsKey(uniqueId))
             {
                 allGuids.Add(uniqueId, this);
             }   
         }
     }
     
     [Title("ID Validation")]
     [Button]
     public void ValidateAndGenerateUniqueID()
     {
         string sceneName = gameObject.scene.name + "_";
 
         if  (sceneName == null) return;
 
         bool hasSceneNameAtBeginning = (uniqueId != null && 
                                         uniqueId.Length > sceneName.Length && 
                                         uniqueId.Substring (0, sceneName.Length) == sceneName);
         
         bool anotherComponentAlreadyHasThisID = (uniqueId != null && 
                                                  allGuids.ContainsKey (uniqueId) && 
                                                  allGuids [uniqueId] != this);
         
         if (!hasSceneNameAtBeginning || anotherComponentAlreadyHasThisID)
         {
             uniqueId =  sceneName + Guid.NewGuid ();

             if (anotherComponentAlreadyHasThisID)
             {
                 Debug.Log("Correcting " + gameObject.name + " unique ID, another game object had the same UniqueID", gameObject);
             }

             if (!hasSceneNameAtBeginning)
             {
                 Debug.Log("Correcting " + gameObject.name + " unique ID, didn't have scene name at the beginning", gameObject);
             }
             
#if UNITY_EDITOR
             
             if (!Application.isPlaying)
             {
                 PrefabUtility.RecordPrefabInstancePropertyModifications(gameObject);
                 EditorUtility.SetDirty (this);
                 EditorUtility.SetDirty(gameObject);
                 EditorSceneManager.MarkSceneDirty(gameObject.scene);
             }
#endif
         }
         AddSelfToAllGuidList();
     }
 }