using UnityEngine;

public class PCInput : IGameInput
{
    public float Movement_X_Axis => Input.GetAxisRaw("Horizontal");

    public float Movement_Y_Axis => Input.GetAxisRaw("Vertical");

    public float LookAround_X_Axis => throw new System.NotImplementedException();

    public float LookAround_Y_Axis => throw new System.NotImplementedException();

    public bool Use_Single => Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0);

    public bool Use_Continous => Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Return) || Input.GetMouseButton(0);

    public bool ChangeTool => Input.GetKeyDown(KeyCode.Q);

    public bool UseDetailer => Input.GetKey(KeyCode.R);

    public float ToolHorizontalAxis
    {
        get
        {
            if (Input.GetKey(KeyCode.Keypad6))
                return 1f;
            if (Input.GetKey(KeyCode.Keypad4))
                return -1f;

            return 0f;
        }
    }

    public float ToolVerticalAxis
    {
        get
        {
            if (Input.GetKey(KeyCode.Keypad8))
                return 1f;
            if (Input.GetKey(KeyCode.Keypad2))
                return -1f;

            return 0f;
        }
    }
}
