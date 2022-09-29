using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpeedTutorial : MonoBehaviour
{
    public event EventHandler<bool> done;
    
    private Transform speedPromptTransform;
    private Transform wasdPromptTransform;
    private Transform pointToSpeedBarTransform;
    [SerializeField] private Transform speedAndHealthBarTransform;
    [SerializeField] private GameObject SpeedAndHealthHighlight;

    // Start is called before the first frame update
    void Start()
    {
        wasdPromptTransform = transform.Find("WASDPrompt").transform;  
        speedPromptTransform = transform.Find("SpeedPrompt").transform;  
        pointToSpeedBarTransform = transform.Find("PointToSpeedBar").transform;  
        StartCoroutine(HighlightSpeedBar());
    }
    
    IEnumerator HighlightSpeedBar(){

        transform.gameObject.SetActive(true);
        wasdPromptTransform.gameObject.SetActive(true);
        SpeedAndHealthHighlight.SetActive(false);
        speedPromptTransform.gameObject.SetActive(false);
        pointToSpeedBarTransform.gameObject.SetActive(false);

        while(true){
            if(Input.GetAxis("Horizontal")==0 && Input.GetAxis("Vertical")==0){
                yield return new WaitForSeconds(2.5f);
            }else{
                GameDetails.pause = true;
                wasdPromptTransform.gameObject.SetActive(false);
                speedPromptTransform.gameObject.SetActive(true);
                SpeedAndHealthHighlight.SetActive(true);
                pointToSpeedBarTransform.gameObject.SetActive(true);

                Vector3 pointerPosition = new Vector3(speedAndHealthBarTransform.position.x, speedAndHealthBarTransform.position.y+1f, 
                                                    speedAndHealthBarTransform.position.z);
                pointToSpeedBarTransform.position = Camera.main.WorldToScreenPoint(pointerPosition);

                while(!Input.GetKeyDown(KeyCode.Return)){
                    yield return null;
                }

                pointToSpeedBarTransform.gameObject.SetActive(false);
                speedPromptTransform.gameObject.SetActive(false);
                SpeedAndHealthHighlight.SetActive(false);
                transform.gameObject.SetActive(false);
                GameDetails.canDragFirstItem = true;
                GameDetails.pause = false;
                done?.Invoke(this,true);
                yield break;
            }
              
        }
       /*
        while(!Input.GetKeyDown(KeyCode.Return))
        {
            yield return null;
        }
        SpeedAndHealthHighlight.SetActive(false);
        transform.gameObject.SetActive(false);
        GameDetails.pause = false;
        done?.Invoke(this,true);
        yield break;
        */
        //Debug.Log("speed tutorial coroutine ends");
    }
}
