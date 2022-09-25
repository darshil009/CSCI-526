using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glow : MonoBehaviour
{


Light light;
    // Start is called before the first frame update
    void Start()
    {
        light = transform.GetComponent<Light>();
        light.enabled = false;
    }

private void Update() {
    Collider[] hitColliders = Physics.OverlapSphere(transform.position,3f);
    bool enable = false;
        foreach (Collider hitCollider in hitColliders)
        {
            if(hitCollider.CompareTag("Player"))
                enable = true;
            //Debug.Log(hitCollider.tag + " "+ hitCollider.gameObject.layer);
            //Debug.Log("Layer mask for boxes"+LayerMask.GetMask(("boxes")));
        }
        light.enabled = enable;
        
}
}
