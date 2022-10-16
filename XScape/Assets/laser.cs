using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{
    private LineRenderer lr;
    [SerializeField] private Transform startPoint;
    public static bool isActive;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        isActive = false;
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


        if (hit.transform.tag == "MagnetBlock1")
        {
            Debug.Log("here");
            if (isActive == false)
            {
                var mb1 = GameObject.FindWithTag("MagnetBlock1");
                Destroy(mb1);
                isActive = true;
            }


        }

        else if (hit.transform.tag == "cap1"  || hit.transform.tag == "vp")
        {
            

        }


        else
        {
            lr.SetPosition(1, transform.forward * 5000);
        }
    }
}
