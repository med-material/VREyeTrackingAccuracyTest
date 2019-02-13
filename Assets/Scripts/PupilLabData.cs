using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PupilLabData : MonoBehaviour
{

    //left and right confidence text
    public Text lconf;
    public Text rconf;

    //left and right pupil frame GO
    public GameObject leftPupil;
    public GameObject rightPupil;

    //timer to limit the reading speed of the confidence
    public float refreshTime;
    private float countDown;
    //confidence 0=> right eye 1 => left eye
    public static float confidence0, confidence1;
    //array with the last 50 confidence average between right and left eye
    public static float[] confArray = new float[50];
    private int arrayIndex = 0;

    /// <summary>
    /// basic connection method with pupil lab software, see pupil lab doc
    /// </summary>
    void Start()
    {
        PupilTools.OnConnected += StartPupilSubscription;
        PupilTools.OnDisconnecting += StopPupilSubscription;

        PupilTools.OnReceiveData += CustomReceiveData;
        countDown = refreshTime;
    }

    /// <summary>
    /// basic connection method with pupil lab software, see pupil lab doc
    /// </summary>
    void StartPupilSubscription()
    {
        PupilTools.CalibrationMode = Calibration.Mode._2D;
        PupilTools.SubscribeTo("pupil.");
    }

    /// <summary>
    /// basic connection method with pupil lab software, see pupil lab doc
    /// </summary>
    void StopPupilSubscription()
    {
        PupilTools.UnSubscribeFrom("pupil.");
    }

    /// <summary>
    /// Reading subscribed topics, doesn't need to call it except in the start method with "PupilTools.OnReceiveData += CustomReceiveData;"
    /// </summary>
    void CustomReceiveData(string topic, Dictionary<string, object> dictionary, byte[] thirdFrame = null)
    {
        //countdown for the confidence
        countDown -= Time.deltaTime;
        //reading the pupil topic of the left eye
        if (topic.StartsWith("pupil.1"))
        {
            //parsing the topic
            foreach (var item in dictionary)
            {
                //switch between the different variable available in the given topic
                switch (item.Key)
                {
                    //at confidence when the countdown is under 0 save the confidence value and show it to the user
                    case "confidence":
                        if (countDown < 0)
                            confidence1 = PupilTools.FloatFromDictionary(dictionary, item.Key);
                        lconf.text = "Left confidence\n" + System.Math.Round(confidence1 * 100) + "%";
                        break;
                    //Normalized position of the fixation's centroid, used to show where is the pupil on the frame
                    case "norm_pos":
                        var positionForKey = PupilTools.VectorFromDictionary(dictionary, item.Key);
                        //check if the eye is tracked and the gameobject is still there
                        //if the eye is not tracked the x will be 0 and the y will be 1, 
                        //as it changes from the last tracked position we ignore it
                        if (positionForKey.x != 0 && positionForKey.y != 1 && leftPupil != null)
                        {
                            positionForKey.x -= 0.5f;
                            positionForKey.y -= 0.5f;
                            positionForKey.x *= -1;
                            leftPupil.transform.localPosition = positionForKey;
                            //if the confidence is above 0.6 (60%) the pupil color will be green, else red
                            // above 60% of confidence the position is trusty enough according to the pupil lab doc
                            if (confidence1 > .6)
                            {
                                leftPupil.GetComponent<Renderer>().material.color = Color.green;
                            }
                            else
                            {
                                leftPupil.GetComponent<Renderer>().material.color = Color.red;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        //same as the topic pupil.0
        if (topic.StartsWith("pupil.0"))
        {
            foreach (var item in dictionary)
            {
                switch (item.Key)
                {
                    case "confidence":
                        if (countDown < 0)
                            confidence0 = PupilTools.FloatFromDictionary(dictionary, item.Key);
                        rconf.text = "Right Confidence\n" + System.Math.Round(confidence0 * 100) + "%";
                        break;
                    case "norm_pos":
                        var positionForKey = PupilTools.VectorFromDictionary(dictionary, item.Key);
                        if (positionForKey.x != 0 && positionForKey.y != 1 && rightPupil != null)
                        {
                            positionForKey.x -= 0.5f;
                            positionForKey.y -= 0.5f;
                            positionForKey.y *= -1;
                            rightPupil.transform.localPosition = positionForKey;
                            if (confidence0 > .6)
                            {
                                rightPupil.GetComponent<Renderer>().material.color = Color.green;
                            }
                            else
                            {
                                rightPupil.GetComponent<Renderer>().material.color = Color.red;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        if (countDown < 0)
        {
            //save the average between right and left eye confidence and increment the index
            confArray[arrayIndex] = (confidence0 + confidence1) / 2;
            arrayIndex++;
            //as we only need the last 50 confidence value, reset the index 
            if (arrayIndex == 50)
                arrayIndex = 0;
            //reset the countdown
            countDown = refreshTime;
        }
    }

    /// <summary>
    /// unsubscribe from the topics
    /// </summary>
    void OnDisable()
    {
        PupilTools.OnConnected -= StartPupilSubscription;
        PupilTools.OnDisconnecting -= StopPupilSubscription;
        PupilTools.OnReceiveData -= CustomReceiveData;
    }
}
