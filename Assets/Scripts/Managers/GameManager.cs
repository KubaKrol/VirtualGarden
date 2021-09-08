using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, ISnapshot
{
    #region Inspector Variables

    [Title("Dependencies")]

    [SerializeField] private OVRPlayerController VRPlayerController;

    #endregion Inspector Variables


    #region Unity Methods

    private void Awake()
    {
        //Singleton init
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    #endregion UnityMethods


    #region Public Variables

    public static GameManager instance;

    [ShowInInspector] public EGameState currentGameState { get; private set; }
    [ShowInInspector] public ESimulationMode currentSimulationMode { get; private set; }

    #endregion Public Variables


    #region Public Methods

    public void ChangeSimulationMode(ESimulationMode newSimulationMode)
    {
        currentSimulationMode = newSimulationMode;
    }

    public void ChangeSimulationMode()
    {
        if (currentSimulationMode == ESimulationMode.Realtime)
            currentSimulationMode = ESimulationMode.Sandbox;
        else
            currentSimulationMode = ESimulationMode.Realtime;
    }

    public void ChangeGameState(EGameState newGameState)
    {
        currentGameState = newGameState;

        if (currentGameState == EGameState.InMainMenu)
        {
            VRPlayerController.SetHaltUpdateMovement(false);
        }
        else
        {
            VRPlayerController.SetHaltUpdateMovement(true);
        }
    }

    public void ChangeGameState()
    {
        if (currentGameState == EGameState.InMainMenu)
        {
            currentGameState = EGameState.InProgress;
            VRPlayerController.SetHaltUpdateMovement(false);
        }
        else
        {
            currentGameState = EGameState.InMainMenu;
            VRPlayerController.SetHaltUpdateMovement(true);
        }
    }

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

    public class GameManagerSnapshotData : SnapshotData
    {
        public ESimulationMode simulationMode;
    }

    public void LoadMe(SnapshotData snapshotData)
    {
        if (snapshotData is GameManagerSnapshotData gameManagerSnapshotData)
        {
            currentSimulationMode = gameManagerSnapshotData.simulationMode;
        }
    }

    public SnapshotData SaveMe()
    {
        var gameManagerSnapshotData = new GameManagerSnapshotData();
        gameManagerSnapshotData.simulationMode = currentSimulationMode;
        return gameManagerSnapshotData;
    }
}
