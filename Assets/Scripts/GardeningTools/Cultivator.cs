using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cultivator : GardeningTool
{
    #region Inspector Variables

    [Title("Cultivator Dependencies")]

    [SerializeField] private PlantsDatabase plantsDatabase;

    [Title("Cultivator Settings")]

    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private LayerMask layerMask;

    #endregion Inspector Variables


    #region Unity Methods

    private void Start()
    {
        //TEMP
        PickUp();
        SelectPlant(EPlant.Tree_01);
    }

    private void Update()
    {
        if(CurrentState == EGardeningToolState.BeingUsed)
        {
            CheckPlanting();

            if (gameInput.CurrentGameInput.Use)
            {
                Plant();
            }
        }
    }

    #endregion UnityMethods


    #region Public Variables

    public PlantData CurrentlySelectedPlant { get; private set; }

    public bool PlantingAvailable { get; private set; }

    #endregion Public Variables


    #region Public Methods

    [Title("Functionalities")]

    [Button]
    public void SelectPlant(EPlant plant)
    {
        CurrentlySelectedPlant = plantsDatabase.GetPlantData(plant);
    }

    [Button]
    public void Plant()
    {
        if (PlantingAvailable)
        {
            var plantGameObject = Instantiate(CurrentlySelectedPlant.plantPrefab, plantingCheckRaycastHit.point, Quaternion.identity);
            var plantedPlant = plantGameObject.GetComponent<Plant>();
            plantedPlant.PlantMe();
        }
    }

    public override void Use()
    {
        base.Use();
    }

    #endregion Public Methods


    #region Private Variables

    private RaycastHit plantingCheckRaycastHit;

    private PlantPreview currentPlantPreview;

    #endregion Private Variables


    #region Private Methods
    
    private bool CheckPlanting()
    {
        if(Physics.Raycast(raycastOrigin.position, transform.up, out plantingCheckRaycastHit, 10f, layerMask))
        {
            DisplayPlantPreview();
            PlantingAvailable = true;
            return true;
        }

        if (currentPlantPreview != null)
            DestroyPlantPreview();

        PlantingAvailable = false;
        return false;
    }

    private void DisplayPlantPreview()
    {
        if(currentPlantPreview == null && CurrentlySelectedPlant != null)
        {
            var currentPlantPreviewObject = Instantiate(CurrentlySelectedPlant.plantPreviewPrefab.gameObject, plantingCheckRaycastHit.point, Quaternion.identity);
            currentPlantPreview = currentPlantPreviewObject.GetComponent<PlantPreview>();
        }

        currentPlantPreview.gameObject.transform.position = plantingCheckRaycastHit.point;
    }

    private void DestroyPlantPreview()
    {
        if(currentPlantPreview != null)
        {
            Destroy(currentPlantPreview.gameObject);
            currentPlantPreview = null;
        }
    }


    #endregion Private Methods


    #region Coroutines
    //IEnumerators<>


    #endregion Coroutines


    #region Public Types
    //enums, structs etc...


    #endregion Public Types
}
