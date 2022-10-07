using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JumpTutorial : MonoBehaviour
{
    // Start is called before the first frame update
    public event EventHandler<bool> done;
    void Start()
    {
        StartCoroutine(waitForJump());
    }

    IEnumerator waitForJump(){
        GameDetails.pause = true;
        transform.gameObject.SetActive(true);
        while(!Input.GetKeyDown("space"))
        {
            yield return null;
        }
        transform.gameObject.SetActive(false);
        GameDetails.pause = false;
        done?.Invoke(this,true);
        yield break;
    }
}
