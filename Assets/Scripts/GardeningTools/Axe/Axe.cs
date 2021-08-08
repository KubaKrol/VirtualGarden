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

    private void Start()
    {
        cachedPos = transform.position;
    }

    public override void Update()
    {
        base.Update();
        CalculateSpeed();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Plant"))
        {
            var plant = other.gameObject.GetComponentInParent<Plant>();

            if (plant != null)
            {
                if(Speed > 0.04f)
                {
                    plant.CutMeDown();
                }
            }
                
        }
    }

    private void OnTriggerExit(Collider other)
    {
        /*if (other.gameObject.layer == LayerMask.NameToLayer("Plant"))
        {
            var plant = other.gameObject.GetComponentInParent<Plant>();

            if (plant != null)
            {

            }
                
        }*/
    }

    #endregion UnityMethods


    #region Public Variables

    public float Speed { get; private set; }

    #endregion Public Variables


    #region Public Methods

    public override void Use()
    {
        //nothing
    }

    public override void PickUp()
    {
        base.PickUp();
    }

    #endregion Public Methods


    #region Private Variables

    private Vector3 cachedPos;
   

    #endregion Private Variables


    #region Private Methods
    
    private void CalculateSpeed()
    {
        var currentPos = transform.position;
        Speed = Vector3.Distance(currentPos, cachedPos);
        cachedPos = transform.position;
    }

    #endregion Private Methods


    #region Coroutines
    //IEnumerators<>


    #endregion Coroutines


    #region Public Types
    //enums, structs etc...


    #endregion Public Types
}
