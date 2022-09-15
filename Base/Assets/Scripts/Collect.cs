using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{

    void Update()
    {
        transform.Rotate(0f, 90* Time.deltaTime, 0f, Space.Self);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
