using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathResult : MonoBehaviour
{

    private int index = 0;
    private SpawnCircle spawnCircle;

    void Start()
    {
        spawnCircle = gameObject.GetComponent<SpawnCircle>();
    }

    /// <summary>
    /// Show previous target and path with offset data
    /// </summary>
    public void PreviousPath()
    {
        //while there is no 9 index ordered by spawn (means that every target didn't spawn yet)
        if (spawnCircle.indexOrder.Count < 9)
            return;
        index--;
        if (index < 0)
        {
            index = 8;
        }
        //hide all targets and then show the index target and path
        HidePaths();
        spawnCircle.goPathList[index].gameObject.SetActive(true);
        spawnCircle.offsetGazeList[spawnCircle.indexOrder[index]].gameObject.SetActive(true);
        SpawnCircle.targetCircle[spawnCircle.indexOrder[index]].gameObject.SetActive(true);
    }

    /// <summary>
    /// Show next target and path with offset data
    /// </summary>
    public void NextPath()
    {
        //while there is no 9 index ordered by spawn (means that every target didn't spawn yet)
        if (spawnCircle.indexOrder.Count < 9)
            return;
        index++;
        if (index > 8)
        {
            index = 0;
        }
        //hide all targets and then show the index target and path
        HidePaths();
        spawnCircle.goPathList[index].gameObject.SetActive(true);
        spawnCircle.offsetGazeList[spawnCircle.indexOrder[index]].gameObject.SetActive(true);
        SpawnCircle.targetCircle[spawnCircle.indexOrder[index]].gameObject.SetActive(true);
    }

    /// <summary>
    /// Disable all targets and path / offset data
    /// </summary>
    private void HidePaths()
    {
        //disable all path, circle, offset
        foreach (var go in spawnCircle.goPathList)
        {
            go.gameObject.SetActive(false);
        }
        foreach (var go in SpawnCircle.targetCircle)
        {
            go.gameObject.SetActive(false);
        }
        foreach (var go in spawnCircle.offsetGazeList)
        {
            go.gameObject.SetActive(false);
        }
    }
}
