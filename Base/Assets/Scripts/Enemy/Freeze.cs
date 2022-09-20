using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : MonoBehaviour
{
    // Start is called before the first frame update
    private int freezeDuration;
    void Start()
    {
        //Freeze Duration in seconds
        freezeDuration = 5;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        PlayerScript playerScript = other.GetComponent<PlayerScript>();
        if (playerScript!=null)
        {
            Destroy(gameObject);
            playerScript.freezePlayer(freezeDuration * 1000);
        }
        
    }
}
