using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject[] children ;
    [SerializeField] private PlayerScript playerScript;
    private bool firstLightOn = false;

    public Transform lightItemTransform;

    private int index;
    
    private void Start() {
        GameDetails.tutorialEnded = false;
        WASDTutorial wASDScript = children[0].transform.GetComponent<WASDTutorial>();
        wASDScript.done += incrementIndex;
        PickUpTutorial pickupScript = children[1].transform.GetComponent<PickUpTutorial>();
        pickupScript.done += incrementIndex;
        
        SpeedTutorial SpeedTutorialScript = children[2].transform.GetComponent<SpeedTutorial>();
        SpeedTutorialScript.done += incrementIndex;

        InventoryTutorial inventoryTutorial = children[3].transform.GetComponent<InventoryTutorial>();
        inventoryTutorial.done += incrementIndex;
        
        TimerTutorial timerTutorial = children[4].transform.GetComponent<TimerTutorial>();
        timerTutorial.done += incrementIndex;

        //EscapeTutorial escapeTutorial = children[5].transform.GetComponent<EscapeTutorial>();
        //escapeTutorial.done+=incrementIndex;
        //Uncommenting this causes a block dont know why
        // for(int i=0;i<children.Length;i++)
        // children[i].SetActive(false);
        
       // Debug.Log("tutorial manager: start: " + GameDetails.tutorialEnded);
        
        StartCoroutine(DisplayTutorial());
    }
    IEnumerator DisplayTutorial()
    {
        while(index<children.Length && !GameDetails.tutorialEnded){
            while(index<children.Length && children[index].activeSelf==true)
                yield return null;
        
            if(index-1>=0)
            children[index-1].SetActive(false);
            switch(index)
            {
                case 0:
                    children[index].SetActive(true);
                    break;
                case 1:
                
                if(lightItemTransform!=null && firstLightOn)
                {
                    firstLightOn = false;
                    children[index].SetActive(true);
                }
                break;
                case 2:
                    children[index].SetActive(true);
                break;
                
                case 3:
                children[index].SetActive(true);
                break;

                case 4:
                children[index].SetActive(true);
                break;
            }
            yield return null;
        }
        GameDetails.tutorialEnded = true;
        yield break;
    }

    public void OnFirstLight(Transform transform)
    {
        Debug.Log("TutorialManager: On first light on");
        firstLightOn =true;
        lightItemTransform = transform;
    }


    private void incrementIndex(object sender, bool done)
    {
        Debug.Log("TutorialManager: Incrementing");
        if(done)
        {
            index++;
        }
    }

    
}
