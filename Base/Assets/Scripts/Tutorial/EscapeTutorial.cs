
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class EscapeTutorial : MonoBehaviour
{
 public event EventHandler<bool> done;
    void Start()
    {
        Debug.Log("Escape tut");
        StartCoroutine(WaitForKeyPress());
    }

    IEnumerator WaitForKeyPress(){
        GameDetails.pause = true;
        transform.gameObject.SetActive(true);
        while(!Input.GetKeyDown(KeyCode.Return))
        {
            yield return null;
        }
        transform.gameObject.SetActive(false);
        GameDetails.pause = false;
        done?.Invoke(this,true);
        yield break;
    }
}
