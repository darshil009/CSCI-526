using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TimerCounDown : MonoBehaviour
{
    
    AnalyticsManager analyticsManager;
    [SerializeField] public static float timeValue;
    public TextMeshProUGUI timerText;
    private SentToGoogle sg;
    public bool isBlink = false;
    public GameOverScript gameOverScript;
    public bool isRoutineCalled = false;
    public static List<int> timeList = new List<int>();
    public static List<int> healthList = new List<int>();
    public static List<int> weightList = new List<int>();
    IDictionary<int, int> timeDict = new Dictionary<int, int>();

    void Start()
    {
        timeValue = 180;
        Time.timeScale = 1;
        analyticsManager = new AnalyticsManager();
        analyticsManager.Reset(1);
        sg = new SentToGoogle();
        timeList.Add(0);
        healthList.Add(100);
        weightList.Add(0);
        timeDict.Add((int)0, 1);
        //timer = Random.Range(minTime, maxTime);
    }

    
    void Update()
    {
        if(!GameDetails.tutorialEnded) return;
        
        if (timeValue > 0)
        {
            if ((int)timeValue % 30 == 0 && !timeDict.ContainsKey(180 - (int)timeValue))
            {
                timeDict.Add(180 - (int)timeValue, 1);
                Debug.Log("here");
                float tw = Weights.total_weights;
                float h = PlayerScript.PlayerHealth;
                timeList.Add(180 - (int)timeValue);
                Debug.Log(180 - (int)timeValue);
                healthList.Add((int)h);
                Debug.Log((int)h);
                weightList.Add((int)tw);
                Debug.Log((int)tw);
                Debug.Log(timeList + " " + healthList + " " + weightList);

            }
            timeValue -= Time.deltaTime;
        }
        else
        {
            float tw = Weights.total_weights;
            timeValue = 0;
            if (isRoutineCalled == false)
            {
                isRoutineCalled = true;
                StartCoroutine(sg.Post("1", timeList, healthList, weightList, "0", "1", (10 - tw).ToString(), null));
            }
            // analyticsManager.RegisterEvent(GameEvent.TIME_UP, timeValue);
            // IDictionary<string, string> analytics = analyticsManager.Publish();
            // Debug.Log(analytics["level"] + " " + analytics["time"] + " " + analytics["health"] + " " + tw);
            // // StartCoroutine(sg.Post("1", "2", "3"));
            // StartCoroutine(sg.Post(analytics["level"], analytics["time"], analytics["health"], tw.ToString()));
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
