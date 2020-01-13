﻿//
// Copyright: Aalborg University
// Author: YoNeXia <yohann.neraud@viacesi.fr> (https://gitlab.com/YoNeXia)
// Git: https://github.com/med-material/VREyeTrackingAccuracyTest
//
// Date: 10/7/2019
//
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoggingManager : MonoBehaviour
{

    private Dictionary<string, List<string>> metaCollection;

    private Dictionary<string, List<string>> logCollection;

    private ConnectToMySQL mySQL;

    [SerializeField]
    private InputField emailInputField;

    [SerializeField]
    private InputField userInputField;

    [SerializeField]
    private InputField testInputField;

    [SerializeField]
    private InputField commentInputField;

    [SerializeField]
    private Text errorField;

    private bool spacebarInteract = true, showGrid = true, showGazeDot = true, hasLoggedCalibration = false;

    private float startTime = 0f;

    [SerializeField]
    private StartConfig startConfig;

    void Awake()
    {        
        logCollection = new Dictionary<string, List<string>>();

        // Add the database columns
        logCollection.Add("Email", new List<string>());
        logCollection.Add("DateAdded", new List<string>());
        logCollection.Add("ParticipantNumber", new List<string>());
        logCollection.Add("CircleName", new List<string>());
        logCollection.Add("EyeTrackAccuracy", new List<string>());
        logCollection.Add("CustomCondition", new List<string>());
        logCollection.Add("ShowGazeDot", new List<string>());
        logCollection.Add("ShowGrid", new List<string>());
        logCollection.Add("SpacebarInteract", new List<string>());

        metaCollection = new Dictionary<string, List<string>>();
        metaCollection.Add("Email", new List<string>());
        metaCollection.Add("DateAdded", new List<string>());
        metaCollection.Add("ParticipantNumber", new List<string>());
        metaCollection.Add("CircleName", new List<string>());
        metaCollection.Add("EyeTrackAccuracy", new List<string>());
        metaCollection.Add("CustomCondition", new List<string>());
        metaCollection.Add("ShowGazeDot", new List<string>());
        metaCollection.Add("ShowGrid", new List<string>());
        metaCollection.Add("SpacebarInteract", new List<string>());

        mySQL = gameObject.GetComponent<ConnectToMySQL>();

        DontDestroyOnLoad(this.gameObject);
    }

    public void ToggleStartCalibration()
    {
        if (!CheckEmptyValue(emailInputField.text, userInputField.text, testInputField.text)) return;
        if (errorField.text != "") return;

        startConfig.startCalibration();

        showGrid = StartConfig.grid;
        spacebarInteract = StartConfig.inputMode;

        if (!hasLoggedCalibration)
        {
            startTime = 0f;
            metaCollection["Email"].Add(emailInputField.text);

            metaCollection["ParticipantNumber"].Add(userInputField.text);
            if (showGazeDot)
            {
                metaCollection["ShowGazeDot"].Add("On");
            }
            else
            {
                metaCollection["ShowGazeDot"].Add("Off");
            }

            if (showGrid)
            {
                metaCollection["ShowGrid"].Add("On");
            }
            else
            {
                metaCollection["ShowGrid"].Add("Off");
            }

            if (spacebarInteract)
            {
                metaCollection["SpacebarInteract"].Add("On");
            }
            else
            {
                metaCollection["SpacebarInteract"].Add("Off");
            }
            if (string.IsNullOrEmpty(commentInputField.text))
            {
                metaCollection["CustomCondition"].Add("No Condition");
            }
            else
            {
                metaCollection["CustomCondition"].Add(commentInputField.text);
            }

            hasLoggedCalibration = true;

        }
    }

    public void DuplicateMetaColumns()
    {
        logCollection["Email"].Add(metaCollection["Email"][0]);
        logCollection["ParticipantNumber"].Add(metaCollection["ParticipantNumber"][0]);
        logCollection["ShowGazeDot"].Add(metaCollection["ShowGazeDot"][0]);
        logCollection["ShowGrid"].Add(metaCollection["ShowGrid"][0]);
        logCollection["SpacebarInteract"].Add(metaCollection["SpacebarInteract"][0]);
        logCollection["CustomCondition"].Add(metaCollection["CustomCondition"][0]);
    }

    public void WriteToLog(string varName, string varValue)
    {
        logCollection[varName].Add(varValue);
    }

    // TODO: Write to log file on disk as backup

    public void UploadLogs()
    {
        mySQL.AddToUploadQueue(logCollection);
        mySQL.UploadNow();
        foreach (string key in logCollection.Keys)
        {
            Debug.Log("Key: " + key + ", Count: " + logCollection[key].Count.ToString());
            logCollection[key].Clear();
        }
    }

    private bool CheckEmptyValue(string email, string userID, string testNumber)
    {
        if (string.IsNullOrEmpty(email))
        {
            errorField.GetComponent<ErrorField>().ChangeTextValue(1);
            return false;
        }
        if (string.IsNullOrEmpty(userID))
        {
            errorField.GetComponent<ErrorField>().ChangeTextValue(2);
            return false;
        }
        if (string.IsNullOrEmpty(testNumber))
        {
            errorField.GetComponent<ErrorField>().ChangeTextValue(3);
            return false;
        }
        return true;
    }
}
