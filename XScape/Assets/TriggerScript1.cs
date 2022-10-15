using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript1 : MonoBehaviour
{
    DoorScript ds = new DoorScript();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger enter");
        if (other.gameObject.CompareTag("CollideCube1"))
        {
            
            ds.countActiveTriggers(1);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("trigger exit");
        if (other.gameObject.CompareTag("CollideCube1"))
        {

            ds.countActiveTriggers(-1);
        }

    }

}
