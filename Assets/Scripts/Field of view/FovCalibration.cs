using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FovCalibration : MonoBehaviour
{

    //all targets
    public GameObject UpLeft;
    public GameObject UpRight;
    public GameObject DownLeft;
    public GameObject DownRight;
    //raycast
    private RayCaster rCaster;
    //last hited GO
    private GameObject lastObj;
    //index to find at which step of the calibration we are
    private int index = 0;
    //speed of the moving targets
    private float speed = 1f;
    private GameObject obj;
    //all the vertical and horizontal texts of the targets 
    public List<Text> verticalTextList = new List<Text>();
    public List<Text> horizontalTextList = new List<Text>();

    // Use this for initialization
    void Start()
    {
        // rCaster = GameObject.FindGameObjectWithTag("EditorOnly").GetComponent<RayCastF>();
        rCaster = Camera.main.GetComponent<RayCaster>();

        //if the upleftpos target is at 0.0.0 we put back the targets at the last calibration
        if (FovStatic.upLeftPos != new Vector3(0, 0, 0))
            ResumeLastCalibration();

        //logs name and scenetimer
        LoggerBehavior.sceneName = "Fov calibration";
        LoggerBehavior.sceneTimer = 0;

        //change text on the targets
        RandomizeText();

        //disable targets
        UpRight.SetActive(false);
        DownRight.SetActive(false);
        DownLeft.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //update scene timer
        LoggerBehavior.sceneTimer += Time.deltaTime;
        //press space to go to the next target
        if (Input.GetKeyUp(KeyCode.Space))
            NextTarget();

        //Raycast of from camera through the gaze point
        RaycastHit hit;
        if (Physics.Raycast(rCaster.ray, out hit))
        {
            //save hited go and make it green
            obj = hit.transform.gameObject;
            obj.GetComponent<Renderer>().material.color = Color.green;

            //if the new hited object is different from the last one, change the last one's color to white again
            if ((obj != lastObj || obj == null) && lastObj != null)
                lastObj.GetComponent<Renderer>().material.color = Color.white;

            //hited GO become last hited GO
            lastObj = obj;

            //Look at the name of the GO and his parent then move his parent to the right position
            if (obj.name == "Diagonal" && obj.transform.parent.gameObject.name == "UpLeft")
            {
                hit.transform.parent.localPosition = new Vector3(hit.transform.parent.localPosition.x - speed * Time.deltaTime,
                    hit.transform.parent.localPosition.y + speed * Time.deltaTime, hit.transform.parent.localPosition.z);
            }
            else if (obj.name == "Diagonal" && obj.transform.parent.gameObject.name == "DownLeft")
            {
                hit.transform.parent.localPosition = new Vector3(hit.transform.parent.localPosition.x - speed * Time.deltaTime,
                    hit.transform.parent.localPosition.y - speed * Time.deltaTime, hit.transform.parent.localPosition.z);
            }
            else if (obj.name == "Diagonal" && obj.transform.parent.gameObject.name == "DownRight")
            {
                hit.transform.parent.localPosition = new Vector3(hit.transform.parent.localPosition.x + speed * Time.deltaTime,
                    hit.transform.parent.localPosition.y - speed * Time.deltaTime, hit.transform.parent.localPosition.z);
            }
            else if (obj.name == "Diagonal" && obj.transform.parent.gameObject.name == "UpRight")
            {
                hit.transform.parent.localPosition = new Vector3(hit.transform.parent.localPosition.x + speed * Time.deltaTime,
                    hit.transform.parent.localPosition.y + speed * Time.deltaTime, hit.transform.parent.localPosition.z);
            }
            else if (obj.name == "Up")
            {
                hit.transform.parent.localPosition = new Vector3(hit.transform.parent.localPosition.x,
                    hit.transform.parent.localPosition.y + speed * Time.deltaTime, hit.transform.parent.localPosition.z);
            }
            else if (obj.name == "Left")
            {
                hit.transform.parent.localPosition = new Vector3(hit.transform.parent.localPosition.x - speed * Time.deltaTime,
                    hit.transform.parent.localPosition.y, hit.transform.parent.localPosition.z);
            }
            else if (obj.name == "Right")
            {
                hit.transform.parent.localPosition = new Vector3(hit.transform.parent.localPosition.x + speed * Time.deltaTime,
                    hit.transform.parent.localPosition.y, hit.transform.parent.localPosition.z);
            }
            else if (obj.name == "Down")
            {
                hit.transform.parent.localPosition = new Vector3(hit.transform.parent.localPosition.x,
                    hit.transform.parent.localPosition.y - speed * Time.deltaTime, hit.transform.parent.localPosition.z);
            }
        }
        else if (lastObj != null)
        {
            lastObj.GetComponent<Renderer>().material.color = Color.white;
        }
    }

    /// <summary>
    /// Go to the next step
    /// save the last step's target position for the fov
    /// the position is updated with the different scales
    /// active the next step's target
    /// </summary>
    public void NextTarget()
    {
        index++;
        if (index == 1)
        {
            FovStatic.upLeftPos = new Vector3(UpLeft.transform.localPosition.x / (5 / 0.31f),
             UpLeft.transform.localPosition.y / (5 / 0.31f),
             UpLeft.transform.localPosition.z / (5 / 0.31f));
            UpRight.SetActive(true);
        }

        if (index == 2)
        {
            FovStatic.upRightPos = new Vector3(UpRight.transform.localPosition.x / (5 / 0.31f),
             UpRight.transform.localPosition.y / (5 / 0.31f),
             UpRight.transform.localPosition.z / (5 / 0.31f));
            DownRight.SetActive(true);
        }

        if (index == 3)
        {
            FovStatic.downRightPos = new Vector3(DownRight.transform.localPosition.x / (5 / 0.31f),
             DownRight.transform.localPosition.y / (5 / 0.31f),
             DownRight.transform.localPosition.z / (5 / 0.31f));
            DownLeft.SetActive(true);
        }

        if (index == 4)
            FovStatic.downLeftPos = new Vector3(DownLeft.transform.localPosition.x / (5 / 0.31f),
             DownLeft.transform.localPosition.y / (5 / 0.31f),
             DownLeft.transform.localPosition.z / (5 / 0.31f));
    }

    /// <summary>
    /// resume the postion of the targets from the last calibration
    /// </summary>
    private void ResumeLastCalibration()
    {
        UpLeft.transform.localPosition = new Vector3(FovStatic.upLeftPos.x * (5 / 0.31f), FovStatic.upLeftPos.y * (5 / 0.31f), FovStatic.upLeftPos.z * (5 / 0.31f));
        UpRight.transform.localPosition = new Vector3(FovStatic.upRightPos.x * (5 / 0.31f), FovStatic.upRightPos.y * (5 / 0.31f), FovStatic.upRightPos.z * (5 / 0.31f)); ;
        DownLeft.transform.localPosition = new Vector3(FovStatic.downLeftPos.x * (5 / 0.31f), FovStatic.downLeftPos.y * (5 / 0.31f), FovStatic.downLeftPos.z * (5 / 0.31f)); ;
        DownRight.transform.localPosition = new Vector3(FovStatic.downRightPos.x * (5 / 0.31f), FovStatic.downRightPos.y * (5 / 0.31f), FovStatic.downRightPos.z * (5 / 0.31f)); ;
    }

    /// <summary>
    /// reset the positions of the target, the index 
    /// and only make the first step's target active
    /// </summary>
    public void ResetFov()
    {
        FovStatic.upLeftPos = new Vector3(0, 0, 0);
        FovStatic.upRightPos = new Vector3(0, 0, 0);
        FovStatic.downLeftPos = new Vector3(0, 0, 0);
        FovStatic.downRightPos = new Vector3(0, 0, 0);

        UpLeft.transform.localPosition = new Vector3(0, 0, 5);
        UpRight.transform.localPosition = new Vector3(0, 0, 5);
        DownLeft.transform.localPosition = new Vector3(0, 0, 5);
        DownRight.transform.localPosition = new Vector3(0, 0, 5);

        index = 0;

        UpLeft.SetActive(true);
        UpRight.SetActive(false);
        DownRight.SetActive(false);
        DownLeft.SetActive(false);
    }

    private string randomText = "";

    /// <summary>
    /// Randomize the text shown on the targets
    /// </summary>
    public void RandomizeText()
    {
        foreach (var htext in horizontalTextList)
        {
            for (int i = 0; i < 5; i++)
            {
                randomText += (char)Random.Range(65, 91) + " ";
            }
            htext.text = randomText;
            randomText = "";
        }
        foreach (var vtext in verticalTextList)
        {
            for (int i = 0; i < 5; i++)
            {
                randomText += (char)Random.Range(65, 91) + "\n";
            }
            vtext.text = randomText;
            randomText = "";
        }
    }

    /// <summary>
    /// Increase the scale of the texts
    /// </summary>
    public void IncreaseTextSize()
    {
        foreach (var htext in horizontalTextList)
        {
            htext.transform.localScale = new Vector3(htext.transform.localScale.x + 0.1f,
            htext.transform.localScale.y + 0.1f,
            htext.transform.localScale.z + 0.1f);
        }
        foreach (var vtext in verticalTextList)
        {
            vtext.transform.localScale = new Vector3(vtext.transform.localScale.x + 0.1f,
            vtext.transform.localScale.y + 0.1f,
            vtext.transform.localScale.z + 0.1f);
        }
    }

    /// <summary>
    /// decrease the size of the texts
    /// </summary>
    public void ReduceTextSize()
    {
        foreach (var htext in horizontalTextList)
        {
            if (htext.transform.localScale.x > 0)
                htext.transform.localScale = new Vector3(htext.transform.localScale.x - 0.1f,
            htext.transform.localScale.y - 0.1f,
            htext.transform.localScale.z - 0.1f);
        }
        foreach (var vtext in verticalTextList)
        {
            if (vtext.transform.localScale.x > 0)
                vtext.transform.localScale = new Vector3(vtext.transform.localScale.x - 0.1f,
            vtext.transform.localScale.y - 0.1f,
            vtext.transform.localScale.z - 0.1f);
        }
    }

    /// <summary>
    /// reset scene timer and scene name for logs when we leave the scene
    /// </summary>
    void OnDestroy()
    {
        LoggerBehavior.sceneName = "";
        LoggerBehavior.sceneTimer = 0;
    }
}
