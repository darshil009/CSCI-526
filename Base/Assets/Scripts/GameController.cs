using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

     // called first
    void OnEnable()
    {
        Debug.Log("GameController: OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {

            Debug.Log("GameController: OnSceneLoaded: " + scene.name);
            GameDetails.reset();
           // Debug.Log(mode);

        }
    // Update is called once per frame
    void Update()
    {
        
    }
    // called when the game is terminated
    void OnDisable()
    {
        Debug.Log("GameController: OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
