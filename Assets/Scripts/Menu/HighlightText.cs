//
// Copyright: Aalborg University
// Author: YoNeXia <yohann.neraud@viacesi.fr> (https://gitlab.com/YoNeXia)
// Git: https://github.com/med-material/VREyeTrackingAccuracyTest
//
// Date: 10/7/2019
//
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HighlightText : MonoBehaviour
{

    // Set variables
    private InputField inputField;
    private Text text;

    // Use this for initialization
    void Start()
    {
        // Get components
        inputField = GetComponent<InputField>();
        text = GetComponentInChildren<Text>();
    }

    // When mouse enter in the Input Field
    public void MouseInInputField()
    {
        // Set text in iput Field
        text.text = "Edit";
    }

    // When mouse exit the Input Field
    public void MouseOutInputField()
    {
        switch (inputField.name)
        {
            case "email":
                text.text = "Enter an email.";
                break;
            case "user":
                text.text = "Enter an user ID.";
                break;
            case "test":
                text.text = "Enter a test number.";
                break;
            case "comment":
                text.text = "Enter a comment if necessary.";
                break;
            default:
                text.text = "";
                break;
        }
    }
}
