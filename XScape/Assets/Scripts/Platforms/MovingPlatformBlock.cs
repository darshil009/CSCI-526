using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformBlock : MonoBehaviour
{
    [SerializeField] private Waypoints waypoint;

    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;

    private float time, timeElapsed;
    private int targetIndex;

    private Transform previous, target, first;
    
    // Start is called before the first frame update
    void Start()
    {
        first = waypoint.getWayPoints(0);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target == first) transform.DetachChildren();
        timeElapsed += Time.deltaTime;
        float pct = timeElapsed / time;
        pct = Mathf.SmoothStep(0, 1, pct);
        if (pct >= 1)
        {
            transform.DetachChildren();
            return;
        }
        transform.position = Vector3.Lerp(previous.position, target.position, pct);

    }

    private void OnMouseDown()
    {
        float pct = timeElapsed / time;
        pct = Mathf.SmoothStep(0, 1, pct);
        if (pct < 1)
        {
            return;
        }
        TargetNextWayPoint();
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
        if (other.CompareTag("MagnetBlock") || other.CompareTag("Player"))
            other.transform.SetParent(transform);
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.parent = null;
    }
}
