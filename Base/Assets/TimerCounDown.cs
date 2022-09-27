using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TimerCounDown : MonoBehaviour
{
    
    AnalyticsManager analyticsManager;
    [SerializeField] public float timeValue = 180;
    public TextMeshProUGUI timerText;
    private SentToGoogle sg;
    public bool isBlink = false;
    public GameOverScript gameOverScript;

    void Start()
    {
        Time.timeScale = 1;
        analyticsManager = new AnalyticsManager();
        analyticsManager.Reset(1);
        sg = new SentToGoogle();
        //timer = Random.Range(minTime, maxTime);
    }

    
    void Update()
    {
        if (timeValue > 0)
        {
            timeValue -= Time.deltaTime;
        }
        else
        {
            float tw = Weights.total_weights;
            timeValue = 0;
            analyticsManager.RegisterEvent(GameEvent.TIME_UP, timeValue);
            IDictionary<string, string> analytics = analyticsManager.Publish();
            Debug.Log(analytics["level"] + " " + analytics["time"] + " " + analytics["health"] + " " + tw);
            // StartCoroutine(sg.Post("1", "2", "3"));
            StartCoroutine(sg.Post(analytics["level"], analytics["time"], analytics["health"], tw.ToString()));
            //SceneManager.LoadScene("Scenes/SampleScene");
            //analyticsManager.RegisterEvent(GameEvent.TIME_UP, timeValue);
            //analyticsManager.Publish();
            //SceneManager.LoadScene("Scenes/SampleScene");
            Time.timeScale = 0;
            gameOverScript.gameOverDisplay();

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
        if (minutes==0 && seconds>=55)
        {
                timerText.enabled = !timerText.enabled;
                timerText.color = Color.red;

        }
        else if (minutes==0 && seconds<50)
        {
            timerText.enabled = true;
            timerText.color = Color.red;
        }
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }


}
