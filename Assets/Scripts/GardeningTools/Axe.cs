using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : GardeningTool
{
    #region Inspector Variables
    //These are variables that should be set in the Inspector - Use [SerializeField] or [ShowInInspector]
    //Can be public or private.

    #endregion Inspector Variables


    #region Unity Methods

    public override void Update()
    {
        base.Update();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Plant"))
        {
            var plant = other.gameObject.GetComponentInParent<Plant>();

            if (plant != null)
                if (!interactingPlants.Contains(plant))
                    interactingPlants.Add(plant);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Plant"))
        {
            var plant = other.gameObject.GetComponentInParent<Plant>();

            if (plant != null)
                if (interactingPlants.Contains(plant))
                    interactingPlants.Remove(plant);
        }
    }

    #endregion UnityMethods


    #region Public Variables
    //Variables accessible from every other script referencing this class.
    //These variables should not be visible in Inspector and should be hidden by using [HideInInspector] or [ReadOnly]

    #endregion Public Variables


    #region Public Methods

    public override void Use()
    {
        if (gameInput.CurrentGameInput.Use_Single)
        {
            for(int i = 0; i < interactingPlants.Count; i++)
            {
                interactingPlants[i].CutMeDown();
            }

            interactingPlants.Clear();
        }
    }

    public override void PickUp()
    {
        base.PickUp();
        interactingPlants.Clear();
    }

    #endregion Public Methods


    #region Private Variables

    private List<Plant> interactingPlants = new List<Plant>();


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
