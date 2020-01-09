using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusHelper : MonoBehaviour
{
    public GameObject targetHelper;
    private GameObject newObject;
    private float circleTimer = 0.3f, circleMaxSize = 28f;
    private float circleSpeed = 0.3f / 28f;

    // Use this for initialization
    void Start()
    {
        //if there is none or more than 1 circle
        if (SpawnCircle.targetCircle.Count != 1)
            return;
        //spawn a red circle on the target
        newObject = Instantiate(targetHelper);
        newObject.transform.SetParent(gameObject.transform);
        newObject.transform.localScale = new Vector3(1, 1, 1);
        newObject.transform.localRotation = Quaternion.Euler(90, 0, 0);
        newObject.transform.localPosition = new Vector3(0, -2, 0);
    }

    // Update is called once per frame
    void Update()
    {
        float lastCircleTimer = circleTimer;
        circleTimer -= Time.deltaTime;

        //if the red circle is destroyed
        if (newObject == null)
            return;
        //decrease the scale of the red circle
        newObject.transform.localScale = new Vector3(newObject.transform.localScale.x * circleTimer / lastCircleTimer,
            newObject.transform.localScale.y * circleTimer / lastCircleTimer, 1);
        //when the circle is too small, destroy it
        if (newObject.transform.localScale.x < 0.05 || circleTimer <= 0) Destroy(newObject);
    }
}