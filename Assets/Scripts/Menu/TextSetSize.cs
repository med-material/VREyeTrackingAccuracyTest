using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextSetSize : MonoBehaviour
{

    [SerializeField]
    private Text line_1, line_2;

    void Update()
    {
        if (!line_1)
        {
            Debug.Log("No first line enter.");
            return;
        }
        line_2.fontSize = line_1.cachedTextGenerator.fontSizeUsedForBestFit;
        return;
    }
}
