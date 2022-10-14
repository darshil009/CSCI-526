using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTriggered : MonoBehaviour
{
    public TriggerScript Triggeractivation;
    // public Transform moveToPoint;
    public float speed;

    private int totalTriggersCount;
    private int activeTriggersCount = 0;
    private bool isPlatform = false;
    [SerializeField] private static float movingHeight = 5f;

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

    public void addActiveTriggers()
    {
        activeTriggersCount += 1;
        if(activeTriggersCount == totalTriggersCount)
        {
            movePlatformUp();
        }

    }
    
    public void subtractActiveTriggers()
    {
        activeTriggersCount += -1;
        if(activeTriggersCount == totalTriggersCount-1){
            movePlatformDown();
        }
    }

    public void movePlatformUp()
    {
        isPlatform = true;
        // float step = speed * Time.deltaTime;
        // if(Triggeractivation.isActive == true)
        // {
            // transform.position = Vector3.MoveTowards(transform.position,moveToPoint.position,step);
        // }
        transform.position += new Vector3(0f, movingHeight, 0f);
        
    }

    public void movePlatformDown(){
        isPlatform = false;
        transform.position -= new Vector3(0f, movingHeight, 0f);
    }
}
