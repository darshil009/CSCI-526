using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool GameIsPaused = false;
    public List<GameObject> disableItems;
    private SentToGoogle sg;

    [SerializeField] GameObject pauseMenuUI;


    void Start()
    {

        sg = new SentToGoogle();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !GameIsPaused)
        {
            string levelName = StarterAssets.FirstPersonController.sceneName;
            StartCoroutine(sg.Post2(levelName, "replay"));
            int magnetClick1 = MagnetButtonController.magnetClick;
            int platformClick1 = platformMove.movingPlatformClick;
            string levelName1 = StarterAssets.FirstPersonController.sceneName;
            StartCoroutine(sg.Post1(levelName1, magnetClick1, platformClick1));
            string sessionid = StarterAssets.FirstPersonController.sess_id;
            StartCoroutine(sg.Post3(sessionid, levelName));
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        print("Hi");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        foreach (var obj in disableItems)
        {
            obj.SetActive(true);
        }
    }

    void Pause()
    {
        print("Hello");
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.lockState = CursorLockMode.None;
        foreach (var obj in disableItems)
        {
            obj.SetActive(false);
        }
    }

    public void replayLevel()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void openLevelSelect()
    {
        string levelName = StarterAssets.FirstPersonController.sceneName;
        Debug.Log("here------>>>" + levelName);
        StartCoroutine(sg.Post2(levelName, "quit"));
        string sessionid = StarterAssets.FirstPersonController.sess_id;
        StartCoroutine(sg.Post3(sessionid, levelName));
        int magnetClick1 = MagnetButtonController.magnetClick;
        int platformClick1 = platformMove.movingPlatformClick;
        StartCoroutine(sg.Post1(levelName, magnetClick1, platformClick1));
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelSelect");
    }
}
