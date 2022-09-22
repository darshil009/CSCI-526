
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class weightIdenity : MonoBehaviour
{
    int weight = 0;
    
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

        

        if (other.gameObject.tag == "1lb")
        {
            Debug.Log("dropped 1 lb box");
            weight += 1;
            //txt.text = weight.ToString();
            Debug.Log("printing weight");
            Debug.Log(weight.ToString());
            
        }
        if (other.gameObject.tag == "2lb")
        {
            Debug.Log("dropped 2 lb box");
            weight += 2;
            Debug.Log("weight: " + weight);
         
        }
        else if (other.gameObject.tag == "3lb")
        {
            Debug.Log("dropped 3 lb box");
            weight += 3;
            Debug.Log("weight: " + weight);
        }
        if (other.gameObject.tag == "5lb")
        {
            Debug.Log("dropped 5 lb box");
            weight += 5;
            Debug.Log("weight: " + weight);
           
        }
    }
    
       
    


}
