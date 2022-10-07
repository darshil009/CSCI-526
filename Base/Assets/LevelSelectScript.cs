using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectScript : MonoBehaviour
{
    
    public void levelOne()
    {
        SceneManager.LoadScene("Scenes/SampleScene");
    }
    public void levelTwo()
    {
        SceneManager.LoadScene("Scenes/Level1Maze");
    }
}
