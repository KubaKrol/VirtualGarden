using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerViewpointManager : MonoBehaviour, ISnapshot
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
    
    [Button]
    public void CreatePlayerViewpoint()
    {
        if (playerViewpointsList == null)
            playerViewpointsList = new List<PlayerViewpoint>(3);

        var newPlayerViewpoint = new PlayerViewpoint();
        newPlayerViewpoint.position = transform.position;
        newPlayerViewpoint.rotation = transform.rotation;

        if(playerViewpointsList.Count == 3)
        {
            playerViewpointsList.Insert(0, newPlayerViewpoint);
            playerViewpointsList.RemoveAt(3);
        }
        else
        {
            playerViewpointsList.Insert(0, newPlayerViewpoint);
        }
    }

    #endregion Public Methods


    #region Private Variables

    [ShowInInspector] private List<PlayerViewpoint> playerViewpointsList;

    #endregion Private Variables


    #region Private Methods
    //Private methods, accessible only from this class.


    #endregion Private Methods


    #region Coroutines
    //IEnumerators<>


    #endregion Coroutines


    #region Public Types

    [System.Serializable]
    public struct PlayerViewpoint
    {
        public Vector3 position;
        public Quaternion rotation;
    }

    #endregion Public Types

    public UniqueId MyUniqueID => GetComponent<UniqueId>();

    [System.Serializable]
    public class PlayerViewpointSnapshotData : SnapshotData
    {
        public List<PlayerViewpoint> playerViewpointsList;
    }

    public void LoadMe(SnapshotData snapshotData)
    {
        if(snapshotData is PlayerViewpointSnapshotData playerViewpointSnapshotData)
        {
            playerViewpointsList = playerViewpointSnapshotData.playerViewpointsList;
        }
    }

    public SnapshotData SaveMe()
    {
        var playerViewpointSnapshotData = new PlayerViewpointSnapshotData();
        playerViewpointSnapshotData.playerViewpointsList = playerViewpointsList;
        return playerViewpointSnapshotData;
    }
}
