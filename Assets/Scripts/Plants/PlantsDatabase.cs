using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlantsDatabase", menuName = "Plants/Database")]
public class PlantsDatabase : ScriptableObject
{
    public List<PlantData> plantsDatabase;

    public PlantData GetPlantData(EPlant plant)
    {
        for(int i = 0; i < plantsDatabase.Count; i++)
        {
            if(plantsDatabase[i].plant == plant)
            {
                return plantsDatabase[i];
            }
        }

        return null;
    }
}

[System.Serializable]
public class PlantData
{
    public EPlant plant;
    public Plant plantPrefab;
    public PlantPreview plantPreviewPrefab;
}
