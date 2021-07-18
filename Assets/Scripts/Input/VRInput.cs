using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRInput : IGameInput
{
    public float Movement_X_Axis => throw new System.NotImplementedException();

    public float Movement_Y_Axis => throw new System.NotImplementedException();

    public float LookAround_X_Axis => throw new System.NotImplementedException();

    public float LookAround_Y_Axis => throw new System.NotImplementedException();

    public bool Use => OVRInput.GetDown(OVRInput.RawButton.A);
}
