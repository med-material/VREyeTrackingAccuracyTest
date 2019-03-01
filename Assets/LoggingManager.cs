using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoggingManager : MonoBehaviour {

	private Dictionary<string, List<string>> metaCollection;

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

	void Awake () {
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

	public void ToggleStartCalibration() {
		if (!hasLoggedCalibration) {
			startTime = 0f;
			metaCollection["Email"].Add(emailInputField.text);
			
			metaCollection["ParticipantNumber"].Add(participantInputField.text);

			if (showGazeDot.isOn) {
			metaCollection["ShowGazeDot"].Add("On");
			} else {
			metaCollection["ShowGazeDot"].Add("Off");
			}

			if (showGrid.isOn) {
			metaCollection["ShowGrid"].Add("On");
			} else {
			metaCollection["ShowGrid"].Add("Off");			
			}

			if (spacebarInteractToggle.isOn) {
			metaCollection["SpacebarInteract"].Add("On");
			} else {
			metaCollection["SpacebarInteract"].Add("Off");
			}

			if (string.IsNullOrEmpty(customConditionInputField.text)) {
				metaCollection["CustomCondition"].Add("No Condition");				
			} else {
				metaCollection["CustomCondition"].Add(customConditionInputField.text);
			}

			hasLoggedCalibration = true;
		
		}
	}

	public void DuplicateMetaColumns() {
		logCollection["Email"].Add(metaCollection["Email"][0]);
		logCollection["ParticipantNumber"].Add(metaCollection["ParticipantNumber"][0]);
		logCollection["ShowGazeDot"].Add(metaCollection["ShowGazeDot"][0]);
		logCollection["ShowGrid"].Add(metaCollection["ShowGrid"][0]);
		logCollection["SpacebarInteract"].Add(metaCollection["SpacebarInteract"][0]);
		logCollection["CustomCondition"].Add(metaCollection["CustomCondition"][0]);	
	}

	public void WriteToLog(string varName, string varValue) {
		logCollection[varName].Add(varValue);

	}

	// TODO: Write to log file on disk as backup

	public void UploadLogs() {
		mySQL.AddToUploadQueue(logCollection);
		mySQL.UploadNow();
		foreach (string key in logCollection.Keys) {
			Debug.Log("Key: " + key + ", Count: " + logCollection[key].Count.ToString());
			logCollection[key].Clear();
		}
	}
}
