using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AxeUI : MonoBehaviour
{
    #region Inspector Variables

    [SerializeField] private Axe myAxe;
    [SerializeField] private TextMeshProUGUI axeSpeedText;
    [SerializeField] private TextMeshProUGUI axeSpeedText_TopSpeed;

    #endregion Inspector Variables


    #region Unity Methods

    private void Update()
    {
        if(myAxe.CurrentState == EGardeningToolState.BeingUsed)
        {
            if (!axeSpeedText.gameObject.activeSelf)
            {
                axeSpeedText.gameObject.SetActive(true);
                axeSpeedText_TopSpeed.gameObject.SetActive(true);
            }

            if (myAxe.Speed > topSpeed)
                topSpeed = myAxe.Speed;

            axeSpeedText_TopSpeed.text = "Top speed: " + topSpeed.ToString();
            axeSpeedText.text = "Axe speed: " + Math.Round(myAxe.Speed, 3).ToString();

            if(myAxe.Speed == 0f)
            {
                axeSpeedText.text = "Axe speed: 0,000";
            }
        }
        else
        {
            if (axeSpeedText.gameObject.activeSelf)
            {
                axeSpeedText.gameObject.SetActive(false);
                axeSpeedText_TopSpeed.gameObject.SetActive(false);
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

    private float topSpeed = 0f;

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
