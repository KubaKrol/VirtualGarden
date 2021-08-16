using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Replanter : MonoBehaviour
{
    #region Inspector Variables

    [Title("Dependencies")]

    [SerializeField] private UniqueIdSceneValidator uniqueIdSceneValidator;
    [SerializeField] private PlantsDatabase plantsDatabase;
    [SerializeField] private BuildablesDatabase buildablesDatabase;

    #endregion Inspector Variables


    #region Unity Methods

    #endregion UnityMethods


    #region Public Variables
    //Variables accessible from every other script referencing this class.
    //These variables should not be visible in Inspector and should be hidden by using [HideInInspector] or [ReadOnly]

    #endregion Public Variables


    #region Public Methods    

    [Button]
    public void Replant()
    {
        if (!SaveSystem.SaveFileExists())
            return;

        var allSavedUniqueIDs = SaveSystem.GetAllSavedUniqueIDs();

        for(int i = 0; i < allSavedUniqueIDs.Count; i++)
        {
            if(uniqueIdSceneValidator.FindUniqueIdOnScene(allSavedUniqueIDs[i]) == null)
            {
                var snapshotDatas = SaveSystem.GetSnapshotsByUniqueID(allSavedUniqueIDs[i]);

                foreach (var snapshotData in snapshotDatas)
                {
                    if(snapshotData is Plant.PlantSnapshotData plantSnapshotData)
                    {
                        var plantGameObject = Instantiate(plantsDatabase.GetPlantData(plantSnapshotData.myDatabaseId).plantPrefab, plantSnapshotData.position, Quaternion.identity);
                        var plantedPlant = plantGameObject.GetComponent<Plant>();
                        plantedPlant.LoadMe(plantSnapshotData);
                    }

                    if(snapshotData is Buildable.BuildableSnapshotData buildableSnapshotData)
                    {
                        var buildableGameObject = Instantiate(buildablesDatabase.GetBuildableData(buildableSnapshotData.myDatabaseId).buildablePrefab, buildableSnapshotData.position, buildableSnapshotData.rotation);
                        var plantedBuildable = buildableGameObject.GetComponent<Buildable>();
                        plantedBuildable.LoadMe(buildableSnapshotData);
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
