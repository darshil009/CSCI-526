
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weightIdenity : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        void OnCollisionEnter(Collision col)
        {
            int weight = 0;

            if (col.gameObject.name == "Block1LB(Clone)")
            {
                UnityEngine.Debug.Log("message");
                weight+=1;
            }
        }
        
    }


}
