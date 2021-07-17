using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public Dictionary<string, int> intDatas = new Dictionary<string, int>();
    public Dictionary<string, float> floatDatas = new Dictionary<string, float>();
    public Dictionary<string, string> stringDatas = new Dictionary<string, string>();
    public Dictionary<string, bool> boolDatas = new Dictionary<string, bool>();
    
    public Dictionary<string, List<int>> intListDatas = new Dictionary<string, List<int>>();
    public Dictionary<string, List<float>> floatListDatas = new Dictionary<string, List<float>>();
    public Dictionary<string, List<string>> stringListDatas = new Dictionary<string, List<string>>();
    public Dictionary<string, List<bool>> boolListDatas = new Dictionary<string, List<bool>>();

    public Dictionary<string, Dictionary<string, List<SnapshotData>>> snapshotContainers = new Dictionary<string, Dictionary<string, List<SnapshotData>>>();
}
