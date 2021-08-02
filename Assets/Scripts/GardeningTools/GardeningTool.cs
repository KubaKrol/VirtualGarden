using Sirenix.OdinInspector;
using UnityEngine;

public class GardeningTool : MonoBehaviour
{
    #region Inspector Variables

    [Title("Gardening Tool Dependencies")]

    [SerializeField] protected GameInput gameInput;

    #endregion Inspector Variables


    #region Unity Methods

    public virtual void Update()
    {
        if (CurrentState == EGardeningToolState.BeingUsed)
        {
            Use();
        }
    }

    #endregion UnityMethods


    #region Public Variables

    public EGardeningToolState CurrentState { get; private set; }

    #endregion Public Variables


    #region Public Methods

    public virtual void PickUp()
    {
        gameObject.SetActive(true);
        CurrentState = EGardeningToolState.BeingUsed;
    }

    public virtual void PutDown()
    {
        CurrentState = EGardeningToolState.Idle;
        gameObject.SetActive(false);
    }

    public virtual void Use()
    {
        //To override
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
