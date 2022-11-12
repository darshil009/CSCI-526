using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalEntryExit : MonoBehaviour
{
    // Start is called before the first frame update
    public string direction;
    // +x,-x,+y,-y,+z,-z

    float x1,y1,z1,val;
    Vector3 pos;
    string endPortal;

    void Start()
    {

      // Debug.Log(this.gameObject.tag);

      GameObject[] gameObjects;
      gameObjects = GameObject.FindGameObjectsWithTag(this.gameObject.tag);

      Debug.Log(gameObjects);
      
      if(this.gameObject.name=="portalEntry")
      {
        endPortal="portalExit";
      }
      else
      {
        endPortal="portalEntry";
      }

      foreach (GameObject portal in gameObjects)
      {
        if(portal.name==endPortal)
        {
          x1=portal.transform.position.x;
          y1=portal.transform.position.y;
          z1=portal.transform.position.z;
          break;
        }
      }

        // x1=GameObject.Find(endPortal).transform.position.x;
        // y1=GameObject.Find(endPortal).transform.position.y;
        // z1=GameObject.Find(endPortal).transform.position.z;
        // x2,y2,z2=gameObject.Find("PortalExit").transform.position;

        val = (float)2.5;
        if(direction[0]=='-')
        {
          val = -1*val;
        }

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
        if(direction[1]=='x')
        {
          pos=new Vector3(x1+val,y1,z1);
        }
        else if(direction[1]=='y')
        {
          pos=new Vector3(x1,y1+val,z1);
        }
        else if(direction[1]=='z')
        {
          pos=new Vector3(x1,y1,z1+val);
        }

        collision.gameObject.transform.position=pos;
        // GameObject.Find(collision.gameObject.name).transform.position=(x1,y1,z1);
      }

    }
}
