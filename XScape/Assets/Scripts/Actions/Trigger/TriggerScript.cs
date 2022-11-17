using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    [SerializeField] private Material triggerActiveMat;
    [SerializeField] private Material triggerInActiveMat;

    public delegate void TriggerActive();
    public delegate void TriggerInActive();
    public event TriggerActive triggerActiveSub;
    public event TriggerInActive triggerInActiveSub;

    private bool isActive = false; 
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
        if(!isActive && other.CompareTag("MagnetBlock"))
            activateTrigger();
        
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("trigger exit");
        if(isActive && other.CompareTag("MagnetBlock"))
            deActivateTrigger();
        
    }

    public void activateTrigger(){
        triggerActiveSub();
        isActive = true;
        GetComponent<Renderer>().material = triggerActiveMat;
    }

    public void deActivateTrigger(){
        triggerInActiveSub();
        isActive = false;
        GetComponent<Renderer>().material = triggerInActiveMat;
    }
}
