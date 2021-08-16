using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UniqueId))]
public class Plant : MonoBehaviour, ISnapshot
{
    #region Inspector Variables

    [Title("Plant Settings")]

    [SerializeField] public List<PlantGrowthStage> plantGrowthStages;
    [SerializeField] public float maxIrrigationTime;

    #endregion Inspector Variables


    #region Unity Methods
    
    private void Update()
    {
        Vegetate();
    }

    #endregion UnityMethods


    #region Public Variables

    [Title("Properties")]

    [ShowInInspector]
    [ReadOnly]
    public System.DateTime PlantedDate { get; private set; }
    [ShowInInspector]
    [ReadOnly]
    public double PlantedTime_InSeconds => (System.DateTime.Now - PlantedDate).TotalSeconds;
    [ShowInInspector]
    [ReadOnly]
    public EPlantState CurrentPlantState;
    [ShowInInspector]
    [ReadOnly]
    public PlantWateringRecord LatestWateringRecord { get; private set; }
    [ShowInInspector]
    [ReadOnly]
    public float IrrigationLevel { get; private set; }
    [ShowInInspector]
    [ReadOnly]
    public PlantGrowthStage CurrentGrowthStage { get; private set; }

    #endregion Public Variables


    #region Public Methods

    [Title("Functionalities")]

    [Button]
    public void PlantMe(int databaseId)
    {
        myDatabaseId = databaseId;

        UpdatePlantedDateWithCurrentDate();
        IrrigationLevel = maxIrrigationTime;

        LatestWateringRecord = new PlantWateringRecord();
        LatestWateringRecord.irrigationLevelDuringWatering = maxIrrigationTime;
        LatestWateringRecord.wateringDate = System.DateTime.Now;

        EventManager.OnPlantPlanted.Invoke();
    }

    [Button]
    public void WaterMe(float value)
    {
        if (CurrentPlantState == EPlantState.Dead)
            return;

        if(IrrigationLevel + value > maxIrrigationTime)
        {
            IrrigationLevel = maxIrrigationTime;
        } 
        else
        {
            IrrigationLevel += value;
        }

        var newWateringRecord = new PlantWateringRecord();
        newWateringRecord.irrigationLevelDuringWatering = IrrigationLevel;
        newWateringRecord.wateringDate = System.DateTime.Now;
        LatestWateringRecord = newWateringRecord;
    }

    [Button]
    public void CutMeDown()
    {
        Destroy(this.gameObject, 0.01f);
    }

    public void UpdatePlantedDateWithCurrentDate()
    {
        PlantedDate = System.DateTime.Now;
    }

    public void UpdatePlantedDate(System.DateTime date)
    {
        PlantedDate = date;
    }

    [Button]
    public void DebugStats()
    {
        Debug.Log("Age: " + (System.DateTime.Now - PlantedDate));
        Debug.Log("Days planted: " + Mathf.Floor((float)(System.DateTime.Now - PlantedDate).TotalDays));
        Debug.Log("Hours planted: " + Mathf.Floor((float)(System.DateTime.Now - PlantedDate).TotalHours));
        Debug.Log("Minutes planted: " + Mathf.Floor((float)(System.DateTime.Now - PlantedDate).TotalMinutes));
        Debug.Log("Seconds planted: " + (System.DateTime.Now - PlantedDate).TotalSeconds);

        Debug.Log("Irrigation Level: " + IrrigationLevel);
    }

    public PlantStats GetStats()
    {
        PlantStats myStats = new PlantStats();
        myStats.age = System.DateTime.Now - PlantedDate;
        myStats.daysPlanted = (int)Mathf.Floor((float)(System.DateTime.Now - PlantedDate).TotalDays);
        myStats.hoursPlanted = (int)Mathf.Floor((float)(System.DateTime.Now - PlantedDate).TotalHours);
        myStats.minutesPlanted = (int)Mathf.Floor((float)(System.DateTime.Now - PlantedDate).TotalMinutes);
        myStats.secondsPlanted = (int)(System.DateTime.Now - PlantedDate).TotalSeconds;
        myStats.irrigationLevel = IrrigationLevel;
        myStats.irrigationPercentage = IrrigationLevel / maxIrrigationTime;
        myStats.aliveTimeLeft = TimeSpan.FromSeconds(Convert.ToDouble(IrrigationLevel));
        myStats.aliveDaysLeft = TimeSpan.FromSeconds(Convert.ToDouble(IrrigationLevel)).Days;
        myStats.currentStage = plantGrowthStages.IndexOf(CurrentGrowthStage);
        myStats.maxStages = plantGrowthStages.Count - 1;

        if(myStats.currentStage + 1 <= plantGrowthStages.Count - 1)
            myStats.timeToNextStage = TimeSpan.FromSeconds(Convert.ToDouble(plantGrowthStages[myStats.currentStage + 1].timeToReachStage - PlantedTime_InSeconds));

        return myStats;
    }

    #endregion Public Methods


    #region Private Variables

    private int myDatabaseId;

    #endregion Private Variables


    #region Private Methods
    
    private void Vegetate()
    {
        IrrigationLevel = CalculateIrrigationLevel();

        var calculatedPlantStage = CalculateCurrentPlantStage();
        var calculatedPlantState = CalculateCurrentPlantState();

        if(CurrentGrowthStage != calculatedPlantStage)
            UpdatePlantStage(calculatedPlantStage);

        if (CurrentPlantState != calculatedPlantState)
            UpdatePlantState(calculatedPlantState);
    }

    private PlantGrowthStage CalculateCurrentPlantStage()
    {
        if(plantGrowthStages != null && plantGrowthStages.Count > 0)
        {
            PlantGrowthStage calculatedGrowthStage = plantGrowthStages[0];
            
            for(int i = 0; i < plantGrowthStages.Count; i++)
            {
                if(PlantedTime_InSeconds > plantGrowthStages[i].timeToReachStage)
                {
                    calculatedGrowthStage = plantGrowthStages[i];
                }
            }

            return calculatedGrowthStage;
        }

        return null;
    }

    private float CalculateIrrigationLevel()
    {
        if(LatestWateringRecord != null)
        {
            var timeElapsed = (System.DateTime.Now - LatestWateringRecord.wateringDate).TotalSeconds;
            var calculatedIrrigationLevel = LatestWateringRecord.irrigationLevelDuringWatering - timeElapsed;
            return (float)calculatedIrrigationLevel;
        } 
        else
        {
            return 0f;
        }
    }

    private void UpdatePlantStage(PlantGrowthStage plantGrowthStage)
    {
        CurrentGrowthStage = plantGrowthStage;
        UpdatePlantRepresentation(CurrentGrowthStage, CurrentPlantState);
    }

    private EPlantState CalculateCurrentPlantState()
    {
        if (IrrigationLevel < 0)
        {
            return EPlantState.Dead;
        }

        if (IrrigationLevel < maxIrrigationTime * 0.5f)
        {
            return EPlantState.Sick;
        }

        return EPlantState.Healthy;
    }

    private void UpdatePlantState(EPlantState plantState)
    {
        CurrentPlantState = plantState;
        UpdatePlantRepresentation(CurrentGrowthStage, CurrentPlantState);
    }

    private void UpdatePlantRepresentation(PlantGrowthStage currentGrowthStage, EPlantState currentPlantState)
    {
        foreach (var plantStage in plantGrowthStages)
        {
            if(plantStage.healthyPlantStageGameObject != null)
                plantStage.healthyPlantStageGameObject.SetActive(false);
            if (plantStage.sickPlantStageGameObject != null)
                plantStage.sickPlantStageGameObject.SetActive(false);
            if (plantStage.deadPlantStageGameObject != null)
                plantStage.deadPlantStageGameObject.SetActive(false);
        }

        if (currentPlantState == EPlantState.Healthy || currentGrowthStage.sickPlantStageGameObject == null || currentGrowthStage.deadPlantStageGameObject == null)
            currentGrowthStage.healthyPlantStageGameObject?.SetActive(true);

        if (currentPlantState == EPlantState.Sick)
            currentGrowthStage.sickPlantStageGameObject?.SetActive(true);

        if (currentPlantState == EPlantState.Dead)
            currentGrowthStage.deadPlantStageGameObject?.SetActive(true);
    }

    #endregion Private Methods


    #region Coroutines
    //IEnumerators<>


    #endregion Coroutines


    #region Public Types
    //enums, structs etc...


    #endregion Public Types

    public UniqueId MyUniqueID => GetComponent<UniqueId>();

    public class PlantSnapshotData : SnapshotData
    {
        public int myDatabaseId;
        public float irrigationLevel;
        public int growthStage;
        public System.DateTime plantedDate;
        public PlantWateringRecord latestWateringRecord;

        public Vector3 position;
    }

    public void LoadMe(SnapshotData snapshotData)
    {
        if(snapshotData is PlantSnapshotData plantSnapshotData)
        {
            myDatabaseId = plantSnapshotData.myDatabaseId;
            IrrigationLevel = plantSnapshotData.irrigationLevel;
            PlantedDate = plantSnapshotData.plantedDate;
            LatestWateringRecord = plantSnapshotData.latestWateringRecord;
        }
    }

    public SnapshotData SaveMe()
    {
        var newPlantSnapshotData = new PlantSnapshotData();

        newPlantSnapshotData.myDatabaseId = myDatabaseId;
        newPlantSnapshotData.irrigationLevel = IrrigationLevel;
        newPlantSnapshotData.plantedDate = PlantedDate;
        newPlantSnapshotData.latestWateringRecord = LatestWateringRecord;
        newPlantSnapshotData.position = transform.position;

        return newPlantSnapshotData;
    }
}
