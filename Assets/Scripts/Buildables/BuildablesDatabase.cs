using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildablesDatabase", menuName = "Buildables/Database")]
public class BuildablesDatabase : ScriptableObject
{
    public List<BuildableData> buildablesDatabase;

    public int buildablesDatabaseIterator;

    public BuildableData GetBuildableData(int id)
    {
        if(id >= 0 && id < buildablesDatabase.Count)
        {
            return buildablesDatabase[id];
        }

        return null;
    }

    public BuildableData GetNextBuildableData()
    {
        buildablesDatabaseIterator += 1;

        if (buildablesDatabaseIterator >= buildablesDatabase.Count)
            buildablesDatabaseIterator = 0;

        return buildablesDatabase[buildablesDatabaseIterator];
    }

    public BuildableData GetPreviousBuildableData()
    {
        buildablesDatabaseIterator -= 1;

        if (buildablesDatabaseIterator < 0)
            buildablesDatabaseIterator = buildablesDatabase.Count - 1;

        return buildablesDatabase[buildablesDatabaseIterator];
    }
}

[System.Serializable]
public class BuildableData
{
    public Buildable buildablePrefab;
    public BuildablePreview buildablePreviewPrefab;
}
