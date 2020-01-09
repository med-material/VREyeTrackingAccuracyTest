using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// remove parent from the color switching cross (the center of the circle)
/// so its scale doesn't change with the circle
/// </summary>
public class CrossController : MonoBehaviour
{

    private float timer = 0.25f;
    private float waitTime = 0;
    private Transform oldParent;

    // Use this for initialization
    void Start()
    {
        //get the parent GO
        oldParent = gameObject.transform.parent;
        //set the parent's parent GO as parent (the parent is no more the circle but the whole grid)
        gameObject.transform.SetParent((gameObject.transform.parent).transform.parent);
    }

    // Update is called once per frame
    void Update()
    {
        //if the old parent (the target) is destroyed, destroy the circle aswell
        if (oldParent == null)
            Destroy(gameObject);
        else
        {
            //when the old parent (the circle) is disabled, just hide the cross
            if (oldParent.gameObject.activeSelf == false)
                gameObject.GetComponent<MeshRenderer>().enabled = false;
            else
                gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
