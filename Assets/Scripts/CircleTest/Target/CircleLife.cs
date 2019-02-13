using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Target lifetime and TTFF,
/// save his informations when it's destroyed
/// </summary>
public class CircleLife : MonoBehaviour
{
    private Vector3 lastSize;
    private Vector3 currentSize;
    private int nbOfSwitch;
    private float limitTimer = 1f;
    public float TTFF;
    public bool isTTFF;
    private bool isBigger, isSmaller;
    private int index;
    public float lifeTime;


    /// <summary>
    /// set the index for the lists where it save its data
    /// </summary>
    /// <param name="index"></param>
    public void Init(int index)
    {
        this.index = index;
        nbOfSwitch = 0;
        isSmaller = true;
        isBigger = true;
        lastSize = gameObject.transform.localScale;
        lifeTime = StartConfig.targetLifeSpan / 1000;
        isTTFF = true;
    }

    // Update is called once per frame
    void Update()
    {
        //if there is more than 1 circle, return (for result)
        if (SpawnCircle.targetCircle.Count > 1)
            return;

        if (isTTFF) //update TTFF time until the circle is focused by the gaze point (when the scale change)
            TTFF += Time.deltaTime;
        if (gameObject.transform.localScale.x < 30)
            isTTFF = false;

        if (Input.GetKeyUp(KeyCode.Space) && StartConfig.inputMode)
            SelfDestroy();

        if (!StartConfig.inputMode)
            AutoMode();
    }

    /// <summary>
    /// Auto mode, where no input of another user is needed
    /// </summary>
    private void AutoMode()
    {
        //current size of the circle
        currentSize = gameObject.transform.localScale;
        if (currentSize.x < lastSize.x)
        {
            Reducing();
            lifeTime = StartConfig.targetLifeSpan / 1000;
            //put isTTFF false so the TTFF time doesn't update anymore
            isTTFF = false;
        }
        if (currentSize.x > lastSize.x)
        {
            Extending();
            lifeTime = StartConfig.targetLifeSpan / 1000;
        }
        //if the size of the circle is 0 or less OR if the size edit switched more than 5 times
        if (currentSize.x <= 0 || nbOfSwitch > 5)
            SelfDestroy();
        //every second reset the number of switch between reducing and extending the circle's size
        limitTimer -= Time.deltaTime;
        if (limitTimer < 0)
        {
            limitTimer = 1f;
            nbOfSwitch = 0;
        }
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
            SelfDestroy();
    }

    /// <summary>
    /// Destroy the target and save the deta
    /// </summary>
    private void SelfDestroy()
    {
        //save the circle's scale and destroy it
        SpawnCircle.circleFinalSize[index] = gameObject.transform.localScale.x;
        SpawnCircle.finalGazePos[index] = GazeMarker.gazePath[GazeMarker.gazePath.Count - 1];
        SpawnCircle.targetCircle.Remove(gameObject);
        Destroy(gameObject);
        GazeMarker.gazePath.Clear();
    }

    /// <summary>
    /// update circle's last size, and increment the number of switch 
    /// put the reducing bool at false and the extending one to true
    /// </summary>
    private void Reducing()
    {
        lastSize = currentSize;
        if (isSmaller)
        {
            nbOfSwitch++;
            isSmaller = false;
            isBigger = true;
        }
    }

    /// <summary>
    /// update circle's last size, and increment the number of switch 
    /// put the extending bool at false and the reducing one to true
    /// </summary>
    private void Extending()
    {
        lastSize = currentSize;
        if (isBigger)
        {
            nbOfSwitch++;
            isBigger = false;
            isSmaller = true;
        }
    }
}
