using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// update the confidence of the right eye text
/// </summary>
public class RightConfidence : MonoBehaviour
{
	//Update current confidence of the right eye text
    void Update()
    {
        this.gameObject.GetComponent<Text>().text = "Right confidence\n" + System.Math.Round(PupilLabData.confidence0 * 100) + "%";
    }
}
