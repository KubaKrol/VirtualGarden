public interface IGameInput
{
    float Movement_X_Axis { get; }
    float Movement_Y_Axis { get; }
    float LookAround_X_Axis { get; }
    float LookAround_Y_Axis { get; }
    bool Use_Single { get; }
    bool Use_Continous { get; }
    bool ChangeTool { get;  }
    bool UseDetailer { get; }
    float ToolHorizontalAxis { get; }
    float ToolVerticalAxis { get; }

    bool saveViewpoint { get; }
    bool goToViewpoint { get;  }

    bool useMenuNavigator { get; }
}
