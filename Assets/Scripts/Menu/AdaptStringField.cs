//
// Copyright: Aalborg University
// Author: YoNeXia <yohann.neraud@viacesi.fr> (https://gitlab.com/YoNeXia)
// Git: https://github.com/med-material/VREyeTrackingAccuracyTest
//
// Date: 10/25/2019
//
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class AdaptStringField : MonoBehaviour {

    // Set variables
    [SerializeField]
    private InputField inputField;

    [SerializeField]
    private Text text_edit;

    [SerializeField]
    private Text errorField;

    // When mouse enter in the Input Field
    public void MouseInInputField()
    {
        // Set text in iput Field
        text_edit.text = "Edit";
    }

    // When mouse exit the Input Field
    public void MouseOutInputField()
    {
        switch (inputField.name)
        {
            case "email":
                text_edit.text = "example@example.com";
                break;
            case "comment":
                text_edit.text = "No comment";
                break;
            default:
                text_edit.text = "";
                break;
        }
    }

    public void EmailIsValid()
    {
        if (!Regex.IsMatch(inputField.text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
        {
            PrintError(true, 4);
            return;
        }
        PrintError(false, 0);
        return;
    }

    private void PrintError(bool error, int errorID)
    {
        if (error) inputField.textComponent.color = new Color(1f, 0f, 0f, 1f);
        else inputField.textComponent.color = new Color(0.196f, 0.196f, 0.196f, 1f);
        errorField.GetComponent<ErrorField>().ChangeTextValue(errorID);
    }
}
