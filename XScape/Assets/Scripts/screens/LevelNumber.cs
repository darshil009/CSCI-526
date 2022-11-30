using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelNumber : MonoBehaviour
{

    public TextMeshProUGUI levelText;
    
    // Start is called before the first frame update
    void Start()
    {
        levelText.text = "Level : " + (SceneManager.GetActiveScene().buildIndex - 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
