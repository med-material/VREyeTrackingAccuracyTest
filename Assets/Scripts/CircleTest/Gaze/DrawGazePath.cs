using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DrawGazePath : MonoBehaviour
{

    // Use this for initialization
    private LineRenderer line;
    private Vector3 lastPos;
    private List<Vector3> currentLinePoints = new List<Vector3>();
    private GameObject lastTarget;
    void Start()
    {
        SetupLine();
    }

    // Update is called once per frame
    void Update()
    {
        //if there is more or less than 1 return
        if (SpawnCircle.targetCircle.Count != 1)
            return;
        //save all the gazemarkers position during the same target, space destroy the target aswell
        if (SpawnCircle.targetCircle[0] != lastTarget)
        {
            GazeMarker.savedGazePath.Add(new List<Vector3>(currentLinePoints));
            line.positionCount = 0;
            currentLinePoints.Clear();
            lastTarget = SpawnCircle.targetCircle[0];
        }
        //avoid saving staring position to save place, add the position and draw the 
        if (GazeMarker.gazePath.Count == 0) {
            return;
        } 

        if (lastPos != GazeMarker.gazePath[GazeMarker.gazePath.Count - 1])
        {
            lastPos = GazeMarker.gazePath[GazeMarker.gazePath.Count - 1];
            AddLinePoint(lastPos);
        }
    }

    /// <summary>
    /// Add linerenderer and set it up
    /// </summary>
    void SetupLine()
    {
        line = gameObject.AddComponent<LineRenderer>();
        line.material.color = Color.red;
        line.startWidth = 0.04f;
        line.endWidth = 0.04f;
        line.useWorldSpace = false;
        line.sortingLayerName = "DesktopOnly";
        line.sortingOrder = 5;
        // line.SetPosition(0, GazeMarker.gazePath[GazeMarker.gazePath.Count - 1]);
    }

    /// <summary>
    /// Add new linerenderer point, draw the line
    /// </summary>
    /// <param name="newPos">new Vector3 position</param>
    void AddLinePoint(Vector3 newPos)
    {
        currentLinePoints.Add(newPos); // add the new point to our saved list of line points
        line.positionCount = currentLinePoints.Count; // set the line’s vertex count to how many points we now have, which will be 1 more than it is currently
        line.SetPosition(currentLinePoints.Count - 1, newPos); // add newPoint as the last point on the line (count -1 because the SetPosition is 0-based and Count is 1-based)    
    }
}
