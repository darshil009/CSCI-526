using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteScript : MonoBehaviour
{
    public void levelCompleteDisplay()
    {
        gameObject.SetActive(true);
    }

    public void ReplayButton()
    {
        SceneManager.LoadScene("Scenes/Level1Maze");
        Time.timeScale = 1;
    }

    public void menuButton()
    {
        SceneManager.LoadScene("Scenes/InstructionsMenu");
        Time.timeScale = 1;
    }
    public void nextLevelButton()
    {

    }

}