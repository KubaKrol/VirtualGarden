using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildablesDatabase", menuName = "Buildables/Database")]
public class BuildablesDatabase : ScriptableObject
{
    public List<BuildableData> buildablesDatabase;

    public BuildableData GetBuildableData(int id)
    {
        if(id >= 0 && id < buildablesDatabase.Count)
        {
            return buildablesDatabase[id];
        }

        return null;
    }
}

[System.Serializable]
public class BuildableData
{
    public Buildable buildablePrefab;
    public BuildablePreview buildablePreviewPrefab;
}
