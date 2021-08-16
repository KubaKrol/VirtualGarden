using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Detailer : MonoBehaviour
{
    #region Inspector Variables

    [Title("Dependencies")]

    [SerializeField] private GameInput gameInput;

    [SerializeField] private Transform raycastOrigin;

    [SerializeField] private CanvasGroup myCanvasGroup;

    [SerializeField] private TextMeshProUGUI treeAgeText;
    [SerializeField] private TextMeshProUGUI treeIrigationText;
    [SerializeField] private TextMeshProUGUI treeStageText;
    [SerializeField] private Image irrigationFillImage;

    [Title("Settings")]

    [SerializeField] private LayerMask layerMask;

    #endregion Inspector Variables


    #region Unity Methods

    private void Update()
    {
        if (gameInput.CurrentGameInput.UseDetailer)
        {
            if (!raycastOrigin.gameObject.activeSelf)
                raycastOrigin.gameObject.SetActive(true);
        }
        else
        {
            if(raycastOrigin.gameObject.activeSelf)
                raycastOrigin.gameObject.SetActive(false);
        }

        if (Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out detailerRaycastHit, 10f, layerMask) && raycastOrigin.gameObject.activeSelf)
        {
            Plant raycastedPlant;

            if (raycastedPlant = detailerRaycastHit.collider.GetComponentInParent<Plant>())
            {
                myCanvasGroup.alpha = 1f;

                var plantStats = raycastedPlant.GetStats();

                if(raycastedPlant.CurrentPlantState != EPlantState.Dead)
                {
                    treeAgeText.text = "Age: " + plantStats.age.ToString();
                    irrigationFillImage.fillAmount = plantStats.irrigationPercentage;
                    treeIrigationText.text = "Irrigation: " /*+ plantStats.irrigationPercentage*/ + "\nAlive time left: " + plantStats.aliveTimeLeft.ToString();
                    treeStageText.text = "Growth stage: " + plantStats.currentStage + "/" + plantStats.maxStages + "\nTime to next stage: " + plantStats.timeToNextStage.ToString();
                } 
                else
                {
                    treeAgeText.text = "Age: " + "DEAD";
                    irrigationFillImage.fillAmount = plantStats.irrigationPercentage;
                    treeIrigationText.text = "Irrigation: " /*+ plantStats.irrigationPercentage*/ + "\nAlive time left: " + "DEAD";
                    treeStageText.text = "Growth stage: " + "DEAD" + "\nTime to next stage: " + "DEAD";
                }
            }
        }
        else
        {
            myCanvasGroup.alpha = 0f;
            treeAgeText.text = "Age: ";
            irrigationFillImage.fillAmount = 0f;
            treeIrigationText.text = "Irrigation: " + " " + "\nAlive days left: " + " ";
            treeStageText.text = "Growth stage: " + " " + "/" + " ";
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

    private RaycastHit detailerRaycastHit;


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
