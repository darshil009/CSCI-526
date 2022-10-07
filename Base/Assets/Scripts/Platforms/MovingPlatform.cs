using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Waypoints waypoint;

    [SerializeField] private float speed;

    private float time, timeElapsed;
    private int targetIndex;

    private Transform previous, target;
    private Transform prevParent;
    
    // Start is called before the first frame update
    void Start()
    {
        TargetNextWayPoint();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeElapsed += Time.deltaTime;
        float pct = timeElapsed / time;
        pct = Mathf.SmoothStep(0, 1, pct);
        transform.position = Vector3.Lerp(previous.position, target.position, pct);
        if (pct >= 1) TargetNextWayPoint();
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
        prevParent = other.transform.parent;
        other.transform.SetParent(transform);
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.SetParent(prevParent);
    }
}
