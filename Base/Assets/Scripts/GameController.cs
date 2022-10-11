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
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {

            Debug.Log("GameController: OnSceneLoaded: " + scene.name);
            
            if (scene.name == "SampleScene")
            {
                PlayerScript.maxSpeed = 5;
                GameDetails.reset();
            }
            else if (scene.name == "Level1Maze")
            {
                PlayerScript.maxSpeed = 8;
                GameDetails.reset();
            }
            else
            {
                GameDetails.reset();
                PlayerScript.maxSpeed = 8;
                GameDetails.firstLight = true;
                GameDetails.pause = false;
                GameDetails.firstItemPickedUp = true;
                GameDetails.firstItemDropped = true;
                //tutorialEnded = false;
                GameDetails.canDragFirstItem = true;
            }

        }
    // Update is called once per frame
    void Update()
    {
        
    }
    // called when the game is terminated
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
