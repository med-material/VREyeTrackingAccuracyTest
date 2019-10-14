//
// Copyright: Aalborg University
// Author: YoNeXia <yohann.neraud@viacesi.fr> (https://gitlab.com/YoNeXia)
// Git: https://github.com/med-material/VREyeTrackingAccuracyTest
//
// Date: 9/27/2019
//
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HighlightText : MonoBehaviour
{

    // Set variables
    private Text text;

    // Use this for initialization
    void Start()
    {
        // Get component
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
        // Set text in iput Field
        text.text = "";
    }
}