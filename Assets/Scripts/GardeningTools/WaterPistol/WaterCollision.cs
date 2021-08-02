using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCollision : MonoBehaviour
{
    #region Inspector Variables
    


    #endregion Inspector Variables


    #region Unity Methods

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Plant"))
        {
            var plant = other.gameObject.GetComponentInParent<Plant>();

            if(plant != null)
                if (!plantsInRange.Contains(plant))
                    plantsInRange.Add(plant);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Plant"))
        {
            var plant = other.gameObject.GetComponentInParent<Plant>();

            if(plant != null)
                if (plantsInRange.Contains(plant))
                    plantsInRange.Remove(plant);
        }
    }

    #endregion UnityMethods


    #region Public Variables

    [HideInInspector] public List<Plant> plantsInRange = new List<Plant>();

    #endregion Public Variables


    #region Public Methods

    /*public void TurnOn(WaterPistol myPistol)
    {
        gameObject.SetActive(true);
    }

    public void TurnOff(WaterPistol myPistol)
    {
        gameObject.SetActive(false);
        plantsInRange.Clear();
    }*/


    #endregion Public Methods


    #region Private Variables


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
