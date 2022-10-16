using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{

    void Start(){
    }

    public void playNextLevel(){
        Debug.Log(SceneManager.sceneCountInBuildSettings + ". " + SceneManager.GetActiveScene().buildIndex);
        if(SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings-1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void replayLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void openLevelSelect(){
        SceneManager.LoadScene("LevelSelect");
    }
}
