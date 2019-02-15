using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private RayCaster rCaster;
    private RaycastHit hit;
    private RaycastHit[] hits;
    public GameObject gazePosObj;
    public static Vector3 gazePosition;

    // private GameObject mainCamera;
    // private bool dotMarkVisibility;
    public GameObject menu;

    [SerializeField]
    private GameObject gridObject;

    [SerializeField]
    private GameObject gazeDispObject;

    // Use this for initialization
    void Start()
    {
        // rCaster = GameObject.FindGameObjectWithTag("EditorOnly").GetComponent<RayCastF>();
        rCaster = Camera.main.GetComponent<RayCaster>();
        LoggerBehavior.sceneName = "CircleTest";
        LoggerBehavior.sceneTimer = 0;

        bool grid = PlayerPrefs.GetInt("Settings:Grid") == 1;
        bool gazeDot = PlayerPrefs.GetInt("Settings:GazeDot") == 1;
        if (grid) {
            gridObject.SetActive(true);
        } else {
            gridObject.SetActive(false);
        }
        if (gazeDot) {
            gazeDispObject.SetActive(true);
            gazePosObj.SetActive(true);
        } else {
            gazeDispObject.SetActive(false);
            gazePosObj.SetActive(false);
        }
        // Toggle off grid
        // Toggle off GazeDispersion and GazePos

    }

    // Update is called once per frame
    void Update()
    {
        //update scene timer for logs
        LoggerBehavior.sceneTimer += Time.deltaTime;
        GazePosUpdate();
        if (Input.GetKeyUp(KeyCode.M))
            menu.SetActive(!menu.activeSelf);

        if (Physics.Raycast(rCaster.ray, out hit))
        {

            //if there is 1 circle on the grid
            if (SpawnCircle.targetCircle.Count == 1)
            {
                //if the hited object is a circle contained in the list
                if (SpawnCircle.targetCircle.Contains(hit.transform.gameObject))
                {
                    ReduceCircle(hit.transform.gameObject);
                }
                else
                {
                    ExtendCircle(SpawnCircle.targetCircle[0]);
                }
            }
        }
    }

    /// <summary>
    /// update gaze position on the grid
    /// </summary>
    void GazePosUpdate()
    {
        hits = Physics.RaycastAll(rCaster.ray);
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            if (hit.transform.name == "Grid")
            {
                gazePosObj.transform.localPosition = hit.transform.InverseTransformPoint(hit.point);
                gazePosition = hit.transform.InverseTransformPoint(hit.point);
                // print(gazePosObj.transform.localPosition);
            }
        }
    }

    /// <summary>
    /// Reduce the size of the circle
    /// </summary>
    /// <param name="circle">GameObject of the current circle</param>
    private void ReduceCircle(GameObject circle)
    {
        //15f * Time.deltaTime so the computers speed doesn't affect the speed
        if (circle.transform.localScale.x > 0)
        {
            circle.transform.localScale =
           new Vector3(circle.transform.localScale.x - 5f * (Time.deltaTime * 6),
           0.1f, circle.transform.localScale.z - 5f * (Time.deltaTime * 6));
        }
    }

    /// <summary>
    /// Extend the size of the circle
    /// </summary>
    /// <param name="circle">GameObject of the current circle</param>
    private void ExtendCircle(GameObject circle)
    {
        //if it's smaller than the max circle size
        if (circle.transform.localScale.x < 30)
        {
            //15f * Time.deltaTime so the computers speed doesn't affect the speed
            circle.transform.localScale =
            new Vector3(circle.transform.localScale.x + 5f * (Time.deltaTime * 6),
            0.1f, circle.transform.localScale.z + 5f * (Time.deltaTime * 6));
        }
    }
}