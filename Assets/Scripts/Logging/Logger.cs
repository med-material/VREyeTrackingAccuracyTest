using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Text.RegularExpressions;

public class Logger
{
    #region Fields
    private static Logger _instance;

    public string userID = StartConfig.userID;
    public string FolderName = DateTime.Now.ToString("MM-dd-yy");
    public string FileName = DateTime.Now.ToString("hh-mm-ss"); // + LoggerBehavior.sceneName;

    private List<string> nonUsableChar = new List<string>() { "\\", "/", ":", "*", "?", "<", ">", "|" };

    #endregion

    #region Properties

    void Start()
    {
        //if there is no forbidden char save the userID
        if (nonUsableChar.Any(this.userID.Contains))
        {
            foreach (var c in nonUsableChar)
            { this.userID = Regex.Replace(this.userID, c, "-"); }
        }
    }

    /// <summary>
    /// path of the log file's folder
    /// </summary>
    /// <value>folder path</value>
    public string FullPathLogDir
    {
        get { return AppConstants.DefaultEyeTrackingFolder; }
    }

    /// <summary>
    /// Path and name of the log file
    /// </summary>
    /// <value>Path and name of the log file</value>
    public string FullPathLogFile
    {
        get { return AppConstants.DefaultEyeTrackingFolder + "\\" + FolderName + "\\" + FileName + "_id-" + userID + ".csv"; }
    }

    /// <summary>
    /// Singleton of the log file
    /// </summary>
    /// <value>current log instance or a new one if there is no instance</value>
    public static Logger Instance
    {
        get
        {
            if (_instance == null) Create();
            return _instance;
        }
    }

    #endregion

    #region Constructor(s)

    public Logger()
    {
        InitDefaultFolder();
        InitSubjectFolder();
        InitSubjectLogFile();
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// create a new log instance
    /// </summary>
    private static void Create()
    {
        _instance = new Logger();
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// write logs
    /// </summary>
    /// <param name="data">logs data</param>
    public void Log(object[] data)
    {
        using (var writer = new CsvWriter(FullPathLogFile))
        {
            foreach (var o in data)
            {
                writer.Write(o);
            }
        }
    }

    #endregion

    #region Init Methods

    private void InitDefaultFolder()
    {
        if (!Directory.Exists(AppConstants.DefaultEyeTrackingFolder))
            Directory.CreateDirectory(AppConstants.DefaultEyeTrackingFolder);
    }

    private void InitSubjectFolder()
    {
        if (!Directory.Exists(AppConstants.DefaultEyeTrackingFolder + "\\" + FolderName))
            Directory.CreateDirectory(AppConstants.DefaultEyeTrackingFolder + "\\" + FolderName);
    }

    private void InitSubjectLogFile()
    {
        if (!File.Exists(FullPathLogFile))
            File.Create(FullPathLogFile).Dispose();

    }
    #endregion
}
