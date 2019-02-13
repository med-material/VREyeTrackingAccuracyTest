using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

//deprecated script, use PupilLabData script
public class PupilInfo : MonoBehaviour
{

    public Text lconf;
    public Text rconf;
    public Slider ConfidenceBarSlider;
    private List<float> ConfidenceList = new List<float>();
    public float refreshTime;
    private float countDown;

    public static float confidence0, confidence1, gazeConfidence;


    // Use this for initialization
    void Start()
    {
        ConfidenceBarSlider.minValue = 0;
        ConfidenceBarSlider.maxValue = 1;
        // PupilTools.OnConnected += StartPupilSubscription;
        // PupilTools.OnDisconnecting += StopPupilSubscription;
        PupilTools.SubscribeTo("pupil.");
        PupilTools.SubscribeTo("gaze");
        PupilTools.OnReceiveData += CustomReceiveData;
        countDown = refreshTime;
    }

    void StartPupilSubscription()
    {
        PupilTools.CalibrationMode = Calibration.Mode._2D;
        // PupilTools.SubscribeTo("pupil.");
        // PupilTools.SubscribeTo("gaze");
    }

    void StopPupilSubscription()
    {
        PupilTools.UnSubscribeFrom("pupil.");
        PupilTools.UnSubscribeFrom("gaze");
    }

    void CustomReceiveData(string topic, Dictionary<string, object> dictionary, byte[] thirdFrame = null)
    {
        if (topic.StartsWith("pupil.1"))
        {
            foreach (var item in dictionary)
            {
                switch (item.Key)
                {
                    case "confidence":
                        countDown -= Time.deltaTime;
                        if (countDown < 0)
                        {
                            confidence1 = PupilTools.FloatFromDictionary(dictionary, item.Key);
                            lconf.text = "Left confidence\n" + (confidence1 * 100) + "%";
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        if (topic.StartsWith("pupil.0"))
        {
            foreach (var item in dictionary)
            {
                switch (item.Key)
                {
                    case "confidence":
                        if (countDown < 0)
                        {
                            confidence0 = PupilTools.FloatFromDictionary(dictionary, item.Key);
                            rconf.text = "Right confidence\n" + (confidence0 * 100) + "%";
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        if (topic.StartsWith("gaze"))
        {
            foreach (var item in dictionary)
            {
                switch (item.Key)
                {
                    case "confidence":
                        if (countDown < 0)
                        {
                            gazeConfidence = PupilTools.FloatFromDictionary(dictionary, item.Key);
                            print(gazeConfidence);
                            ConfidenceList.Add(gazeConfidence);
                            countDown = refreshTime;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }

    private float confidenceSum;

    void Update()
    {
        // ConfidenceList.Add(Random.Range(0, 100));
        if (ConfidenceList.Count < 50)
            return;
        confidenceSum = ConfidenceList.Skip(ConfidenceList.Count - 50).Take(50).Average();

        ConfidenceBarSlider.value = confidenceSum;
        print(ConfidenceBarSlider.value);
        ConfBarUpdate();
        countDown = refreshTime;
    }

    void ConfBarUpdate()
    {
        Vector3 currentPos = transform.position;
        if (ConfidenceBarSlider.value < 0.6)
            ConfidenceBarSlider.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().color = Color.red;
        else
            ConfidenceBarSlider.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().color = Color.green;
    }

    void OnDisable()
    {
        PupilTools.OnConnected -= StartPupilSubscription;
        PupilTools.OnDisconnecting -= StopPupilSubscription;
        PupilTools.OnReceiveData -= CustomReceiveData;
    }
}
