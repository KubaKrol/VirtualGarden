using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour, ISnapshot
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

    [System.Serializable]
    public class PlayerCameraSnapshotData : SnapshotData
    {
        public Quaternion cameraRotation;
    }

    public void LoadMe(SnapshotData snapshotData)
    {
        if(snapshotData is PlayerCameraSnapshotData playerCameraSnapshotData)
        {
            Debug.Log("Camera loaded");
            transform.rotation = playerCameraSnapshotData.cameraRotation;
        }
    }

    public SnapshotData SaveMe()
    {
        var playerCameraSnapshotData = new PlayerCameraSnapshotData();
        playerCameraSnapshotData.cameraRotation = transform.rotation;
        return playerCameraSnapshotData;
    }
}
