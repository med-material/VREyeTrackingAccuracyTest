using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System;

public class StartConfig : MonoBehaviour
{

    public static string userID;
    public static float targetLifeSpan;
    public GameObject lifeSpanFieldGameObject;
    public Text userIDValue, targetLifeSpanValue, targetLifeSpanTxt;
    public static bool makeUp = false, glasses = false, gazeDot = true, grid = true, inputMode = true;
    public Dropdown targetLifeSpanDropdown;

    /// <summary>
    /// Save all config variables and start calibration 2D
    /// </summary>
    public void startCalibration()
    {
        if (userIDValue.text == "")
        {
            StartCoroutine(InfoMissing(userIDValue));
            return;
        }

        userID = userIDValue.text;
        PlayerPrefs.SetInt("Settings:GazeDot", gazeDot ? 1: 0);
        PlayerPrefs.SetInt("Settings:Grid", grid ? 1: 0);

        switch(targetLifeSpanDropdown.value)
        {
            case 0:
                inputMode = false;
                targetLifeSpan = 500;
                break;
            case 1:
                inputMode = false;
                targetLifeSpan = 1000;
                break;
            case 2:
                inputMode = false;
                targetLifeSpan = 2000;
                break;
            case 3:
                inputMode = true;
                targetLifeSpan = 0;
                break;
            default:
                inputMode = true;
                targetLifeSpan = 0;
                break;
        }

        StartCoroutine(LoadCurrentScene("2D Calibration Demo"));
    }

    /// <summary>
    /// Load the scene from his name
    /// </summary>
    /// <param name="sceneName">scene name in the build</param>
    /// <returns>async load of the scene</returns>
    IEnumerator LoadCurrentScene(string sceneName)
    {
        AsyncOperation asyncScene = SceneManager.LoadSceneAsync(sceneName
            , LoadSceneMode.Single);

        while (!asyncScene.isDone)
        {
            yield return null;
        }
    }

    public void TargetSwitchMode()
    {
        lifeSpanFieldGameObject.SetActive(!lifeSpanFieldGameObject.activeSelf);
        targetLifeSpanTxt.enabled = !targetLifeSpanTxt.enabled;
    }

    // userIDValue.transform.parent.transform.GetComponent<Image>().color = Color.red;
    private float timer = 0.25f;
    private float waitTime = 0;
    private IEnumerator InfoMissing(Text missingText)
    {
        bool blinked = false;

        while (!blinked)
        {
            waitTime += Time.deltaTime;
            if (waitTime < timer)
            {
                missingText.transform.parent.transform.GetComponent<Image>().color = Color.red;
            }
            if (waitTime > timer)
            {
                missingText.transform.parent.transform.GetComponent<Image>().color = Color.white;
            }
            if (waitTime > timer * 2)
            {
                blinked = true;
                waitTime = 0;
            }
            yield return null;
        }
    }
}
