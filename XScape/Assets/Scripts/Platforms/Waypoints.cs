using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform getWayPoints(int index)
    {
        return transform.GetChild(index);
    }
    
    public int getNextWayPointIndex(int current)
    {
        return (current + 1) % transform.childCount;
    }
}
