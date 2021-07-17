using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UniqueId))]
public class TerrainSnapshot : MonoBehaviour, ISnapshot
{
    #region Inspector Variables

    [Title("Dependencies")]

    [SerializeField] public Terrain myTerrain;

    #endregion Inspector Variables


    #region Unity Methods
    //Methods from MonoBehaviour.
    //OnEnable(), OnDisable(), Awake(), Start(), Update() etc...

    #endregion UnityMethods


    #region Public Variables
    //Variables accessible from every other script referencing this class.
    //These variables should not be visible in Inspector and should be hidden by using [HideInInspector] or [ReadOnly]

    #endregion Public Variables


    #region Public Methods
    //Methods accessible from every other script referencing this class.


    #endregion Public Methods


    #region Private Variables
    //Private variables, accessible only from this class.


    #endregion Private Variables


    #region Private Methods
    //Private methods, accessible only from this class.


    #endregion Private Methods


    #region Coroutines
    //IEnumerators<>


    #endregion Coroutines


    #region Public Types
    //enums, structs etc...


    #endregion Public Types
    public UniqueId MyUniqueID => GetComponent<UniqueId>();

    public class TerrainSnapshotData : SnapshotData
    {
        public TerrainData terrainData;
    }

    public void LoadMe(SnapshotData snapshotData)
    {
        if(snapshotData is TerrainSnapshotData terrainSnapshotData)
        {
            myTerrain.terrainData = terrainSnapshotData.terrainData;
        }
    }

    public SnapshotData SaveMe()
    {
        var terrainSnapshotData = new TerrainSnapshotData();
        terrainSnapshotData.terrainData = myTerrain.terrainData;
        return terrainSnapshotData;
    }
}
