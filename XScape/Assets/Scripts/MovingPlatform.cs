using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    public Vector3[] points;
    public int point_number = 0;
    private Vector3 current_target;

    public float tolerance;
    public float speed;
    public float delay_time;

    private float delay_start; // not needed for uni-direction

    public bool automatic; // not needed for uni-direction

    // Start is called before the first frame update
    void Start()
    {
        // transform.position = new Vector3(-80.12f, 0.70f, -5.47f) ; // position coordinates
        Debug.Log(points[0]);
        if(points.Length>0)
        {
            current_target = points[0];
        }
        tolerance = speed * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("current target:"+current_target+" transform.position:"+transform.position); // works on world position coordinates
        if(transform.position != current_target)
        {
            MovePlatform();
        }
        else
        {
            UpdateTarget();
        }
    }

    void MovePlatform()
    {
        Vector3 heading = current_target - transform.position;
        transform.position += (heading / heading.magnitude) * speed * Time.deltaTime;
        if(heading.magnitude < tolerance)
        {
            transform.position = current_target;
            delay_start = Time.time; // not needed for uni-direction
        }
    }

    void UpdateTarget()
    {
        if(automatic)
        {
            if(Time.time - delay_start > delay_time)
            {
                NextPlatform();
            }
        }
    }

    public void NextPlatform()
    {
        point_number++;
        if(point_number >= points.Length)
        {
            point_number = 0;
        }
        current_target = points[point_number];
    }

    // triggered when on top of platform 
    // private void OnTriggerEnter(Collider other)
    // {
    //     other.transform.parent = transform;
    // }

    // private void OnTriggerExit(Collider other)
    // {
    //     other.transform.parent = null;
    // }

}
