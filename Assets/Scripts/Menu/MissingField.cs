using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissingField : MonoBehaviour
{

    private Text missingField;

    // Use this for initialization
    void Start()
    {
        missingField = GetComponent<Text>();
    }

    public void ChangeTextValue(int field)
    {
        switch (field)
        {
            case 1:
                missingField.text = "Warning, mail field is empty!";
                break;
            case 2:
                missingField.text = "Warning, user ID field is empty!";
                break;
            case 3:
                missingField.text = "Warning, test number field is empty!";
                break;
            default:
                break;

        }

    }
}
