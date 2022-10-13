using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTriggered : MonoBehaviour
{
    public TriggerScript Triggeractivation;
    public Transform moveToPoint;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        if(Triggeractivation.isActive == true)
        {
            transform.position = Vector3.MoveTowards(transform.position,moveToPoint.position,step);
        }
    }
}
