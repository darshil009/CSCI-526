using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PickUpTutorial : MonoBehaviour
{
    public event EventHandler<bool> done;

    [SerializeField] private TutorialManager tutorialManager;

    [SerializeField] private PlayerScript playerScript;
    private Transform pickUpPointerTransform;
    private Transform pickUpPrompt1;
    private Transform pickUpPrompt2;

    bool once = false;
    


    private float startTime;

    // Total distance between the markers.
    private float journeyLength;

    private Transform arrowTransform;
    [SerializeField] private float speed = 1.0F;

    
    void Start()
    {
        tutorialManager = transform.parent.GetComponent<TutorialManager>();
        pickUpPrompt1 = transform.Find("PickUpPrompt1").transform;
        pickUpPointerTransform = transform.Find("pointer").transform;
        arrowTransform = transform.Find("arrow").transform;
        
        pickUpPrompt2 = transform.Find("PickUpPrompt2").transform;
        
        //StartCoroutine(DrawMousePointer());
        StartCoroutine(waitForItemPickup());
    
    }


IEnumerator DrawMousePointer()
    {

        Vector3 toVec = Camera.main.WorldToScreenPoint(tutorialManager.lightItemTransform.position);
        Vector3 fromVec = Camera.main.WorldToScreenPoint(playerScript.transform.position);
        pickUpPointerTransform.position = fromVec;
        while(!GameDetails.firstItemDropped)
        {
            float distCovered = (Time.time - startTime) * speed;
            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            pickUpPointerTransform.position = Vector3.Lerp(fromVec,toVec, fractionOfJourney);
            if(Vector3.Distance(pickUpPointerTransform.position,toVec)<0.1)
            {
                fromVec = Camera.main.WorldToScreenPoint(playerScript.transform.position);
                startTime = Time.time;
            }
            yield return null;
        }
    }


    private IEnumerator waitForItemPickup(){
        
        float time = 0f;
        GameDetails.pause = true;
        pickUpPrompt1.gameObject.SetActive(true);
        pickUpPrompt2.gameObject.SetActive(false);
        arrowTransform.position = Camera.main.WorldToScreenPoint(tutorialManager.lightItemTransform.position);
        //pickUpPointerTransform.position = Camera.main.WorldToScreenPoint(playerScript.transform.position);
        while(true){
            time+=Time.deltaTime;
            if(GameDetails.firstItemPickedUp){
                    GameDetails.firstItemPickedUp = false;
                    GameDetails.pause = false;
                    done?.Invoke(this,true); 
                    yield break;
            }
            else{
                if(time>3f){
                pickUpPrompt1.gameObject.SetActive(false);
                pickUpPrompt2.gameObject.SetActive(true);
                }
                yield return null;
            }
        }       
    }
}
