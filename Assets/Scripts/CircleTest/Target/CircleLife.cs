using UnityEngine;

/// <summary>
///     Target lifetime and TTFF,
///     save his informations when it's destroyed
/// </summary>
public class CircleLife : MonoBehaviour
{
    private Vector3 currentSize;
    public int index = -1;
    private bool isBigger, isSmaller;
    public bool isTTFF;
    private Vector3 lastSize;
    public float lifeTime;
    private float limitTimer = 1f;
    private int nbOfSwitch;
    public float TTFF;


    /// <summary>
    ///     set the index for the lists where it save its data
    /// </summary>
    /// <param name="index"></param>
    public void Init(int index)
    {
        this.index = index;
        isSmaller = true;
        isBigger = true;
        lastSize = gameObject.transform.localScale;
        lifeTime = StartConfig.targetLifeSpan / 1000;
        isTTFF = true;
    }

    // Update is called once per frame
    private void Update()
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
    ///     Auto mode, where no input of another user is needed
    /// </summary>
    private void AutoMode()
    {
        //current size of the circle
        currentSize = gameObject.transform.localScale;
        if (currentSize.x < lastSize.x)
        {
            Reducing();
            //put isTTFF false so the TTFF time doesn't update anymore
            isTTFF = false;
        }

        if (currentSize.x > lastSize.x) Extending();
        if (lifeTime < 0) SelfDestroy();
        //every second reset the number of switch between reducing and extending the circle's size
        if (limitTimer < 0) limitTimer = 1f;
        limitTimer -= Time.deltaTime;
        lifeTime -= Time.deltaTime;
    }

    /// <summary>
    ///     Destroy the target and save the deta
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
    ///     update circle's last size, and increment the number of switch
    ///     put the reducing bool at false and the extending one to true
    /// </summary>
    private void Reducing()
    {
        lastSize = currentSize;
        if (isSmaller)
        {
            isSmaller = false;
            isBigger = true;
        }
    }

    /// <summary>
    ///     update circle's last size, and increment the number of switch
    ///     put the extending bool at false and the reducing one to true
    /// </summary>
    private void Extending()
    {
        lastSize = currentSize;
        if (isBigger)
        {
            isBigger = false;
            isSmaller = true;
        }
    }
}