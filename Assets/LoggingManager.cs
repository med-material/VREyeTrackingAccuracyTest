using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoggingManager : MonoBehaviour {

	private Dictionary<string, List<string>> logCollection;

	private ConnectToMySQL mySQL;

	[SerializeField]
	private InputField emailInputField;

	[SerializeField]
	private InputField participantInputField;

	[SerializeField]
	private InputField customConditionInputField;

	[SerializeField]
	private Toggle spacebarInteractToggle;

	[SerializeField]
	private Toggle showGrid;

	[SerializeField]
	private Toggle showGazeDot;

	private bool hasLoggedCalibration = false;

	private float startTime = 0f;

	private bool calibrationBegun = false;

	void Awake () {
		logCollection = new Dictionary<string, List<string>>();

		// Add the database columns
		logCollection.Add("Email", new List<string>());
		logCollection.Add("DateAdded", new List<string>());
		logCollection.Add("ParticipantNumber", new List<string>());
		logCollection.Add("CircleName", new List<string>());
		logCollection.Add("EyeTrackAccuracy", new List<string>());
		logCollection.Add("TimeToShrinkCircle", new List<string>());
		logCollection.Add("CustomCondition", new List<string>());
		logCollection.Add("TotalTestTime", new List<string>());
		logCollection.Add("ShowGazeDot", new List<string>());
		logCollection.Add("ShowGrid", new List<string>());
		logCollection.Add("SpacebarInteract", new List<string>());

		mySQL = gameObject.GetComponent<ConnectToMySQL>();
		
		DontDestroyOnLoad(this.gameObject);
	}

	public void ToggleStartCalibration() {
		if (!hasLoggedCalibration) {
			startTime = 0f;
			logCollection["Email"].Add(emailInputField.text);
			string date = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			logCollection["DateAdded"].Add(date);
			logCollection["ParticipantNumber"].Add(participantInputField.text);

			if (showGazeDot.isOn) {
			logCollection["ShowGazeDot"].Add("On");
			} else {
			logCollection["ShowGazeDot"].Add("Off");
			}

			if (showGrid.isOn) {
			logCollection["ShowGrid"].Add("On");
			} else {
			logCollection["ShowGrid"].Add("Off");			
			}

			if (spacebarInteractToggle.isOn) {
			logCollection["SpacebarInteract"].Add("On");
			} else {
			logCollection["SpacebarInteract"].Add("Off");
			}

			logCollection["CustomCondition"].Add(customConditionInputField.text);
			hasLoggedCalibration = true;

			// Fake Values
			logCollection["CircleName"].Add("Test");
			logCollection["EyeTrackAccuracy"].Add("0");
			logCollection["TimeToShrinkCircle"].Add("0"); 
			logCollection["TotalTestTime"].Add("0");

		}
	}

	void Update() {
		if (calibrationBegun) {
			startTime += Time.deltaTime;
		}
	}



	public void WriteToLog(string varName, string varValue) {
		logCollection[varName].Add(varValue);

	}

	// TODO: Write to log file on disk as backup

	public void UploadLogs() {
		mySQL.AddToUploadQueue(logCollection);
		mySQL.UploadNow();
		foreach (string key in logCollection.Keys) {
			logCollection[key].Clear();
		}
	}
}
