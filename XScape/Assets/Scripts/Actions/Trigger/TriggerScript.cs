using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    [SerializeField] private Material triggerActiveMat;
    [SerializeField] private Material triggerInActiveMat;

    public delegate void TriggerActive();
    public delegate void TriggerInActive();
    public static event TriggerActive triggerActiveSub;
    public static event TriggerInActive triggerInActiveSub;

    public bool isActive = false; //private
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material = triggerInActiveMat;        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        triggerActiveSub();
        isActive = true;
        GetComponent<Renderer>().material = triggerActiveMat;
    }

    private void deActivateTrigger(){
        triggerInActiveSub();
        isActive = false;
        GetComponent<Renderer>().material = triggerInActiveMat;
    }
}
