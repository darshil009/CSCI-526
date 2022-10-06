using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaKillScript : MonoBehaviour
{
    public GameObject PlayerSidePlank;
    public Renderer WallSidePlank;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerSidePlank = GetComponent<Renderer>();
        WallSidePlank = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collider c)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (c.CompareTag("2lb"))
        {
            PlayerSidePlank.transform.position.y = 0.2;
        }
    }
}
