using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Inspector Variables

    [Title("Dependencies")]

    [SerializeField] private GameInput gameInput;
    [SerializeField] private PlayerCamera playerCamera;

    #endregion Inspector Variables


    #region Unity Methods

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        var movementDirection = Vector3.zero;

        if (gameInput.CurrentGameInput.Movement_Y_Axis > 0f)
            movementDirection += playerCamera.transform.forward;

        if (gameInput.CurrentGameInput.Movement_X_Axis < 0f)
            movementDirection += -playerCamera.transform.right;

        if (gameInput.CurrentGameInput.Movement_X_Axis > 0f)
            movementDirection += playerCamera.transform.right;

        if (gameInput.CurrentGameInput.Movement_Y_Axis < 0f)
            movementDirection += -playerCamera.transform.forward;

        movementDirection.y = 0f;
        if(movementDirection != Vector3.zero)
            Move(movementDirection);
    }

    #endregion UnityMethods


    #region Public Variables
    //Variables accessible from every other script referencing this class.
    //These variables should not be visible in Inspector and should be hidden by using [HideInInspector] or [ReadOnly]

    #endregion Public Variables


    #region Public Methods

    public void Move(Vector3 direction)
    {
        myRigidBody.velocity = new Vector3(direction.normalized.x * 300f * Time.deltaTime, myRigidBody.velocity.y, direction.normalized.z * 300f * Time.deltaTime);
    }

    public void RotateInDirection(Vector3 direction)
    {
        direction.y = 0f;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = rotation;
    }

    #endregion Public Methods


    #region Private Variables

    private Rigidbody myRigidBody;

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
