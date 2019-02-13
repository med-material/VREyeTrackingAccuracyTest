using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// destroy the representative frame with pupil position from the pupil lab calibration scene
/// </summary>
public class DestroyPositioner : MonoBehaviour
{
    void Update()
    {
        //destroy the frames when D or C is pressed 
        //(D just to remove them and C is pressed to start the calibration)
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.C))
        {
            Destroy(gameObject);
        }
    }
}
