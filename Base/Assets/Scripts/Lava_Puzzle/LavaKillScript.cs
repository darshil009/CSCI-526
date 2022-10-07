using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaKillScript : MonoBehaviour
{
    public GameObject PlayerSidePlank;
    public GameObject WallSidePlank;

    //// Start is called before the first frame update
    void Start()
    { 
    
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider c)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (c.CompareTag("2lb"))
        {
            PlayerSidePlank.transform.position = new Vector3(-57, -0.2f, 13);
            Destroy(c.gameObject);
        }
        else if (c.CompareTag("3lb"))
        {
            WallSidePlank.transform.position = new Vector3(-57, 0, 19);
            Destroy(c.gameObject);
        }
    }
}
