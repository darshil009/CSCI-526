using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    [SerializeField] private Material triggerActiveMat;
    [SerializeField] private Material triggerInActiveMat;

    [SerializeField] private Material triggerActiveLightMat;
    [SerializeField] private Material triggerInActiveLightMat;

    public delegate void TriggerActive();
    public delegate void TriggerInActive();
    public event TriggerActive triggerActiveSub;
    public event TriggerInActive triggerInActiveSub;
    
    private Transform[] children;
    
    AudioSource audioData;

    private bool isActive = false; 
    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<Renderer>().material = triggerInActiveMat;   

        Transform highlightTransform =  transform.Find("Highlight");
        children = new Transform[highlightTransform.childCount];
        for(int i = 0; i<highlightTransform.childCount; i++)
            children[i] = highlightTransform.GetChild(i);

        updateMaterial();
    }

    void updateMaterial(){
        Material triggerMat = isActive? triggerActiveMat: triggerInActiveMat;
        Material triggerLightMat = isActive? triggerActiveLightMat: triggerInActiveLightMat;

        GetComponent<Renderer>().material = triggerMat;
        foreach(Transform child in children){
            child.GetComponent<Renderer>().material = triggerLightMat;
        }
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
        audioData = GetComponent<AudioSource>();
        audioData.Play(0);
        triggerActiveSub();
        isActive = true;
        updateMaterial();
        //GetComponent<Renderer>().material = triggerActiveMat;
    }

    public void deActivateTrigger(){
        triggerInActiveSub();
        isActive = false;
        updateMaterial();
        //GetComponent<Renderer>().material = triggerInActiveMat;
    }
}
