using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlantsDatabase", menuName = "Plants/Database")]
public class PlantsDatabase : ScriptableObject
{
    public List<PlantData> plantsDatabase;

    private int plantDatabaseIterator = 0;

    public PlantData GetPlantData(int id)
    {
        if(id >= 0 && id < plantsDatabase.Count)
        {
            return plantsDatabase[id];
        }

        return null;
    }

    public int GetDatabaseIndex(PlantData plantData)
    {
        if (plantsDatabase.Contains(plantData))
        {
            return plantsDatabase.IndexOf(plantData);
        }

        return -1;
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
    public Plant plantPrefab;
    public PlantPreview plantPreviewPrefab;
}
