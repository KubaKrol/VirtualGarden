using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlantsDatabase", menuName = "Plants/Database")]
public class PlantsDatabase : ScriptableObject
{
    public List<PlantData> plantsDatabase;

    private int plantDatabaseIterator = 0;

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

    public PlantData GetNextPlantData()
    {
        plantDatabaseIterator += 1;

        if (plantDatabaseIterator >= plantsDatabase.Count)
            plantDatabaseIterator = 0;

        return plantsDatabase[plantDatabaseIterator];
    }
    public PlantData GetPreviousPlantData()
    {
        plantDatabaseIterator -= 1;

        if (plantDatabaseIterator < 0)
            plantDatabaseIterator = plantsDatabase.Count - 1;

        return plantsDatabase[plantDatabaseIterator];
    }
}

[System.Serializable]
public class PlantData
{
    public EPlant plant;
    public Plant plantPrefab;
    public PlantPreview plantPreviewPrefab;
}
