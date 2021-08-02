using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPistol : GardeningTool
{
    #region Inspector Variables

    [Title("WaterPistol Dependencies")]

    [SerializeField] private WaterCollision waterCollision;
    [SerializeField] private ParticleSystem waterParticleSystem;

    [Title("Settings")]

    [SerializeField] private float wateringSpeed = 2f;

    #endregion Inspector Variables


    #region Unity Methods

    public override void Update()
    {
        base.Update();
    }

    #endregion UnityMethods


    #region Public Variables
    //Variables accessible from every other script referencing this class.
    //These variables should not be visible in Inspector and should be hidden by using [HideInInspector] or [ReadOnly]

    #endregion Public Variables


    #region Public Methods
    
    public override void Use()
    {
        if (gameInput.CurrentGameInput.Use_Continous)
        {
            waterParticleSystem.Play();

            foreach(var plant in waterCollision.plantsInRange)
            {
                plant.WaterMe(wateringSpeed * Time.deltaTime);
            }
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
}
