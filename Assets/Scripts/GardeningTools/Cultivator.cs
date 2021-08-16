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
    [SerializeField] private float previewRotationSpeed = 15f;

    #endregion Inspector Variables


    #region Unity Methods

    public override void Update()
    {
        if(CurrentState == EGardeningToolState.BeingUsed)
        {
            CheckPlanting();

            rotationOffset += previewRotationSpeed * gameInput.CurrentGameInput.ToolHorizontalAxis * Time.deltaTime;

            if(gameInput.CurrentGameInput.ToolVerticalAxis > 0)
            {
                if (canChangePlant)
                {
                    DestroyPlantPreview();
                    CurrentlySelectedPlant = plantsDatabase.GetNextPlantData();
                    canChangePlant = false;
                }
            }

            if(gameInput.CurrentGameInput.ToolVerticalAxis < 0)
            {
                if (canChangePlant)
                {
                    DestroyPlantPreview();
                    CurrentlySelectedPlant = plantsDatabase.GetPreviousPlantData();
                    canChangePlant = false;
                }
            }

            if(gameInput.CurrentGameInput.ToolVerticalAxis == 0)
            {
                canChangePlant = true;
            }
        }

        base.Update();
    }

    #endregion UnityMethods


    #region Public Variables

    public PlantData CurrentlySelectedPlant { get; private set; }

    public bool PlantingAvailable { get; private set; }

    #endregion Public Variables


    #region Public Methods

    [Title("Functionalities")]

    [Button]
    public void SelectPlant(int id)
    {
        CurrentlySelectedPlant = plantsDatabase.GetPlantData(id);
    }

    [Button]
    public void Plant()
    {
        if (PlantingAvailable)
        {
            var plantGameObject = Instantiate(CurrentlySelectedPlant.plantPrefab, plantingCheckRaycastHit.point, Quaternion.AngleAxis(rotationOffset, Vector3.up));
            var plantedPlant = plantGameObject.GetComponent<Plant>();
            plantedPlant.PlantMe(plantsDatabase.GetDatabaseIndex(CurrentlySelectedPlant));
        }
    }

    public override void Use()
    {
        if (gameInput.CurrentGameInput.Use_Single)
        {
            Plant();
        }
    }

    public override void PickUp()
    {
        base.PickUp();
        SelectPlant(0);
    }

    public override void PutDown()
    {
        base.PutDown();
        DestroyPlantPreview();
    }

    #endregion Public Methods


    #region Private Variables

    private RaycastHit plantingCheckRaycastHit;

    private PlantPreview currentPlantPreview;

    private float rotationOffset;

    private bool canChangePlant = true;

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
            var currentPlantPreviewObject = Instantiate(CurrentlySelectedPlant.plantPreviewPrefab.gameObject, plantingCheckRaycastHit.point, Quaternion.AngleAxis(rotationOffset, Vector3.up));
            currentPlantPreview = currentPlantPreviewObject.GetComponent<PlantPreview>();
        }

        currentPlantPreview.transform.rotation = Quaternion.AngleAxis(rotationOffset, Vector3.up);
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
