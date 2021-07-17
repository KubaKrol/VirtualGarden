﻿public interface IGameInput
{
    float Movement_X_Axis { get; }
    float Movement_Y_Axis { get; }
    float LookAround_X_Axis { get; }
    float LookAround_Y_Axis { get; }
    bool Use { get; }
}
