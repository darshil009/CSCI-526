using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LevelSelectScript : MonoBehaviour
{
    public int level;
    public Text levelText;

    private string path="Final_Levels/";
    void Start()
    {
        levelText.text = level.ToString();
    }

    public void OpenScene()
    {
        string levelString="L";
        if(level<10)
            levelString+="0";
        levelString+=level.ToString();
        SceneManager.LoadScene(levelString);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
