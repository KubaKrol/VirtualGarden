using System;
using System.Collections.Generic;
using System.IO;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.Events;

public static class SaveSystem
{
#region Public Variables

    public static DataFormat dataFormat { get; private set; } = DataFormat.JSON;
    public static ESaveProfile currentSaveProfile { get; private set; } = ESaveProfile.profile1;
    public static string currentSnapshotContainer { get; private set; } = "Default";

    public static UnityAction gamePreSave;
    public static UnityAction<Transform> gameSaved;
    public static UnityAction gamePreLoad;
    public static UnityAction gameLoaded;
    
#endregion Public Variables


#region Public Methods

    public static bool SaveFileExists()
    {
        return File.Exists(FilePath(currentSaveProfile));
    }
    
    public static void SaveGame(Transform checkpointPlayerTransform)
    {
        gamePreSave?.Invoke();
        
        if(!SaveFileExists())
            Debug.Log("Creating new save file " + currentSaveProfile.ToString());
        
        SaveSnapshots(checkpointPlayerTransform);
        SerializeData();
        
        gameSaved?.Invoke(checkpointPlayerTransform);
    }
    
    public static void LoadGame()
    {
        if (SaveFileExists())
        {
            gamePreLoad?.Invoke();
            
            DeserializeData();
            LoadSnapshots();
            
            gameLoaded?.Invoke();
        }
        else
        {
            Debug.Log("Save file does not exist");
        }
    }

    public static void SetSaveProfile(ESaveProfile newSaveProfile)
    {
        currentSaveProfile = newSaveProfile;
    }
    
    public static void SetSnapshotContainer(string container)
    {
        currentSnapshotContainer = container;
    }
    
    public static void AssignSceneSnapshots(List<ISnapshot> snapshotList)
    {
        allSnapshotsList.Clear();
        allSnapshotsList = snapshotList;
    }

    public static List<string> GetAllSavedUniqueIDs()
    {
        List<string> uniqueIDList = new List<string>();

        foreach(var snapshot in GameData.snapshotContainers[currentSnapshotContainer])
        {
            uniqueIDList.Add(snapshot.Key);
        }

        return uniqueIDList;
    }

    public static List<SnapshotData> GetSnapshotsByUniqueID(string uniqueID)
    {
        if (GameData.snapshotContainers[currentSnapshotContainer].ContainsKey(uniqueID))
        {
            return GameData.snapshotContainers[currentSnapshotContainer][uniqueID];
        }

        return null;
    }

    public static void SaveRecord<T>(string key, T value)
    {
        SaveRecordToDictionary(GetCorrectDictionary<T>(), key, value);
    }

    public static bool RecordExists<T>(string key)
    {
        return GetCorrectDictionary<T>().ContainsKey(key);
    }
    
    public static T GetRecord<T>(string key)
    {
        return GetRecordFromDictionary(GetCorrectDictionary<T>(), key);   
    }
    
    public static string GetGameObjectUniqueID(GameObject gameObject)
    {
        if (gameObject == null)
        {
            return null;
        }

        UniqueId uniqueId;
        if (uniqueId = gameObject.GetComponent<UniqueId>())
            return uniqueId.uniqueId;
        
        return null;
    }

    public static T GetUniqueIDComponent<T>(string uniqueId)
    {
        if (uniqueId == null)
        {
            return default;   
        }

        if (UniqueId.allGuids.ContainsKey(uniqueId))
        {
            return UniqueId.allGuids[uniqueId].GetComponent<T>();   
        }
        else
        {
            throw new NullReferenceException();
        }
    }

    public static GameObject GetUniqueIDGameObject(string uniqueId)
    {
        if (uniqueId == null)
        {
            return null;   
        }

        if (UniqueId.allGuids.ContainsKey(uniqueId))
        {
            return UniqueId.allGuids[uniqueId].gameObject;
        }
        else
        {
            throw new NullReferenceException();
        }
    }

    public static bool ValidateSnapshot(ISnapshot snapshot)
    {
        if (snapshot.MyUniqueID == null)
        {
            if(snapshot is MonoBehaviour mb)
                return false;
        }

        return true;
    }

    /// <summary>
    /// Clearing containers from snapshots that can be cleared, total clear option clears ALL the snapshots - use with caution!
    /// </summary>
    /// <param name="containerToClear"></param>
    /// <param name="totalClear"></param>
    /// <returns></returns>
    public static bool ClearSnapshotContainer(string containerToClear, bool totalClear = false)
    {
        if(GameData == null)
            DeserializeData();

        if (GameData.snapshotContainers.ContainsKey(containerToClear))
        {
            if(totalClear)
                GameData.snapshotContainers[containerToClear].Clear();
            else
            {
                var newContainer = new Dictionary<string, List<SnapshotData>>();

                foreach (var snapshotKVP in GameData.snapshotContainers[containerToClear])
                {
                    foreach (var snapshot in snapshotKVP.Value)
                    {
                        if (snapshot.cannotBeCleared)
                        {
                            if(!newContainer.ContainsKey(snapshotKVP.Key))
                                newContainer.Add(snapshotKVP.Key, new List<SnapshotData>());
                            
                            newContainer[snapshotKVP.Key].Add(snapshot);
                        }
                    }
                }

                GameData.snapshotContainers[containerToClear] = newContainer;
            }
            
            SerializeData();
            Debug.Log("Container " + containerToClear + " cleared");
            return true;
        } 
        else
        {
            Debug.Log(containerToClear + " is not a container - nothing to delete");
            return false;
        }
    }
    
    /// <summary>
    /// Deleting snapshot containers - can be used instead of a total clear.
    /// </summary>
    /// <param name="containerToDelete"></param>
    /// <returns></returns>
    public static bool DeleteSnapshotContainer(string containerToDelete)
    {
        if(GameData == null)
            DeserializeData();

        if (GameData.snapshotContainers.ContainsKey(containerToDelete))
        {
            GameData.snapshotContainers.Remove(containerToDelete);
            SerializeData();
            return true;
        } 
        else
        {
            Debug.Log(containerToDelete + " is not a container - nothing to delete");
            return false;
        }
    }
    
#endregion Public Methods


#region Private Variables
    
    private static GameData GameData = new GameData();
    private static List<ISnapshot> allSnapshotsList = new List<ISnapshot>();
    private static int saveDataVersion = 1;

#endregion Private Variables


#region Private Methods
    
    private static string FilePath(ESaveProfile saveProfile)
    {
        var fileName = "MySaveData.save";
        
        switch (saveProfile)
        {
            case ESaveProfile.profile1:
                fileName = "Profile1.save";
                break;
            case ESaveProfile.profile2:
                fileName = "Profile2.save";
                break;
            case ESaveProfile.profile3:
                fileName = "Profile3.save";
                break;
            default:
                fileName = "MySaveData.save";
                break;
        }

        return Application.persistentDataPath + "/" + fileName;
    }
    
    private static void SerializeData()
    {
        byte[] bytes = SerializationUtility.SerializeValue(GameData, dataFormat);
        File.WriteAllBytes(FilePath(currentSaveProfile), bytes);
    }
    
    private static void DeserializeData()
    {
        byte[] bytes = File.ReadAllBytes(FilePath(currentSaveProfile));
        GameData = SerializationUtility.DeserializeValue<GameData>(bytes, dataFormat);
    }

    private static void SaveSnapshots(Transform checkpointPlayerTransform)
    {
        if (allSnapshotsList == null || allSnapshotsList.Count <= 0)
        {
            return;   
        }

        if(!GameData.snapshotContainers.ContainsKey(currentSnapshotContainer))
            GameData.snapshotContainers.Add(currentSnapshotContainer, new Dictionary<string, List<SnapshotData>>());

        foreach (var snapshot in allSnapshotsList)
        {
            if (ValidateSnapshot(snapshot) == false)
                continue;
            
            if(GameData.snapshotContainers[currentSnapshotContainer].ContainsKey(snapshot.MyUniqueID.uniqueId))
                GameData.snapshotContainers[currentSnapshotContainer][snapshot.MyUniqueID.uniqueId].Clear();
        }
        
        foreach (var snapshot in allSnapshotsList)
        {
            if (ValidateSnapshot(snapshot) == false)
                continue;
            
            var snapshotSaveData = snapshot.SaveMe();

            if (checkpointPlayerTransform != null)
            {
                /*if (snapshotSaveData is PlayerController.PlayerSnapshotData playerSnapshotData)
                {
                    playerSnapshotData.position = checkpointPlayerTransform.position;
                    playerSnapshotData.rotation = checkpointPlayerTransform.rotation;
                    snapshotSaveData = playerSnapshotData;
                } */  
            }

            if (GameData.snapshotContainers[currentSnapshotContainer].ContainsKey(snapshot.MyUniqueID.uniqueId))
            {
                GameData.snapshotContainers[currentSnapshotContainer][snapshot.MyUniqueID.uniqueId].Add(snapshotSaveData);
            }
            else
            {
                var snapshotList = new List<SnapshotData>();
                snapshotList.Add(snapshotSaveData);
                GameData.snapshotContainers[currentSnapshotContainer].Add(snapshot.MyUniqueID.uniqueId, snapshotList);
            }
        }
    }

    private static void LoadSnapshots()
    {
        if (allSnapshotsList == null || allSnapshotsList.Count <= 0)
        {
            return; 
        }

        if (!GameData.snapshotContainers.ContainsKey(currentSnapshotContainer))
        {
            return; 
        }
        
        foreach (var snapshot in allSnapshotsList)
        {
            if (ValidateSnapshot(snapshot) == false)
                continue;
            
            if (GameData.snapshotContainers[currentSnapshotContainer].ContainsKey(snapshot.MyUniqueID.uniqueId) && GameData.snapshotContainers[currentSnapshotContainer][snapshot.MyUniqueID.uniqueId] != null)
            {
                if (GameData.snapshotContainers[currentSnapshotContainer][snapshot.MyUniqueID.uniqueId].Count > 0)
                {
                    foreach (var savedData in GameData.snapshotContainers[currentSnapshotContainer][snapshot.MyUniqueID.uniqueId])
                    {
                        snapshot.LoadMe(savedData);
                    }   
                }
            }
        } 
    }

    private static void SaveRecordToDictionary<T>(Dictionary<string, T> dictionary, string key, T value)
    {
        if (dictionary == null)
            return;
        
        if (dictionary.ContainsKey(key))
            dictionary[key] = value;
        else
            dictionary.Add(key, value);
    }

    private static T GetRecordFromDictionary<T>(Dictionary<string, T> dictionary, string key)
    {
        if (!SaveFileExists())
            throw new NullReferenceException();
        
        if (dictionary.ContainsKey(key))
            return dictionary[key];
        
        Debug.LogError("Saved game data does not contain record for: " + key);
        throw new NullReferenceException();  
    }

    private static Dictionary<string, T> GetCorrectDictionary<T>()
    {
        if (typeof(T) == typeof(int))
            return (Dictionary<string, T>) Convert.ChangeType(GameData.intDatas, typeof(Dictionary<string, T>));

        if (typeof(T) == typeof(float))
            return (Dictionary<string, T>) Convert.ChangeType(GameData.floatDatas, typeof(Dictionary<string, T>));   

        if (typeof(T) == typeof(string))
            return (Dictionary<string, T>) Convert.ChangeType(GameData.stringDatas, typeof(Dictionary<string, T>));
        
        if (typeof(T) == typeof(bool))
            return (Dictionary<string, T>) Convert.ChangeType(GameData.boolDatas, typeof(Dictionary<string, T>));
        
        if (typeof(T) == typeof(List<int>))
            return (Dictionary<string, T>) Convert.ChangeType(GameData.intListDatas, typeof(Dictionary<string, T>));
        
        if (typeof(T) == typeof(List<float>))
            return (Dictionary<string, T>) Convert.ChangeType(GameData.floatListDatas, typeof(Dictionary<string, T>));
        
        if (typeof(T) == typeof(List<string>))
            return (Dictionary<string, T>) Convert.ChangeType(GameData.stringListDatas, typeof(Dictionary<string, T>));
        
        if (typeof(T) == typeof(List<bool>))
            return (Dictionary<string, T>) Convert.ChangeType(GameData.boolListDatas, typeof(Dictionary<string, T>));

        Debug.LogError("SaveSystem does not support type: " + typeof(T));
        return null;
    }
    
    
#endregion Private Methods


#region Coroutines
    //IEnumerators<>


#endregion Coroutines


#region Public Types
    //enums, structs etc...
    
    
#endregion Public Types
    
}
