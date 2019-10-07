//
// Copyright: Aalborg University
// Author: YoNeXia <yohann.neraud@viacesi.fr> (https://gitlab.com/YoNeXia)
// Git: https://github.com/med-material/VREyeTrackingAccuracyTest
//
// Date: 10/7/2019
//
using System.Collections.Generic;
using System.Text.RegularExpressions;
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
    private Dropdown switchInputDropdown;

    [SerializeField]
    private Text errorField;

    /*[SerializeField]
    private Toggle spacebarInteractToggle;

    [SerializeField]
    private Toggle showGrid;

    [SerializeField]
    private Toggle showGazeDot;*/

    private bool hasLoggedCalibration = false;

    private float startTime = 0f;

    [SerializeField]
    private StartConfig startConfig;

    void Awake()
    {
        logCollection = new Dictionary<string, List<string>>();

        // Add the database columns
        logCollection.Add("Email", new List<string>());
        logCollection.Add("DateAdded", new List<string>());
        logCollection.Add("UserID", new List<string>());
        logCollection.Add("CircleName", new List<string>());
        logCollection.Add("EyeTrackAccuracy", new List<string>());
        logCollection.Add("Comment", new List<string>());
        //logCollection.Add("ShowGazeDot", new List<string>());
        //logCollection.Add("ShowGrid", new List<string>());
        logCollection.Add("SpacebarInteract", new List<string>());

        metaCollection = new Dictionary<string, List<string>>();
        metaCollection.Add("Email", new List<string>());
        metaCollection.Add("DateAdded", new List<string>());
        metaCollection.Add("UserID", new List<string>());
        metaCollection.Add("CircleName", new List<string>());
        metaCollection.Add("EyeTrackAccuracy", new List<string>());
        metaCollection.Add("Comment", new List<string>());
        //metaCollection.Add("ShowGazeDot", new List<string>());
        //metaCollection.Add("ShowGrid", new List<string>());
        metaCollection.Add("SpacebarInteract", new List<string>());

        mySQL = gameObject.GetComponent<ConnectToMySQL>();

        DontDestroyOnLoad(this.gameObject);
    }

    public void ToggleStartCalibration()
    {
        if (!CheckEmptyValue(emailInputField.text, userInputField.text, testInputField.text))
        {
            return;
        }
        if (!EmailIsValid(emailInputField.text))
        {
            return;
        }


        if (!hasLoggedCalibration)
        {
            startTime = 0f;
            metaCollection["Email"].Add(emailInputField.text);

            metaCollection["UserID"].Add(userInputField.text);

            /*if (showGazeDot.isOn)
            {
                metaCollection["ShowGazeDot"].Add("On");
            }
            else
            {
                metaCollection["ShowGazeDot"].Add("Off");
            }

            if (showGrid.isOn)
            {
                metaCollection["ShowGrid"].Add("On");
            }
            else
            {
                metaCollection["ShowGrid"].Add("Off");
            }

            if (spacebarInteractToggle.isOn)
            {
                metaCollection["SpacebarInteract"].Add("On");
            }
            else
            {
                metaCollection["SpacebarInteract"].Add("Off");
            }
            */
            if (string.IsNullOrEmpty(commentInputField.text))
            {
                metaCollection["Comment"].Add("No Condition");
            }
            else
            {
                metaCollection["Comment"].Add(commentInputField.text);
            }

            hasLoggedCalibration = true;

        }
        startConfig.startCalibration();
    }

    public void DuplicateMetaColumns()
    {
        logCollection["Email"].Add(metaCollection["Email"][0]);
        logCollection["UserID"].Add(metaCollection["UserID"][0]);
        //logCollection["ShowGazeDot"].Add(metaCollection["ShowGazeDot"][0]);
        //logCollection["ShowGrid"].Add(metaCollection["ShowGrid"][0]);
        logCollection["SpacebarInteract"].Add(metaCollection["SpacebarInteract"][0]);
        logCollection["Comment"].Add(metaCollection["Comment"][0]);
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
            Debug.Log(userID);
            return false;
        }
        if (string.IsNullOrEmpty(testNumber))
        {
            errorField.GetComponent<ErrorField>().ChangeTextValue(3);
            Debug.Log(testNumber);
            return false;
        }
        return true;
    }

    private bool EmailIsValid(string email)
    {
        if (!Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
        {
            errorField.GetComponent<ErrorField>().ChangeTextValue(4);
            return false;
        }
        errorField.GetComponent<ErrorField>().ChangeTextValue(0);
        return true;
    }
}
