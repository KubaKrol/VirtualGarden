using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuNavigator : MonoBehaviour
{
    #region Inspector Variables

    [Title("Dependencies")]

    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private GameInput gameInput;

    [Title("Settings")]

    [SerializeField] private LayerMask layerMask;

    #endregion Inspector Variables


    #region Unity Methods

    private void Update()
    {
        if (Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out menuNavigatorRaycastHit, 10f, layerMask) && raycastOrigin.gameObject.activeSelf)
        {
            Button raycastedButton = null;

            if (raycastedButton = menuNavigatorRaycastHit.collider.GetComponent<Button>())
            {   
                if(selectedButton != raycastedButton)
                {
                    EventSystem.current.SetSelectedGameObject(null);
                    selectedButton = raycastedButton;
                }

                raycastedButton.Select();
            }
        } 
        else
        {
            EventSystem.current.SetSelectedGameObject(null);
            selectedButton = null;
        }

        if (selectedButton != null)
        {
            if (gameInput.CurrentGameInput.Use_Single)
            {
                selectedButton.onClick.Invoke();
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

    private RaycastHit menuNavigatorRaycastHit;

    private Button selectedButton;

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
