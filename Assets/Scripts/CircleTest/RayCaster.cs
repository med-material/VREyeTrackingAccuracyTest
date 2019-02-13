using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

/// <summary>
/// RayCast with pupil lab's eye tracking gaze position
/// </summary>
public class RayCaster : MonoBehaviour
{

    //allow to chose which kind of calibration is made (3D or 2D)
    public bool _3D = false;
    //public Transform marker;
    // private CalibrationDemo calibrationDemo;
    //3D calibration gaze poisition
    private Vector3 gazePosition3D;
    public Ray ray;
    private Camera mainCamera;
    private Vector2 gazePosition2D;
    private Vector3 viewportPoint;
    private LineRenderer heading;
    public static RaycastHit hit;

    /// <summary>
    /// basic method used by pupil lab to start the eye tracking
    /// </summary>
    void Start()
    {
        PupilData.calculateMovingAverage = true;
        heading = gameObject.GetComponent<LineRenderer>();
        // calibrationDemo = gameObject.GetComponent<CalibrationDemo>();
        mainCamera = Camera.main;
    }

    /// <summary>
    /// basic method used by pupil lab to start the eye tracking
    /// </summary>
    void OnEnable()
    {
        if (PupilTools.IsConnected)
        {
            PupilGazeTracker.Instance.StartVisualizingGaze();
            //PupilTools.IsGazing = true;
            //PupilTools.SubscribeTo ("gaze");
        }
    }

    // Check if it's a 3D or 2D calibration
    void Update()
    {
        if (_3D)
            ray3D();
        else
            ray2D();
    }

    private RaycastHit[] hits;
    public GameObject gazePosObj;

    /// <summary>
    /// use the 2D gaze point position to cast the ray through a viewport point
    /// </summary>
    private void ray2D()
    {
        //Vector3 point where the raycast will go trough
        viewportPoint = new Vector3(0.5f, 0.5f, 10);
        //if pupil lab is connected and the calibration is done (so IsGazing will be true)
        //take the x and y position of the gaze point to update the viewport point
        if (PupilTools.IsConnected && PupilTools.IsGazing)
        {
            gazePosition2D = PupilData._2D.GazePosition;
            viewportPoint = new Vector3(gazePosition2D.x, gazePosition2D.y, 1f);
        }
        //draw the linerenderer forward from the camera's position
        heading.SetPosition(0, mainCamera.transform.position - mainCamera.transform.up);
        //ray going from the camera through the viewport point
        ray = mainCamera.ViewportPointToRay(viewportPoint);

        //move the linerenderer to see the ray going from the camera through the viewport point in game window
        if (gameObject.GetComponent<LineRenderer>().enabled)
        {
            if (Physics.Raycast(ray, out hit))
            {
                //draw line from camera to the hit point
                heading.SetPosition(1, hit.point);
                //position of the hited object
                hit.point = transform.InverseTransformPoint(hit.point);
            }
            else //if nothing is hit by the raycast
            {
                heading.SetPosition(1, ray.origin + ray.direction * 50f);
            }
        }
    }

    /// <summary>
    /// use the 3D gaze point position to cast the ray
    /// </summary>
    private void ray3D()
    {
        gazePosition3D = PupilData._3D.GazePosition;
        //marker.localPosition = PupilData._3D.GazePosition;
        ray = new Ray(mainCamera.transform.position, mainCamera.transform.rotation * gazePosition3D * 10);
        Debug.DrawRay(mainCamera.transform.position, mainCamera.transform.rotation * gazePosition3D * 10);
    }
}
