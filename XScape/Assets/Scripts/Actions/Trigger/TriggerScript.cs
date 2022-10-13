using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerScript : MonoBehaviour
{
    [SerializeField] private Material triggerActiveMat;
    [SerializeField] private Material triggerInActiveMat;

    [SerializeField] private UnityEvent triggerActiveEvent;
    [SerializeField] private UnityEvent triggerInActiveEvent;

    private bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material = triggerInActiveMat;
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive)
            GetComponent<Renderer>().material = triggerActiveMat;
        else
            GetComponent<Renderer>().material = triggerInActiveMat;

        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger enter");
        activateTrigger();
        
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("trigger exit");
        deActivateTrigger();
        
    }

    private void activateTrigger(){
        triggerActiveEvent.Invoke();
        isActive = true;
        GetComponent<Renderer>().material = triggerActiveMat;
    }

    private void deActivateTrigger(){
        triggerInActiveEvent.Invoke();
        isActive = false;
        GetComponent<Renderer>().material = triggerInActiveMat;
    }
}
