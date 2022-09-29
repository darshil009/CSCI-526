using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WASDTutorial : MonoBehaviour
{
    // Start is called before the first frame update
    public event EventHandler<bool> done;
    void Start()
    {
        StartCoroutine(waitForWASD());
    }

    IEnumerator waitForWASD(){
        transform.gameObject.SetActive(true);
        while(Input.GetAxis("Horizontal")==0 && Input.GetAxis("Vertical")==0)
        {
            yield return null;
        }
        transform.gameObject.SetActive(false);
        done?.Invoke(this,true);
        yield break;
    }
}
