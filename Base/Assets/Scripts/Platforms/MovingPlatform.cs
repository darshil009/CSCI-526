using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Waypoints waypoint;

    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;

    private float time, timeElapsed;
    private int targetIndex;

    private Transform previous, target;
    
    // Start is called before the first frame update
    void Start()
    {
        TargetNextWayPoint();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        timeElapsed += Time.deltaTime;
        float pct = timeElapsed / time;
        pct = Mathf.SmoothStep(0, 1, pct);
        transform.position = Vector3.Lerp(previous.position, target.position, pct);
        if (pct >= 1) TargetNextWayPoint();
        if(CompareTag("rotating_platform"))
        {
            transform.Rotate(new Vector3(0,1,0)*Time.deltaTime*rotationSpeed);
        }
    }

    private void TargetNextWayPoint()
    {
        previous = waypoint.getWayPoints(targetIndex);
        targetIndex = waypoint.getNextWayPointIndex(targetIndex);
        target = waypoint.getWayPoints(targetIndex);

        timeElapsed = 0;
        float distance = Vector3.Distance(previous.position, target.position);
        time = distance / speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.SetParent(transform);
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject parent = GameObject.Find("PlayerModel");
        other.transform.SetParent(parent.transform);
    }
}
