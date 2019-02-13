using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class AimCalculator : MonoBehaviour
{
    private Vector3[] lastGazeMarkerPos = new Vector3[10];
    private Vector3 accuracyPoint = new Vector3(0, 0, -0.2f);

    private float xScale, yScale;
    private float midX, midY;

    // Update is called once per frame
    void Update()
    {
        //while there is less than 10 Vector3 position disable the dispersion image and return (quit update method)
        if (GazeMarker.gazePath.Count <= 10)
        {
            gameObject.GetComponent<Image>().enabled = false;
            return;
        }
        //update the gaze dispersion and the square
        UpdateAccuracy();
        updateDispersionArea();
    }

    /// <summary>
    /// save the last 10 gazemarkers position
    /// and save the center of the pack of gaze markers
    /// </summary>
    private void UpdateAccuracy()
    {
        gameObject.GetComponent<Image>().enabled = true;

        for (int i = 0; i < 10; i++)
        {
            lastGazeMarkerPos[i] = GazeMarker.gazePath[(GazeMarker.gazePath.Count - 10) + i];
        }

        accuracyPoint.x = lastGazeMarkerPos.Average(item => item.x);
        accuracyPoint.y = lastGazeMarkerPos.Average(item => item.y);
        accuracyPoint.z = 0;
    }

    /// <summary>
    /// Get max and min X /m Y of the 10 last gaze position for the center
    /// scale and position of the dispersion square updated
    /// </summary>
    private void updateDispersionArea()
    {
        midX = Mathf.Abs(accuracyPoint.x);

        midY = Mathf.Abs(accuracyPoint.y);

        var maxX = Mathf.Abs(lastGazeMarkerPos.Max(item => item.x));

        var minX = Mathf.Abs(lastGazeMarkerPos.Min(item => item.x));

        xScale = maxX > minX ? maxX - midX : minX - midX;

        var maxY = Mathf.Abs(lastGazeMarkerPos.Max(item => item.y));

        var minY = Mathf.Abs(lastGazeMarkerPos.Min(item => item.y));

        yScale = maxY > minY ? maxY - midY : minY - midY;

        gameObject.transform.localScale = new Vector3(xScale, yScale, 0.1f);
        gameObject.transform.localPosition = accuracyPoint;
    }
}
