using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusHelper : MonoBehaviour
{

    public GameObject targetHelper;
    private GameObject newObject;
    // Use this for initialization
    void Start()
    {
        //if there is none or more than 1 circle
        if(SpawnCircle.targetCircle.Count != 1)
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
        //if the red circle is destroyed
        if(newObject == null)
            return;
        //decrease the scale of the red circle
        newObject.transform.localScale = new Vector3(newObject.transform.localScale.x - 0.15f * (Time.deltaTime * 4),
            newObject.transform.localScale.y - 0.15f * (Time.deltaTime * 4), 1);
        //when the circle is too small, destroy it
        if (newObject.transform.localScale.x < 0.05 || gameObject.transform.localScale.x != 28)
            Destroy(newObject);
    }
}
