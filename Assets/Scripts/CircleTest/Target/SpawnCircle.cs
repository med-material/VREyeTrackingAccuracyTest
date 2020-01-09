using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public enum SpawnArea
{
    UpperLeft,
    UpperCenter,
    UpperRight,
    MiddleLeft,
    MiddleCenter,
    MiddleRight,
    BottomLeft,
    BottomCenter,
    BottomRight
}

public class SpawnCircle : MonoBehaviour
{
    public GameObject spawnObject;
    public GameObject gazeDot;

    private Vector3[] spawnArea =
    {new Vector3(-30f,30f,-0.05f), new Vector3(0f,30f,-0.05f), new Vector3(30f,30f,-0.05f),
    new Vector3(-30f,0f,-0.05f), new Vector3(0f,0f,-0.05f), new Vector3(30f,0f,-0.05f),
    new Vector3(-30f,-30f,-0.05f), new Vector3(0f,-30f,-0.05f), new Vector3(30f,-30f,-0.05f)};

    public static float[] circleFinalSize = { 28, 28, 28, 28, 28, 28, 28, 28, 28 };
    public static Vector3[] finalGazePos = new Vector3[9];
    private bool[] isVisited = new bool[9];
    public List<int> indexOrder = new List<int>();

    public List<GameObject> goPathList = new List<GameObject>();

    private int index;
    public Text countObj;
    public static float countDown = 3f;

    private bool hasLogged = true;

    public static List<GameObject> targetCircle = new List<GameObject>();
    public List<GameObject> offsetGazeList = new List<GameObject>();

    LoggingManager loggingManager;
    LoggerBehavior loggerBehavior;

    private float currentEyeAccuracy = -1f;

    // Use this for initialization
    void Start()
    {
        index = 0;
        loggingManager = GameObject.Find("LoggingManager").GetComponent<LoggingManager>();
        loggerBehavior = GameObject.Find("Pupil Manager").GetComponent<LoggerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;
        //starting countdown
        if (countDown > 0)
        {
            countObj.text = System.Math.Round(countDown, 0).ToString();
            return;
        }
        if (countDown < 0)
        {
            countObj.gameObject.SetActive(false);
        }

        //if there is no circle on the grid
        if (targetCircle.Count < 1) {
            if (!hasLogged) {
                Debug.Log("Logging circle for " + Enum.GetName(typeof(SpawnArea), (SpawnArea)index));
                string date = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                loggingManager.WriteToLog("DateAdded",date);
                loggingManager.WriteToLog("CircleName", Enum.GetName(typeof(SpawnArea), (SpawnArea)index));
                loggingManager.WriteToLog("EyeTrackAccuracy", Math.Abs(currentEyeAccuracy).ToString("0.00"));
                loggingManager.DuplicateMetaColumns();
                hasLogged = true;
            }

            //if the list of spawn area contain a position not visited yet,
            //else we show the result of the test
            if (isVisited.Contains(false))
            {
                //get a random index which is not a visited position
                index = UnityEngine.Random.Range(0, 9);
                while (isVisited[index] == true)
                {
                    index = UnityEngine.Random.Range(0, 9);
                }
                isVisited[index] = true; //set the index position visited to true
                indexOrder.Add(index); //setup the spawn order list
                newCircle(index); //create the circle from that index
                hasLogged = false;
            }
            else
            {
                Result();
                loggerBehavior.SetBoolTest(true);
                loggingManager.UploadLogs();
            }
        } else {
            currentEyeAccuracy = 1f - targetCircle[targetCircle.Count-1].transform.localScale.x * (1f / 28f);
        }
    }

    /// <summary> 
    /// Create a new circle with his id's informations 
    /// </summary>
    /// <param name="id">id of the circle in the array that contain all the positions</param>
    /// <param name="reult">false by default, if true create the circle with its data result</param>
    public void newCircle(int id, bool result = false)
    {
        GameObject newObject = Instantiate(spawnObject);
        newObject.transform.SetParent(gameObject.transform);
        newObject.transform.localScale = new Vector3(circleFinalSize[id], 0.1f, circleFinalSize[id]);
        newObject.transform.localRotation = Quaternion.Euler(90, 0, 0);
        newObject.transform.localPosition = spawnArea[id];
        //set the index of the circle and add it to the target list
        newObject.GetComponent<CircleLife>().Init(id);
        targetCircle.Add(newObject);

        if (result)
        {
            ShowOffset(id, newObject.transform.localPosition);
        }
    }

    /// <summary>
    /// show the gaze offset on a given circle's center and draw a line
    /// from the circle's center to the last gaze position for that circle
    /// </summary>
    /// <param name="id">int id of the circle</param>
    /// <param name="pos">Vector3 position of the circle</param>
    private void ShowOffset(int id, Vector3 pos)
    {
        GameObject newGazePoint = Instantiate(gazeDot);
        newGazePoint.transform.SetParent(gameObject.transform);
        newGazePoint.transform.localPosition = finalGazePos[id];

        var go = new GameObject();
        go.name = "offset : " + id;
        go.transform.SetParent(gameObject.transform);
        go.transform.localPosition = new Vector3(0, 0, 0);
        go.transform.localScale = new Vector3(1, 1, 1);
        go.transform.rotation = gameObject.transform.rotation;

        var lr = go.AddComponent<LineRenderer>();

        lr.SetPosition(0, new Vector3(pos.x, pos.y, newGazePoint.transform.localPosition.z));
        lr.SetPosition(1, newGazePoint.transform.localPosition);
        lr.startWidth = 0.04f;
        lr.endWidth = 0.04f;
        lr.useWorldSpace = false;

        newGazePoint.transform.SetParent(go.transform);

        offsetGazeList.Add(go);
    }

    /// <summary>
    /// Show every gaze path to every target
    /// </summary>
    private void ShowAllPath()
    {
        //parse each path list
        foreach (var path in GazeMarker.savedGazePath)
        {
            //create an empty GO
            var go = new GameObject();
            go.transform.SetParent(gameObject.transform.parent);
            go.name = "path";
            go.transform.localPosition = gameObject.transform.localPosition;
            go.transform.rotation = gameObject.transform.rotation;
            go.transform.localScale = gameObject.transform.localScale;

            //add a linerenderer to draw the path
            var lr = go.AddComponent<LineRenderer>();
            lr.positionCount = path.Count;
            lr.startWidth = 0.04f;
            lr.endWidth = 0.04f;
            lr.useWorldSpace = false;
            lr.material.color = Color.green;
            int positionToSet = 0;
            //parse all the gaze path vector3 and draw the path
            foreach (var pos in path)
            {
                lr.SetPosition(positionToSet, pos);
                positionToSet++;
            }
            //set the parents to its target
            go.transform.SetParent(gameObject.transform);
            goPathList.Add(go);
        }
    }

    /// <summary>
    /// Show every circle with their final size, 
    /// the gaze path, the offset, and the gaze position
    /// when we changed the target
    /// </summary>
    public void Result()
    {
        DestroyAllCircles();
        for (int i = 0; i < 9; i++)
        {
            newCircle(i, true);
        }
        ShowAllPath();
        WorstOffset();
    }

    /// <summary>
    /// Determine the worst X and Y offset
    /// </summary>
    private void WorstOffset()
    {
        float worstX = 0;
        float worstY = 0;

        //get the absolute X and Y offset of each circle and substract its target's position,
        //keep the biggest offset on X and Y in fovStatic for the fov calibration scene
        for (int i = 0; i < indexOrder.Count; i++)
        {
            float offsetX = Mathf.Abs(finalGazePos[indexOrder[i]].x);

            float offsetY = Mathf.Abs(finalGazePos[indexOrder[i]].y);

            float circleCenterX = Mathf.Abs(spawnArea[indexOrder[i]].x);

            float circleCenterY = Mathf.Abs(spawnArea[indexOrder[i]].y);

            float offsetDiffX = Mathf.Abs(offsetX - circleCenterX);
            float offsetDiffY = Mathf.Abs(offsetY - circleCenterY);

            if (worstX < offsetDiffX)
                worstX = offsetDiffX;

            if (worstY < offsetDiffY)
                worstY = offsetDiffY;
        }
        FovStatic.horizontalSize = worstX;
        FovStatic.verticalSize = worstY;
    }

    ///<summary> Destroy every GameObject target
    ///<typeparam name="Retry">if true, everything is reset as every lists that save the state of the different GO</typeparam>
    ///</summary>
    public void DestroyAllCircles(bool retry = false)
    {
        //destroy every circles
        foreach (var obj in targetCircle)
        {
            Destroy(obj);
        }
        //destroy every gaze marker
        foreach (var obj in offsetGazeList)
        {
            Destroy(obj);
        }

        //clear all lists and destroy all GO on the grid
        if (retry)
        {
            targetCircle.Clear();
            offsetGazeList.Clear();
            foreach (var obj in goPathList)
            {
                Destroy(obj);
            }
            goPathList.Clear();
            circleFinalSize = new float[] { 28, 28, 28, 28, 28, 28, 28, 28, 28 };
            finalGazePos = new Vector3[9];
            GazeMarker.gazePath.Clear();
            GazeMarker.savedGazePath.Clear();
            isVisited = new bool[9];
            countObj.gameObject.SetActive(true);
            countDown = 3f;
        }
    }

    /// <summary>
    /// Clear all the lists
    /// </summary>
    void OnDestroy()
    {
        targetCircle.Clear();
        offsetGazeList.Clear();
        goPathList.Clear();
        circleFinalSize = new float[] { 28, 28, 28, 28, 28, 28, 28, 28, 28 };
        finalGazePos = new Vector3[9];
        GazeMarker.gazePath.Clear();
        GazeMarker.savedGazePath.Clear();
        isVisited = new bool[9];
        countObj.gameObject.SetActive(true);

        LoggerBehavior.sceneName = "";
        LoggerBehavior.sceneTimer = 0;
    }
}