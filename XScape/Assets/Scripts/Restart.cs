using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    AnalyticsManager analyticsManager;
    public int restart_count=0;
    private SentToGoogle sg;
    LevelSelectScript ls;
    LevelComplete lc;
    float totalTimeTaken;
    float timerEnd;

    private void Start()
    {
        analyticsManager = new AnalyticsManager();
        Debug.Log("In restart file");
        // analyticsManager.Reset(1);
        sg = new SentToGoogle();
        ls = new LevelSelectScript();
        lc = new LevelComplete();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("pressed restart");
            restart_count++;
            string levelName = StarterAssets.FirstPersonController.sceneName;
            StartCoroutine(sg.Post(levelName, restart_count.ToString())); // null, 
            RestartGame();
            Debug.Log("Restarted: "+restart_count);
        }
        
    }
    public void RestartGame()
    {
        timerEnd = Time.time;
        totalTimeTaken = timerEnd - ls.timerStart;
        string levelName = StarterAssets.FirstPersonController.sceneName;
        StartCoroutine(sg.Post1(levelName, totalTimeTaken.ToString()));
        ls.timerStart = Time.time;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}