using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalEntry : MonoBehaviour
{
    // Start is called before the first frame update
    float x1,y1,z1;
    Vector3 pos;

    void Start()
    {

        x1=GameObject.Find("portalExit").transform.position.x;
        y1=GameObject.Find("portalExit").transform.position.y;
        z1=GameObject.Find("portalExit").transform.position.z;
        // x2,y2,z2=gameObject.Find("PortalExit").transform.position;

        // Debug.Log(x1);
        // Debug.Log(y1);
        // Debug.Log(z1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
      if(collision.gameObject.tag=="MagnetBlock")
      {
        // Debug.Log(collision.gameObject.name);
        pos=new Vector3(x1-(float)1.9,y1,z1);
        collision.gameObject.transform.position=pos;
        // GameObject.Find(collision.gameObject.name).transform.position=(x1,y1,z1);
      }

    }
}
