using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    AnalyticsManager analyticsManager;
    private SentToGoogle sg;
    public float timerEnd;
    public float timerStart;
    public bool isReplay = false;
    // private float scrollBar = 1.0f;
    
    public float totalTimeTaken=0.0f;
    LevelSelectScript ls;
    string levelName = StarterAssets.FirstPersonController.sceneName;

    void Start(){
        
        analyticsManager = new AnalyticsManager();
        sg = new SentToGoogle();
        ls = new LevelSelectScript();
        timerEnd = Time.time;
        Debug.Log("in start:"+ls.timerStart+timerStart+timerEnd);
        if(isReplay == true)
        {
            totalTimeTaken = timerEnd - timerStart;
        }
        else
        {
            totalTimeTaken = timerEnd - ls.timerStart;
        }
        
        Debug.Log("level complete on screen"+totalTimeTaken);
        StartCoroutine(sg.Post1(levelName,totalTimeTaken.ToString()));
    }

    // void Update()
    // {
    //     Time.timeScale = scrollBar;
    // }

    public void playNextLevel(){
        Debug.Log(SceneManager.sceneCountInBuildSettings + ". " + SceneManager.GetActiveScene().buildIndex);
        if(SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings-1)
        {
            // ls.timerStart = Time.time;
            // timerEnd = Time.deltaTime;
            // totalTimeTaken = ls.timer - timerEnd;
            // Debug.Log("level complete on screen"+totalTimeTaken);
            // StartCoroutine(sg.Post(levelName,null,totalTimeTaken.ToString()));
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void replayLevel(){
        // timerEnd = Time.time;
        // totalTimeTaken = timerEnd - ls.timerStart;
        // Debug.Log("time on replay level"+totalTimeTaken);
        // StartCoroutine(sg.Post1(levelName,totalTimeTaken.ToString()));
        // ls.timerStart = Time.time;
        isReplay = true;
        timerStart = Time.time;
        Debug.Log("in start:"+ls.timerStart+timerStart+timerEnd);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    public void openLevelSelect(){
        SceneManager.LoadScene("LevelSelect");
    }
}
