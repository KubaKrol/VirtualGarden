using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlantGrowthStage
{
    [Tooltip("time to reach this stage")]
    public float timeToReachStage;

    [Tooltip("GameObject which represents the stage of the healthy plant")]
    public GameObject healthyPlantStageGameObject;
    [Tooltip("GameObject which represents the stage of the healthy plant")]
    public GameObject sickPlantStageGameObject;
    [Tooltip("GameObject which represents the stage of the healthy plant")]
    public GameObject deadPlantStageGameObject;
}
