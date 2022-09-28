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

        WASDTutorial wASDScript = children[0].transform.GetComponent<WASDTutorial>();
        wASDScript.done += incrementIndex;
        PickUpTutorial pickupScript = children[1].transform.GetComponent<PickUpTutorial>();
        pickupScript.done += incrementIndex;
        InventoryTutorial inventoryTutorial = children[2].transform.GetComponent<InventoryTutorial>();
        inventoryTutorial.done += incrementIndex;
        EscapeTutorial escapeTutorial = children[3].transform.GetComponent<EscapeTutorial>();
        escapeTutorial.done+=incrementIndex;
        //Uncommenting this causes a block dont know why
        // for(int i=0;i<children.Length;i++)
        // children[i].SetActive(false);
        StartCoroutine(DisplayTutorial());
    }
    IEnumerator DisplayTutorial()
    {
        while(index<children.Length){
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

            }
            yield return null;
        }
        yield break;
    }

    public void OnFirstLight(Transform transform)
    {
        Debug.Log("On first light on");
        firstLightOn =true;
        lightItemTransform = transform;
    }


    private void incrementIndex(object sender, bool done)
    {
        Debug.Log("Incrementing");
        if(done)
        {
            index++;
        }
    }

    
}
