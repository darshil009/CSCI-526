using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{

    private int totalTriggersCount;
    private int activeTriggersCount = 0;
    private bool isDoorOpen = false;
   [SerializeField] private static float openCloseHeight = 12f;

    private void OnEnable(){
        TriggerScript.triggerActiveSub += addActiveTriggers;
        TriggerScript.triggerInActiveSub += subtractActiveTriggers;
    }

    private void OnDisable() {
        TriggerScript.triggerActiveSub -= addActiveTriggers;
        TriggerScript.triggerInActiveSub -= subtractActiveTriggers;    
    }
     
    // Start is called before the first frame update
    void Start()
    {
        totalTriggersCount = GameObject.FindGameObjectsWithTag("Trigger").Length;
        activeTriggersCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public void addActiveTriggers(){
        activeTriggersCount += 1;
        if(activeTriggersCount == totalTriggersCount){
            openDoor();
        }

    }
     public void subtractActiveTriggers(){
        activeTriggersCount += -1;
        if(activeTriggersCount == totalTriggersCount-1){
            closeDoor();
        }

    }
    public void openDoor(){
        isDoorOpen = true;
        transform.position += new Vector3(0f, openCloseHeight, 0f);
    }

    public void closeDoor(){
        isDoorOpen = false;
        transform.position -= new Vector3(0f, openCloseHeight, 0f);

    }
}
