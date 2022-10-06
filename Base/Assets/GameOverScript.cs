using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public void gameOverDisplay()
    {
        gameObject.SetActive(true);
    }

    public void RestartButton()
    {
        Time.timeScale = 1;
        Debug.Log("Restart button pressed");
        Debug.Log(Time.timeScale);
        SceneManager.LoadScene("Scenes/Level1Maze");
        
    }

    public void menuButton()
    {
        SceneManager.LoadScene("Scenes/InstructionsMenu");
        Time.timeScale = 1;
    }

}
