using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
     void Update()
    {
        transform.Rotate(0f, 90* Time.deltaTime, 0f, Space.Self);
    }

    void OnTriggerEnter(Collider c){
        print("key: collision");

        Destroy(gameObject);
    }
}
