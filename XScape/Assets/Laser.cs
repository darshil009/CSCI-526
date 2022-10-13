using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private LineRenderer lr;

    [SerializeField] private Transform startPoint;

// Start is called before the first frame update
void Start()
    {
        lr = GetComponent<LineRenderer>();

    }
    // Update is called once per frame
    void Update()
    {
        lr.SetPosition(0, startPoint.position);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
            Debug.Log("here");
            if (hit.collider)
            {
                lr.SetPosition(1, hit.point);
            }
            if (hit.transform.tag == "Player")
        {
            Debug.Log("do something");
        }

        else
            {
                lr.SetPosition(1, transform.forward * 5000);
            }
                
    }

}
