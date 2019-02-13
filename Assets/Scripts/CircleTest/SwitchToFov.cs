using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SwitchToFov : MonoBehaviour
{
    /// <summary>
    /// change the scene from the accuracy test to the fov calibration
    /// </summary>
    public void StartFovScene()
    {
        //unload the accuracy test scene
        SceneManager.UnloadSceneAsync("CircleTest");
        //load the fov calibration scene
        StartCoroutine(LoadCurrentScene("Field of view"));
        //disable the menu
        gameObject.SetActive(false);
    }

    /// <summary>
    /// load the scene with the given name
    /// </summary>
    /// <param name="sceneName">scene's name in the build</param>
    /// <returns>loaded scene</returns>
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
