using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UniqueId))]
public class Buildable : MonoBehaviour, ISnapshot
{
    #region Inspector Variables
    //These are variables that should be set in the Inspector - Use [SerializeField] or [ShowInInspector]
    //Can be public or private.

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

    public void BuildMe(int databaseId)
    {
        myDatabaseId = databaseId;
    }

    #endregion Public Methods


    #region Private Variables

    private int myDatabaseId;

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

    public class BuildableSnapshotData : SnapshotData
    {
        public int myDatabaseId;
        public Vector3 position;
        public Quaternion rotation;
    }

    public void LoadMe(SnapshotData snapshotData)
    {
        if (snapshotData is BuildableSnapshotData buildableSnapshotData)
        {
            myDatabaseId = buildableSnapshotData.myDatabaseId;
            transform.position = buildableSnapshotData.position;
            transform.rotation = buildableSnapshotData.rotation;
        }
    }

    public SnapshotData SaveMe()
    {
        var newBuildableSnapshotData = new BuildableSnapshotData();

        newBuildableSnapshotData.myDatabaseId = myDatabaseId;
        newBuildableSnapshotData.position = transform.position;
        newBuildableSnapshotData.rotation = transform.rotation;

        return newBuildableSnapshotData;
    }
}
