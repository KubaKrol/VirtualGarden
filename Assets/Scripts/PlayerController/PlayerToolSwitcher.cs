using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToolSwitcher : MonoBehaviour
{
    #region Inspector Variables

    [Title("Dependencies")]

    [SerializeField] private GameInput gameInput;

    [Title("Settings")]

    [SerializeField] private List<GardeningTool> gardeningTools;

    #endregion Inspector Variables


    #region Unity Methods

    private void Start()
    {
        gardeningTools[0].PickUp();
    }

    private void Update()
    {
        if (gameInput.CurrentGameInput.ChangeTool)
        {
            for(int i = 0; i < gardeningTools.Count; i++)
            {
                if (gardeningTools[i].CurrentState == EGardeningToolState.BeingUsed)
                {
                    if(i == gardeningTools.Count - 1)
                    {
                        gardeningTools[i].PutDown();
                        gardeningTools[0].PickUp();
                        return;
                    }

                    gardeningTools[i].PutDown();
                    gardeningTools[i + 1].PickUp();
                    return;
                }
            }
        }
    }

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
}
