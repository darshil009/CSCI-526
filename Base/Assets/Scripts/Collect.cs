using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    

    //void Update()
    //{
    //    transform.Rotate(0f, 90* Time.deltaTime, 0f, Space.Self);
    //}
    
    private void OnTriggerEnter(Collider other)
    {
        PlayerScript playerScript = other.GetComponent<PlayerScript>();
        if (playerScript!=null)
        {
            if (gameObject.tag=="1lb")
            {
                playerScript.decreaseSpeed(1);
            }
            else if (gameObject.tag=="2lb")
            {
                playerScript.decreaseSpeed(2);
            }
            else if (gameObject.tag == "3lb")
            {
                playerScript.decreaseSpeed(3);
            }
            else
            {
                playerScript.decreaseSpeed(5);
            }
        }
        Destroy(gameObject);
    }
}
