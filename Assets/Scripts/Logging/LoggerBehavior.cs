// originally created by Theo and Kiefer (French interns at AAU Fall 2017) 
// modified by Bianca

// outcommented parts doesn't work in Pupil Labs Plugin, but is used in another project

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;

public class LoggerBehavior : MonoBehaviour
{

    private static Logger _logger;
    private static List<object> _toLog;

    private LoggingManager loggingManager; // this logging manager facilitates uploading to DB.

    private Vector3 gazeToWorld;
    private static string CSVheader = AppConstants.CsvFirstRow;
    private static string CSVuserConfig = AppConstants.CSVUserConfigRow;

    //CircleTruc log var
    private GameObject circleObject;
    private string gazePosx, gazePosy;
    private double circleXpos, circleYpos;
    private float TTFF;
    public static float sceneTimer = 0;
    public static string sceneName = "";
    private float timer;

    //private Camera dedicatedCapture;


    #region Unity Methods

    private void Start()
    {
        _toLog = new List<object>();
        StartConfigLog();
    }

    private void Update()
    {
        //check if there is 1 circle from the accuracy test displayed on the grid
        if (SpawnCircle.targetCircle.Count == 1)
        {
            gazePosx = (GameController.gazePosition.x).ToString("F2");
            gazePosy = (GameController.gazePosition.y).ToString("F2");
            circleXpos = circleObject != null ? Math.Round(circleObject.transform.localPosition.x, 3) : double.NaN;
            circleYpos = circleObject != null ? Math.Round(circleObject.transform.localPosition.y, 3) : double.NaN;
            CircleInfo();
        }
        //DoLog();
        //AddToLog();
        timer += Time.deltaTime;
    }

    /// <summary>
    /// get the data from the targets from the accuracy test scene
    /// </summary>
    private void CircleInfo()
    {
        if (circleObject != SpawnCircle.targetCircle.First())
            circleObject = SpawnCircle.targetCircle.First();
        if (!circleObject.GetComponent<CircleLife>().isTTFF)
            TTFF = circleObject.GetComponent<CircleLife>().TTFF;
        else
            TTFF = 0;
    }

    private void AddToLog()
    {
        if (PupilData._2D.GazePosition != Vector2.zero)
        {
            gazeToWorld = Camera.main.ViewportToWorldPoint(new Vector3
            (PupilData._2D.GazePosition.x, PupilData._2D.GazePosition.y,
            Camera.main.nearClipPlane));
        }

        //all the datas that will be saved in the log file
        //1 variable = 1 column
        var tmp = new
        {
            // default variables for all scenes
            a = Math.Round(timer, 3),
            //check if we are in accuracy test or fov calibration scene
            sname = sceneName != "" ? sceneName : "No test scene",
            //scene timer
            stimer = Math.Round(sceneTimer, 3) != 0 ? Math.Round(sceneTimer, 3) : double.NaN,
            //frames per second during the last frame, could calculate an average frame rate instead
            fps = (int)(1.0f / Time.deltaTime),

            //targets position from the accuracy test scene
            logcircleXpos = circleObject != null ? Math.Round(circleObject.transform.localPosition.x, 3) : double.NaN,
            logcircleYpos = circleObject != null ? Math.Round(circleObject.transform.localPosition.y, 3) : double.NaN,

            //gaze position on X and Y
            j = PupilData._2D.GazePosition != Vector2.zero ? Math.Round(PupilData._2D.GazePosition.x, 3) : double.NaN,
            k = PupilData._2D.GazePosition != Vector2.zero ? Math.Round(PupilData._2D.GazePosition.y, 3) : double.NaN,
            // l = PupilData._2D.GazePosition != Vector2.zero ? Math.Round(gazeToWorld.x, 3) : double.NaN,
            // m = PupilData._2D.GazePosition != Vector2.zero ? Math.Round(gazeToWorld.y, 3) : double.NaN,

            //gaze position on the grid of the accuracy test
            lbis = gazePosx,
            mbis = gazePosy,

            //Confidence from left / right eye and the average of both
            // n = PupilData._2D.GazePosition != Vector2.zero ? Math.Round(PupilTools.FloatFromDictionary(PupilTools.gazeDictionary, "confidence"), 3) : double.NaN, // confidence value calculated after calibration 
            confLeft = PupilData._2D.GazePosition != Vector2.zero ? Math.Round(PupilLabData.confidence1, 3) : double.NaN, // confidence value calculated after calibration 
            confRight = PupilData._2D.GazePosition != Vector2.zero ? Math.Round(PupilLabData.confidence0, 3) : double.NaN, // confidence value calculated after calibration 
            confGaze = PupilData._2D.GazePosition != Vector2.zero ? Math.Round((PupilLabData.confidence0 + PupilLabData.confidence1) / 2, 3) : double.NaN,

            //target size in the accuracy test scene, can be translated to the offset from the center of the scene
            circleSize = circleObject != null ? Math.Round(circleObject.transform.localScale.x, 3) : double.NaN,
            offsetX = Math.Round(Convert.ToSingle(gazePosx) - circleXpos, 3),
            offsetY = Math.Round(Convert.ToSingle(gazePosy) - circleYpos, 3),
            //TTFF of the targets in the accuracy test scene
            TimeToFirstFix = TTFF != 0 ? Math.Round(TTFF, 3) : double.NaN
        };
        _toLog.Add(tmp);
    }

    private Vector3 CalculEyeGazeOnObject(RaycastHit hit)
    {
        return hit.transform.InverseTransformPoint(hit.point);
    }

    /// <summary>
    /// create the log .csv file and save the logs
    /// </summary>
    private void DoLog()
    {
        CSVheader = AppConstants.CsvFirstRow;
        _logger = Logger.Instance;
        if (_toLog.Count == 0)
        {
            var firstRow = new { CSVheader };
            _toLog.Add(firstRow);
        }
        _logger.Log(_toLog.ToArray());
        _toLog.Clear();
    }

    /// <summary>
    /// logging startconfig info
    /// </summary>
    private void StartConfigLog()
    {
        CSVuserConfig = AppConstants.CSVUserConfigRow;
        _logger = Logger.Instance;

        var ucFirstRown = new { CSVuserConfig };
        _toLog.Add(ucFirstRown);
        _logger.Log(_toLog.ToArray());
        _toLog.Clear();

        _logger = Logger.Instance;
        var userConfig = new
        {
            userID = _logger.userID,
            dateTime = _logger.FolderName.Replace('-', '/') + " - " + _logger.FileName.Replace('-', ':'),
            makeup = StartConfig.makeUp ? "Yes" : "No",
            glasses = StartConfig.glasses ? "Yes" : "No",
            gazeDot = StartConfig.gazeDot ? "Yes" : "No",
            grid = StartConfig.grid ? "Yes" : "No",
            inputMode = StartConfig.inputMode ? "Yes" : "No",
            targetLifespan = StartConfig.targetLifeSpan
        };
        _toLog.Add(userConfig);
        _logger.Log(_toLog.ToArray());
        _toLog.Clear();
    }
    #endregion
}