using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// update the confidence of the left eye text
/// </summary>
public class LeftConfidence : MonoBehaviour
{
	//Update current confidence of the left eye text    
    void Update()
    {
        this.gameObject.GetComponent<Text>().text = "Left confidence\n" + System.Math.Round(PupilLabData.confidence1 * 100) + "%";
    }
}
