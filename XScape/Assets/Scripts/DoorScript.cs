using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{

    private int totalTriggersCount;
    private int activeTriggersCount = 0;
    private bool isDoorOpen = false;

     
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

    public void countActiveTriggers(int add){
        activeTriggersCount += add;
        if(add == +1 && activeTriggersCount == totalTriggersCount){
            openDoor();
        }else if(add == -1 && activeTriggersCount == totalTriggersCount-1){
            closeDoor();
        }

    }
    public void openDoor(){
        isDoorOpen = true;
        transform.position += new Vector3(0f, 8f, 0f);
    }

    public void closeDoor(){
        isDoorOpen = false;
        transform.position -= new Vector3(0f, 8f, 0f);

    }
}
