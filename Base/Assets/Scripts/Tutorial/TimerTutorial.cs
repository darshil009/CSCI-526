using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class TimerTutorial : MonoBehaviour
{
    public event EventHandler<bool> done;

    [SerializeField] private Transform timerTransform;
    [SerializeField] private GameObject timerHighlight;
    private Transform timerPromptTransform; 
     // private Transform pointToTimerTransform;
   
    void Start()
    {
        Debug.Log("Timer Tutorial");
        timerPromptTransform = transform.Find("TimerPrompt").transform;
        //pointToTimerTransform = transform.Find("PointToTimer").transform;
        StartCoroutine(WaitForKeyPress());
    }

    IEnumerator WaitForKeyPress(){
        GameDetails.pause = true;
        transform.gameObject.SetActive(true);
        timerPromptTransform.gameObject.SetActive(true);
        timerHighlight.SetActive(true);
        //Debug.Log("timer y pos: " +  timerTransform.position.y);
        //Vector3 pointToTimerPosition = new Vector3(timerTransform.position.x-5f, timerTransform.position.y-5f, timerTransform.position.z-5f);
        //pointToTimerTransform.position = pointToTimerPosition;
        //Debug.Log("point y pos: " +  pointToTimerTransform.position.y);

        while(!Input.GetKeyDown(KeyCode.Return))
        {
            yield return null;
        }
        timerPromptTransform.gameObject.SetActive(false);
        transform.gameObject.SetActive(false);
        timerHighlight.SetActive(false);
        GameDetails.pause = false;
        done?.Invoke(this,true);
        yield break;
    }
}
