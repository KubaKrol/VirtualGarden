using System;
using System.Collections.Generic;
using System.Diagnostics;
using Sirenix.OdinInspector;
using UnityEngine;
using Debug = UnityEngine.Debug;

/// <summary>
/// SaveManager gathers all ISnapshot's from the scene and pass these to the SaveSystem in order to be processed for saving and loading.
/// </summary>
[RequireComponent(typeof(UniqueIdSceneValidator))]
public class SaveManager : MonoBehaviour
{
#region Inspector Variables

    [BoxGroup("Save system", centerLabel: true)]
    [SerializeField] private bool loadOnAwake;
    [BoxGroup("Save system", centerLabel: true)]
    [SerializeField] public string snapshotContainer;
    [BoxGroup("Save system", centerLabel: true)]
    [SerializeField] private ESaveProfile saveProfile;
    
#endregion Inspector Variables


#region Unity Methods

    private void Awake()
    {
        GetComponent<UniqueIdSceneValidator>().FillOutAllGuidsList();
        CollectAllSnapshots();
        SaveSystem.AssignSceneSnapshots(allSnapshots);
        SaveSystem.SetSnapshotContainer(snapshotContainer);
        SaveSystem.SetSaveProfile(saveProfile);
        
        if(loadOnAwake)
            Load();
    }
    
#endregion UnityMethods


#region Public Variables
    
    
#endregion Public Variables


#region Public Methods

    [BoxGroup("Save system", centerLabel: true)]
    [Button(ButtonSizes.Large)]
    public void Save(Transform playerCheckpointTransform)
    {
        if (Application.isPlaying)
        {
            GetComponent<UniqueIdSceneValidator>().FillOutAllGuidsList();
            CollectAllSnapshots();
            SaveSystem.AssignSceneSnapshots(allSnapshots);
            SaveSystem.SaveGame(playerCheckpointTransform);
        }
    }

    [BoxGroup("Save system", centerLabel: true)]
    [Button(ButtonSizes.Large)]
    public void Load()
    {
        if (Application.isPlaying)
        {
            SaveSystem.LoadGame();
        }
    }

    [BoxGroup("Save system", centerLabel: true)]
    [Button(ButtonSizes.Large)]
    public void ClearContainer(string container)
    {
        if(Application.isPlaying)
            SaveSystem.ClearSnapshotContainer(container);
        else
            Debug.Log("In order to clear container, the application must be playing");
    }
    
#endregion Public Methods


#region Private Variables

    [BoxGroup("Snapshots", centerLabel: true)]
    [Unity.Collections.ReadOnly] [ShowInInspector] private List<ISnapshot> allSnapshots;
    
#endregion Private Variables


#region Private Methods
    
    [BoxGroup("Snapshots", centerLabel: true)]
    [Button(ButtonSizes.Large)]
    private void CollectAllSnapshots()
    {
        allSnapshots = new List<ISnapshot>();

        var savedEntities = Resources.FindObjectsOfTypeAll<MonoBehaviour>();

        foreach (var se in savedEntities)
        {
            if (se.gameObject.scene.rootCount > 0 && se is ISnapshot snapshot)
            {
                allSnapshots.Add(snapshot); 
            }
        }
    }
    
#if UNITY_EDITOR
    [BoxGroup("Snapshots", centerLabel: true)]
    [Button(ButtonSizes.Large)]
    private void GenerateUniqueIDs()
    {
        var generatedIDs = 0;
        
        for (int i = 0; i < allSnapshots.Count; i++)
        {
            if (allSnapshots[i].MyUniqueID == null)
            {
                if (allSnapshots[i] is MonoBehaviour mb)
                {
                    var uniqueID = mb.gameObject.AddComponent<UniqueId>();
                    uniqueID.ValidateAndGenerateUniqueID();
                    generatedIDs++;
                }
            }
            else
            {
                allSnapshots[i].MyUniqueID.ValidateAndGenerateUniqueID();
            }
        }
        
        Debug.Log("Unique IDs added: " + generatedIDs);
    }
    
    [BoxGroup("Save system", centerLabel: true)]
    [Button(ButtonSizes.Large)]
    private void OpenSaveFileFolder()
    {
        Process.Start(@Application.persistentDataPath);
    }
#endif

#endregion Private Methods


#region Coroutines

    //IEnumerators<>


#endregion Coroutines


#region Public Types

    //enums, structs etc...


#endregion Public Types
}
