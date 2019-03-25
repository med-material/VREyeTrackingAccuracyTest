using System;

public static class AppConstants
{

    #region General Constants

    //log file folder name, in "MyDocuments" folder
    public static string DefaultEyeTrackingFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\_GazeData";
    #endregion

    // not used
    #region OSCSystem constants
    public const string CalibrationCubeTag = "CCube";
    public const float CubeScaleRate = 0.75f;
    public const float CubeDistance = 2.0f;
    public const float CubeDepth = 0.1f;
    public const float TimeOutBeforeSizeUp = 1000f;
    public const int GridHeight = 3;
    public const int GridWidth = 3;
    #endregion

    #region LoggerBehavior Constants

    //First row, name of the columns, need to be in the same order as the tmp var in AddToLog in LoggerBehavior script
    public const string CSVUserConfigRow = "UserID;Date - Time;Wearing make-up;Wearing glasses;gaze dot displayed;grid displayed;using input;target lifespan (ms);";

    public const string CsvFirstRow = "Session Time (s);SystemDateTime;Scene Name;Scene Timer (s);framerate (fps);circlename;circle_pos_x;circle_pos_y;" +
     "pupilData_gaze_x;pupilData_gaze_y;gaze_on_grid_x;gaze_on_grid_y;Left_eye_conf;Right_eye_conf;gaze_confidence;" +
     "circle_radius;accuracyCalc;Offset X;Offset Y;Time To First Fix (s);";

    #endregion
}
