using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ConfidenceBar : MonoBehaviour
{
    private Slider slider;
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
    }
    void Update()
    {
        //change the sliders value
        slider.value = ConfidenceAverage();
        //check if the value is under the 60% limit
        CheckLimit();
    }

	/// <summary>
	/// average of last 50 confidence value, last 10sec because of 1 value every 0,2sec
	/// </summary>
	/// <returns>float</returns>
    private float ConfidenceAverage()
    {
        float result = 0;
        for (int i = 0; i < 50; i++)
        {
            result += PupilLabData.confArray[i];
        }
        return result / 50;
    }

	/// <summary>
	/// if the confidence value is under 0,6 (60%) the filler is colored in red, else green
	/// </summary>
    private void CheckLimit()
    {
        if (slider.value < 0.6)
            transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().color = Color.red;
        else
            transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().color = Color.green;
    }
}
