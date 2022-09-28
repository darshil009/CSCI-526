using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventoryTutorial : MonoBehaviour
{

    public event EventHandler<bool> done;
    [SerializeField] InventoryUI inventoryUI;
    private Transform itemSlotTemplateTransform;
    [SerializeField] TutorialManager tutorialManager;

    [SerializeField] PlayerScript playerScript;
    
    private Transform pointerTransform;

        // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;

    [SerializeField] private float speed = 1.0F;


        Vector3 toVec;
        Vector3 fromVec;

    // Start is called before the first frame update
    void Start()
    {
        itemSlotTemplateTransform = inventoryUI.itemSlotTemplate;
        pointerTransform = transform.Find("pointer");
        startTime = Time.time;
        Transform to = playerScript.transform;
        Transform from = itemSlotTemplateTransform;

        toVec  = Camera.main.WorldToScreenPoint(to.position);
        fromVec  = from.position;

        journeyLength = Vector3.Distance(fromVec, toVec);


        StartCoroutine(DrawMousePointer());
    }
    IEnumerator DrawMousePointer()
    {

        GameDetails.pause = true;
        while(!GameDetails.firstItemDropped)
        {
            float distCovered = (Time.time - startTime) * speed;
            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            pointerTransform.position = Vector3.Lerp(fromVec,toVec, fractionOfJourney);
            if(Vector3.Distance(pointerTransform.position,toVec)<0.1)
            {
                fromVec = itemSlotTemplateTransform.position;
                startTime = Time.time;
            }
            yield return null;
        }
        GameDetails.pause = false;
        done?.Invoke(this,true);
    }
}
