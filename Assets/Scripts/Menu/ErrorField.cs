//
// Copyright: Aalborg University
// Author: YoNeXia <yohann.neraud@viacesi.fr> (https://gitlab.com/YoNeXia)
// Git: https://github.com/med-material/VREyeTrackingAccuracyTest
//
// Date: 10/7/2019
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorField : MonoBehaviour
{

    private Text errorField;

    // Use this for initialization
    void Start()
    {
        errorField = GetComponent<Text>();
    }

    public void ChangeTextValue(int field)
    {
        switch (field)
        {
            case 1:
                errorField.text = "Warning, mail field is empty!";
                break;
            case 2:
                errorField.text = "Warning, user ID field is empty!";
                break;
            case 3:
                errorField.text = "Warning, test number field is empty!";
                break;
            case 4:
                errorField.text = "Bad email entered...";
                break;
            case 5:
                errorField.text = "Please enter a Participant number";
                break;
            case 6:
                errorField.text = "Please enter a Test number";
                break;
            default:
                errorField.text = "";
                break;

        }

    }
}
