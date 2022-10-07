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
        if (c.CompareTag("Enemy_health"))
        {
            PlayerSidePlank.transform.position = new Vector3(-58, 0, 16);
            Destroy(c.gameObject);
        }
        else if (c.CompareTag("Enemy_health_to_block_demo"))
        {
            WallSidePlank.transform.position = new Vector3(-58, 0.2f, 19);
            Destroy(c.gameObject);
        }
        else if (c.CompareTag("Player"))
        {

        }
        else
        {
          Destroy(c.gameObject);
        }
    }
}
