using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeMarker : MonoBehaviour
{
    public static List<Vector3> gazePath = new List<Vector3>();
    public static List<List<Vector3>> savedGazePath = new List<List<Vector3>>();
    private RayCaster rCaster;

    void Start()
    {
        // rCaster = GameObject.FindGameObjectWithTag("EditorOnly").GetComponent<RayCastF>();
        rCaster = Camera.main.GetComponent<RayCaster>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject hitObject;
        RaycastHit[] hits;

        // everything that the raycast hit
        hits = Physics.RaycastAll(rCaster.ray);
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            hitObject = hit.collider.gameObject;
            //save the gaze marker position
            gazePath.Add(gameObject.transform.localPosition);
        }
    }
}
