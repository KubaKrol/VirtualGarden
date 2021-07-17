using Sirenix.OdinInspector;
using UnityEngine;

public class GardeningTool : MonoBehaviour
{
    #region Inspector Variables

    [Title("Gardening Tool Dependencies")]

    [SerializeField] protected GameInput gameInput;

    #endregion Inspector Variables


    #region Unity Methods
    //Methods from MonoBehaviour.
    //OnEnable(), OnDisable(), Awake(), Start(), Update() etc...

    #endregion UnityMethods


    #region Public Variables
    
    public EGardeningToolState CurrentState { get; private set; }

    #endregion Public Variables


    #region Public Methods

    public void PickUp()
    {
        CurrentState = EGardeningToolState.BeingUsed;
    }

    public void PutDown()
    {
        CurrentState = EGardeningToolState.Idle;
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
