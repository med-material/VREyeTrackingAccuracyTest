using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatmapToLocal : MonoBehaviour
{

    ParticleSystem part;
	// public GameObject mainCam;

    // Use this for initialization
    void Start()
    {
        part = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        //pupil lab heatmap positionning
        part.simulationSpace = ParticleSystemSimulationSpace.Local;
    }
}
