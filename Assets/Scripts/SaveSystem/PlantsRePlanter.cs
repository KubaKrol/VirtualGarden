﻿using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantsRePlanter : MonoBehaviour
{
    #region Inspector Variables

    [Title("Dependencies")]

    [SerializeField] private UniqueIdSceneValidator uniqueIdSceneValidator;
    [SerializeField] private PlantsDatabase plantsDatabase;

    #endregion Inspector Variables


    #region Unity Methods

    public void Start()
    {
        if(SaveSystem.SaveFileExists())
            Replant();
    }

    #endregion UnityMethods


    #region Public Variables
    //Variables accessible from every other script referencing this class.
    //These variables should not be visible in Inspector and should be hidden by using [HideInInspector] or [ReadOnly]

    #endregion Public Variables


    #region Public Methods    

    [Button]
    public void Replant()
    {
        var allSavedUniqueIDs = SaveSystem.GetAllSavedUniqueIDs();

        for(int i = 0; i < allSavedUniqueIDs.Count; i++)
        {
            if(uniqueIdSceneValidator.FindUniqueIdOnScene(allSavedUniqueIDs[i]) == null)
            {
                var plantSnapshotDatas = SaveSystem.GetSnapshotsByUniqueID(allSavedUniqueIDs[i]);

                foreach (var snapshotData in plantSnapshotDatas)
                {
                    if(snapshotData is Plant.PlantSnapshotData plantSnapshotData)
                    {
                        var plantGameObject = Instantiate(plantsDatabase.GetPlantData(plantSnapshotData.plantType).plantPrefab, plantSnapshotData.position, Quaternion.identity);
                        var plantedPlant = plantGameObject.GetComponent<Plant>();
                        plantedPlant.LoadMe(plantSnapshotData);
                    }
                }
            }
        }
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
