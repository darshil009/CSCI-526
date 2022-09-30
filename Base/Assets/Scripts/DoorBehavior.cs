using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorBehavior : MonoBehaviour
{
    private Transform doorTransform;
    private SentToGoogle sg;
    public GameOverScript gameOverScript;
    bool isDoorOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        sg = new SentToGoogle();
        doorTransform = transform.Find("01_low"); 
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if(GameDetails.NumGems.Green == 1 && !isDoorOpen) {
    //        print("door: all clues collected");
    //        doorTransform.Rotate(0f, 0f, -90f, Space.Self);
    //        isDoorOpen = true;
    //    }

    //}

    public void changeDoor()
    {
        if (!isDoorOpen)
        {
            float tv = TimerCounDown.timeValue;
            List<int> tl = TimerCounDown.timeList;
            List<int> hl = TimerCounDown.healthList;
            List<int> wl = TimerCounDown.weightList;
            //Debug.Log("tl =" + tl);

            doorTransform.Rotate(0f, 0f, -90f, Space.Self);
            isDoorOpen = true;
            StartCoroutine(sg.Post("1", tl, hl, wl, "1", "2", null, tv.ToString()));
            print("door: all clues collected");
            //gameOverScript.gameOverDisplay();
        }

    }
}