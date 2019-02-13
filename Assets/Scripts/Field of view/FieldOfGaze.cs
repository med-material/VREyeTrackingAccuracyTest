using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Used on a desktop only camera following a mainCamera 
/// on any scene to show the tracked area
/// </summary>
public class FieldOfGaze : MonoBehaviour
{
    // Different parts GO
    public GameObject upLeft;
    public GameObject upRight;
    public GameObject downLeft;
    public GameObject downRight;

    void Update()
    {
        //disable if the fov calibration scene is loaded
        if (SceneManager.GetSceneByName("Field of view").isLoaded)
            gameObject.GetComponent<Camera>().enabled = false;
        else
        { gameObject.GetComponent<Camera>().enabled = true; }

        //if the upleft part is still at 0.0.0 (means that there is no calibration) every parts are disabled
        if (FovStatic.upLeftPos == new Vector3(0, 0, 0))
        {
            DisableArea();
            return;
        }
        //if upleft part's position is not at 0.0.0 we define the area
        DefineTrackedArea();
    }

    /// <summary>
    /// Disable the 4 parts so the area
    /// </summary>
    void DisableArea()
    {
        upLeft.gameObject.SetActive(false);
        upRight.gameObject.SetActive(false);
        downLeft.gameObject.SetActive(false);
        downRight.gameObject.SetActive(false);
    }

    /// <summary>
    /// define the 4 part's position and draw line to make a square like shape
    /// </summary>
    void DefineTrackedArea()
    {
        //activate the 4 different parts
        upLeft.gameObject.SetActive(true);
        upRight.gameObject.SetActive(true);
        downLeft.gameObject.SetActive(true);
        downRight.gameObject.SetActive(true);

        //give the position from the calibration to the 4 parts
        upLeft.transform.localPosition = FovStatic.upLeftPos;
        upRight.transform.localPosition = FovStatic.upRightPos;
        downLeft.transform.localPosition = FovStatic.downLeftPos;
        downRight.transform.localPosition = FovStatic.downRightPos;

        //Draw lines between all parts to draw a square like shape
        //which is the area where the eyetracking is working

        var ulLine = upLeft.GetComponent<LineRenderer>();
        ulLine.startWidth = 0.008f;
        ulLine.endWidth = 0.008f;
        ulLine.SetPosition(0, upLeft.transform.position);
        ulLine.SetPosition(1, upRight.transform.position);

        var urLine = upRight.GetComponent<LineRenderer>();
        urLine.startWidth = 0.008f;
        urLine.endWidth = 0.008f;
        urLine.SetPosition(0, upRight.transform.position);
        urLine.SetPosition(1, downRight.transform.position);

        var drLine = downRight.GetComponent<LineRenderer>();
        drLine.startWidth = 0.008f;
        drLine.endWidth = 0.008f;
        drLine.SetPosition(0, downRight.transform.position);
        drLine.SetPosition(1, downLeft.transform.position);

        var dlLine = downLeft.GetComponent<LineRenderer>();
        dlLine.startWidth = 0.008f;
        dlLine.endWidth = 0.008f;
        dlLine.SetPosition(0, downLeft.transform.position);
        dlLine.SetPosition(1, upLeft.transform.position);
    }
}
