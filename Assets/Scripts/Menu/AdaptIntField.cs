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

public class AdaptIntField : MonoBehaviour
{
    [SerializeField]
    private GameObject button;

    [SerializeField]
    private GameObject editText;

    [SerializeField]
    private GameObject infoText;

    [SerializeField]
    private GameObject inputField;

    [SerializeField]
    private int defaultValue = -1;

    [SerializeField]
    private int minValue = 0;

    [SerializeField]
    private bool swapInfoTextOrder = false;

    private int inputValue = -1;
    private string defaultinfoText = "";
    private bool edit = false;
    private bool activated = true;


    // On start, sets the default value if it fits inside the limits and constraints.
    void Start()
    {
        defaultinfoText = infoText.GetComponent<Text>().text;

        if (defaultValue != -1 && defaultValue <= 999)
        {
            if (defaultValue < minValue)
            {
                defaultValue = minValue;
            }
            inputField.GetComponent<InputField>().text = defaultValue.ToString();
            UpdateinputValue();
        }
        FocusInfo();
    }

    // On click, activates the input field and focuses the UI on it.
    public void OnMouseClick()
    {
        button.GetComponent<Button>().interactable = false;
        FocusInput();
        inputField.GetComponent<InputField>().ActivateInputField();
    }

    // On hover enter, activates the "edit" text.
    public void OnMouseEnter()
    {
        if (!activated) return;
        FocusEdit();
    }

    // On hover leave, if the input field is not being edited, activates the information text.
    public void OnMouseLeave()
    {
        if (!activated) return;
        if (!edit)
        {
            FocusInfo();
        }
    }

    // On end of the edition of the input field, checks the input value, activates the information text and notifies any listener that its value has changed.
    public void OnInputEndEdit()
    {
        edit = false;
        UpdateinputValue();
        FocusInfo();
        button.GetComponent<Button>().interactable = true;
    }

    // Updates the input value. Checks if the vaule fits inside the limits and constraints set in the editor and updates the information text accordingly.
    private void UpdateinputValue()
    {
        var inputText = inputField.GetComponent<InputField>().text;
        if (inputText != "")
        {
            var tempValue = int.Parse(inputField.GetComponent<InputField>().text);

            if (tempValue < 0 || tempValue < minValue)
            {
                return;
            }

            inputValue = tempValue;

            if (!swapInfoTextOrder)
            {
                infoText.GetComponent<Text>().text = defaultinfoText + inputValue.ToString();
            }
            else
            {
                infoText.GetComponent<Text>().text = inputValue.ToString() + defaultinfoText;
            }
        }
    }

    // Displays the "edit" text.
    private void FocusEdit()
    {
        inputField.SetActive(false);
        editText.SetActive(true);
        infoText.SetActive(false);
    }

    // Displays the "info" text.
    private void FocusInfo()
    {
        button.SetActive(true);
        inputField.SetActive(false);
        editText.SetActive(false);
        infoText.SetActive(true);
    }

    // Displays the input field.
    private void FocusInput()
    {
        edit = true;
        button.SetActive(false);
        inputField.SetActive(true);
        editText.SetActive(false);
        infoText.SetActive(false);
    }
}