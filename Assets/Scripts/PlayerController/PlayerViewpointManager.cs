using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerViewpointManager : MonoBehaviour, ISnapshot
{
    #region Inspector Variables

    [Title("Dependencies")]

    [SerializeField] private PlayerCamera playerCamera;

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
        newPlayerViewpoint.playerCameraRotX = playerCamera.GetComponent<SmoothMouseLook>().rotX;
        newPlayerViewpoint.playerCameraRotY = playerCamera.GetComponent<SmoothMouseLook>().rotY;

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

    [Button]
    public void MoveToViewport()
    {
        var viewportIndex = currentViewportIndex;
        transform.position = playerViewpointsList[viewportIndex].position;
        playerCamera.GetComponent<SmoothMouseLook>().rotX = playerViewpointsList[viewportIndex].playerCameraRotX;
        playerCamera.GetComponent<SmoothMouseLook>().rotY = playerViewpointsList[viewportIndex].playerCameraRotY;
        currentViewportIndex++;

        if (currentViewportIndex >= playerViewpointsList.Count)
            currentViewportIndex = 0;
        
    }

    #endregion Public Methods


    #region Private Variables

    [ShowInInspector] private List<PlayerViewpoint> playerViewpointsList;

    private int currentViewportIndex = 0;

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
        public float playerCameraRotX;
        public float playerCameraRotY;
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
