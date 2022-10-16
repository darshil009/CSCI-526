using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{

    [SerializeField] private List<TriggerScript> triggerList;
    [SerializeField] private static float openCloseHeight = 12f;

    private int activeTriggersCount = 0;
    private bool isDoorOpen = false;

    private void OnEnable(){
        foreach(TriggerScript triggerScript in triggerList){
            triggerScript.triggerActiveSub += addActiveTriggers;
            triggerScript.triggerInActiveSub += subtractActiveTriggers;
        }
        
    }

    private void OnDisable() {

        foreach(TriggerScript triggerScript in triggerList){
            triggerScript.triggerActiveSub -= addActiveTriggers;
            triggerScript.triggerInActiveSub -= subtractActiveTriggers;
        }
    }
     
    // Start is called before the first frame update
    void Start()
    {
        activeTriggersCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public void addActiveTriggers(){
        activeTriggersCount += 1;
        if(activeTriggersCount == triggerList.Count){
            openDoor();
        }

    }
     public void subtractActiveTriggers(){
        activeTriggersCount += -1;
        if(activeTriggersCount == triggerList.Count-1){
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
