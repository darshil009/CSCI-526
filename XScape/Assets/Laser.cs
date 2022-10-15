using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private LineRenderer lr;

    [SerializeField] private Transform startPoint;
    public static float object1health;
    public static float object2health;
    public static bool isActive;
    DoorScript ds = new DoorScript();

    // Start is called before the first frame update

    public void decreaseHealth(int health, string cubeName)
    {
        if (cubeName == "MagnetBlock1")
        {
            object2health -= health;
            if (object2health == 0)
            {
                Destroy(GameObject.FindWithTag("MagnetBlock1"), 1.0f);
            }
        }
        else if (cubeName == "MagnetBlock")
        {
            object1health -= health;
            if (object1health == 0)
            {
                Destroy(GameObject.FindWithTag("MagnetBlock"), 1.0f);
            }
        }
        
    }


    void Start()
        {
            lr = GetComponent<LineRenderer>();

            isActive = false;
            object1health = 1000;
            object2health = 1000;
        }
        // Update is called once per frame
    void Update()
    {
        lr.SetPosition(0, startPoint.position);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
            if (hit.collider)
            {
                lr.SetPosition(1, hit.point);
            }
            //if (hit.transform.tag == "MagnetBlock")
            //{
            //    //Object.Destroy(gameObject, 2.0f);
            //    Destroy(GameObject.FindWithTag("MagnetBlock"), 10.0f);
            //    Debug.Log("here");

            //}

            if (hit.transform.tag == "MagnetBlock")
            {
            //Object.Destroy(gameObject, 2.0f);
                decreaseHealth(1, "MagnetBlock");

            }
            else if (hit.transform.tag == "MagnetBlock1")
            {
                //Object.Destroy(gameObject, 2.0f);
                decreaseHealth(1, "MagnetBlock1");
            }

        else if (hit.transform.tag == "Trigger1")
        {
            if (isActive == false)
            {

                isActive = true;
            }
            //Object.Destroy(gameObject, 2.0f);

        }

        else
        {
            lr.SetPosition(1, transform.forward * 5000);
        }
                
    }


}
