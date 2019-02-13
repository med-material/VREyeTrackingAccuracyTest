using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public GameObject menu;
    private string accuracyTest = "CircleTest";
    private string fovCalibration = "Field of view";
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //change the active state of the menu when we press Escape
        if (Input.GetKeyUp(KeyCode.Escape))
            menu.gameObject.SetActive(!menu.activeSelf);
    }

    /// <summary>
    /// Start the accuracy test scene
    /// </summary>
    public void StartAccuTest()
    {
        //if the fov scene is active we disable it
        if (SceneManager.GetSceneByName("Field of view").isLoaded)
            UnloadActiveScene("Field of view");
        //if the accuracy test is already launched we return 
        //to avoid having 2 accuracy test scene
        if (SceneManager.GetSceneByName("CircleTest").isLoaded)
            return;
        //disable the menu and the current main camera then start the new scene
        menu.gameObject.SetActive(false);
        mainCamera.enabled = false;
        StartCoroutine(LoadCurrentScene(accuracyTest));
    }

    /// <summary>
    /// Unload the given scene
    /// </summary>
    /// <param name="sceneName">scene name in the build</param>
    public void UnloadActiveScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
        menu.gameObject.SetActive(false);
        mainCamera.enabled = true;
    }

    /// <summary>
    /// start the fov calibration scene
    /// </summary>
    public void StartFovCalibration()
    {
        //if the accuracy test scene is active we disable it
        if (SceneManager.GetSceneByName("CircleTest").isLoaded)
            UnloadActiveScene("CircleTest");
        //if the fov scene is already launched we return 
        //to avoid having 2 fov scene
        if (SceneManager.GetSceneByName("Field of view").isLoaded)
            return;
        //disable the menu and the current main camera then start the new scene
        menu.gameObject.SetActive(false);
        mainCamera.enabled = false;
        StartCoroutine(LoadCurrentScene(fovCalibration));
    }

    /// <summary>
    /// Load the scene from his name
    /// </summary>
    /// <param name="sceneName">scene name in the build</param>
    /// <returns>async load of the scene</returns>
    IEnumerator LoadCurrentScene(string sceneName)
    {
        AsyncOperation asyncScene = SceneManager.LoadSceneAsync(sceneName
            , LoadSceneMode.Additive);

        while (!asyncScene.isDone)
        {
            yield return null;
        }
    }
}
