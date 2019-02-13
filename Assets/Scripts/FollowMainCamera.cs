using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMainCamera : MonoBehaviour {

	public GameObject followObject;
	
	/// <summary>
	/// follow the position and rotation of an given GO
	/// </summary>
	void Update () {
		gameObject.transform.rotation = followObject.transform.rotation;
		gameObject.transform.position = followObject.transform.position;
	}
}
