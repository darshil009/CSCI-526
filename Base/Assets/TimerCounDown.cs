using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerCounDown : MonoBehaviour
{
    AnalyticsManager analyticsManager;
    public float timeValue = 90;
    public Text timerText;
    private SentToGoogle sg;
    // Start is called before the first frame update
    void Start()
    {
        analyticsManager = new AnalyticsManager();
        analyticsManager.Reset(1);
        sg = new SentToGoogle();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeValue > 0)
        {
            timeValue -= Time.deltaTime;
        }
        else
        {
            int tw = Weights.total_weights;
            timeValue = 0;
            analyticsManager.RegisterEvent(GameEvent.TIME_UP, timeValue);
            IDictionary<string, string> analytics = analyticsManager.Publish();
            Debug.Log(analytics["level"] + " " + analytics["time"] + " " + analytics["health"]);
            // StartCoroutine(sg.Post("1", "2", "3"));
            StartCoroutine(sg.Post(analytics["level"], analytics["time"], analytics["health"], tw.ToString()));
            //SceneManager.LoadScene("Scenes/SampleScene");
            //analyticsManager.RegisterEvent(GameEvent.TIME_UP, timeValue);
            //analyticsManager.Publish();
            SceneManager.LoadScene("Scenes/SampleScene");
        }
        DisplayTime(timeValue);
    }

    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }


}
