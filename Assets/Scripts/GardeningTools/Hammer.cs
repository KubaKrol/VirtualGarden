﻿using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : GardeningTool
{
    #region Inspector Variables

    [Title("Hammer Dependencies")]

    [SerializeField] private BuildablesDatabase buildablesDatabase;

    [Title("Hammer Settings")]

    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private LayerMask layerMask;

    #endregion Inspector Variables


    #region Unity Methods
    public override void Update()
    {
        if (CurrentState == EGardeningToolState.BeingUsed)
        {
            CheckBuilding();
        }

        base.Update();
    }

    #endregion UnityMethods


    #region Public Variables

    public BuildableData CurrentlySelectedBuildable { get; private set; }

    public bool BuildingAvailable { get; private set; }

    #endregion Public Variables


    #region Public Methods
    
    [Title("Functionalities")]

    [Button]
    public void SelectBuildable(int id)
    {
        CurrentlySelectedBuildable = buildablesDatabase.GetBuildableData(id);
    }

    [Button]
    public void Build()
    {
        if (BuildingAvailable)
        {
            var buildableGameObject = Instantiate(CurrentlySelectedBuildable.buildablePrefab, buildingCheckRaycastHit.point, Quaternion.identity);
            var buildable = buildableGameObject.GetComponent<Buildable>();
            buildable.BuildMe();
        }
    }

    public override void Use()
    {
        if (gameInput.CurrentGameInput.Use_Single)
        {
            Build();
        }
    }

    public override void PickUp()
    {
        base.PickUp();
        SelectBuildable(0);
    }

    public override void PutDown()
    {
        base.PutDown();
        DestroyBuildablePreview();
    }

    #endregion Public Methods


    #region Private Variables

    private RaycastHit buildingCheckRaycastHit;

    private BuildablePreview currentBuildablePreview;

    #endregion Private Variables


    #region Private Methods

    private bool CheckBuilding()
    {
        if (Physics.Raycast(raycastOrigin.position, transform.up, out buildingCheckRaycastHit, 10f, layerMask))
        {
            DisplayBuildablePreview();
            BuildingAvailable = true;
            return true;
        }

        if (currentBuildablePreview != null)
            DestroyBuildablePreview();

        BuildingAvailable = false;
        return false;
    }

    private void DisplayBuildablePreview()
    {
        if (currentBuildablePreview == null && CurrentlySelectedBuildable != null)
        {
            var currentBuildablePreviewObject = Instantiate(CurrentlySelectedBuildable.buildablePreviewPrefab.gameObject, buildingCheckRaycastHit.point, Quaternion.identity);
            currentBuildablePreview = currentBuildablePreviewObject.GetComponent<BuildablePreview>();
        }

        currentBuildablePreview.gameObject.transform.position = buildingCheckRaycastHit.point;
    }

    private void DestroyBuildablePreview()
    {
        if (currentBuildablePreview != null)
        {
            Destroy(currentBuildablePreview.gameObject);
            currentBuildablePreview = null;
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