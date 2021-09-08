using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameModeDisplayer : MonoBehaviour
{
    #region Inspector Variables
    //These are variables that should be set in the Inspector - Use [SerializeField] or [ShowInInspector]
    //Can be public or private.

    #endregion Inspector Variables


    #region Unity Methods

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        text.text = GameManager.instance.currentSimulationMode.ToString();
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

    private TextMeshProUGUI text;

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
