using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRInput : IGameInput
{
    public float Movement_X_Axis => throw new System.NotImplementedException();

    public float Movement_Y_Axis => throw new System.NotImplementedException();

    public float LookAround_X_Axis => throw new System.NotImplementedException();

    public float LookAround_Y_Axis => throw new System.NotImplementedException();

    public bool Use_Single => OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger);

    public bool Use_Continous => OVRInput.Get(OVRInput.RawButton.RIndexTrigger);

    public bool ChangeTool => OVRInput.GetDown(OVRInput.RawButton.RHandTrigger);

    public bool UseDetailer => OVRInput.Get(OVRInput.RawButton.LIndexTrigger);

    public float ToolHorizontalAxis
    {
        get
        {
            if (Mathf.Abs(OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).x) > 0.1f)
            {
                return OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).x;
            }
            else return 0f;
        }
    }

    public float ToolVerticalAxis
    {
        get
        {
            if (Mathf.Abs(OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).y) > 0.9f)
            {
                return OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).y;
            }
            else return 0f;
        }
    }

    public bool saveViewpoint => OVRInput.Get(OVRInput.RawButton.X);

    public bool goToViewpoint => OVRInput.Get(OVRInput.RawButton.Y);

    public bool useMenuNavigator => OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger);
}
