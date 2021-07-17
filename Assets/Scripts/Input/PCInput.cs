using UnityEngine;

public class PCInput : IGameInput
{
    public float Movement_X_Axis => Input.GetAxisRaw("Horizontal");

    public float Movement_Y_Axis => Input.GetAxisRaw("Vertical");

    public float LookAround_X_Axis => throw new System.NotImplementedException();

    public float LookAround_Y_Axis => throw new System.NotImplementedException();

    public bool Use => Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0);
}
