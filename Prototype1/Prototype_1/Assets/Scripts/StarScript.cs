using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // To rotate an object on its axis
        transform.Rotate(90 * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<PlayerScript>().num_stars++;
        if(other.name=="Player")
        {
            Destroy(gameObject);
        }
    }
}
