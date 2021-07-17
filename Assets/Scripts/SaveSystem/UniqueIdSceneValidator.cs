using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// Find all uniqueID's on the scene and validate their ID's - Check if there are no duplicates and if the ID's are not empty and have correct format with correct scene name in front.
/// </summary>
[ExecuteInEditMode]
public class UniqueIdSceneValidator : MonoBehaviour
{
#region Inspector Variables

    [Title("Settings")]
    [SerializeField] private bool AutomaticValidation;
    
    [Title("All unique ID's")]
    [ShowInInspector] public List<UniqueId> allUniqueIdsOnScene;

#endregion Inspector Variables


#region Unity Methods
    
    #if UNITY_EDITOR

    private void LateUpdate()
    {
        if (AutomaticValidation)
        {
            if (Application.isPlaying)
                return;
        
            ValidateAllUniqueIDs();  
        }
    }

#endif

    private void OnDestroy()
    {
        UniqueId.allGuids.Clear();
    }

#endregion UnityMethods


#region Public Variables

#endregion Public Variables


#region Public Methods

    [Button(ButtonSizes.Large)]
    public void CollectAllUniqueIDs()
    {
        allUniqueIdsOnScene = new List<UniqueId>();

        var allResourceObjects = Resources.FindObjectsOfTypeAll<MonoBehaviour>();

        foreach (var resourceObject in allResourceObjects)
        {
            if (resourceObject.gameObject.scene.rootCount > 0 && resourceObject is UniqueId uniqueId)
            {
                allUniqueIdsOnScene.Add(uniqueId);   
            }
        }
    }

#if UNITY_EDITOR
    [Button(ButtonSizes.Large)]
    public void ValidateAllUniqueIDs()
    {
        CollectAllUniqueIDs();
        
        foreach (var uniqueId in allUniqueIdsOnScene)
        {
            uniqueId.ValidateAndGenerateUniqueID();
        }
    }
#endif

    public void FillOutAllGuidsList()
    {
        CollectAllUniqueIDs();
        
        foreach (var uniqueId in allUniqueIdsOnScene)
        {
            uniqueId.AddSelfToAllGuidList();
        }
    }

    [Button(ButtonSizes.Large)]
    public UniqueId FindUniqueIdOnScene(string uniqueID)
    {
        CollectAllUniqueIDs();

        foreach (var uid in allUniqueIdsOnScene)
        {
            if (uid.uniqueId == uniqueID)
            {
                return uid;
            }
        }

        return null;
    }

    [Button(ButtonSizes.Large)]
    public void FindUniqueIdInGUIDList(string uniqueID)
    {
        bool uidFound = false;
        foreach (var uid in UniqueId.allGuids)
        {
            if (uid.Key == uniqueID)
            {
                Debug.Log(uniqueID + " belongs to " + uid.Value.gameObject, uid.Value.gameObject);
                uidFound = true;
            }
        }
        
        if(!uidFound)
            Debug.Log(uniqueID + " NOT found in the global GUID list");
    }
    
#endregion Public Methods


#region Private Variables

    //Private variables, accessible only from this class.


#endregion Private Variables


#region Private Methods

    //Private methods, accessible only from this class.


#endregion Private Methods


#region Coroutines

    //IEnumerators<>


#endregion Coroutines


#region Public Types

    //enums, structs etc...


#endregion Public Types
}
