using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformMoveVertical : MonoBehaviour
{
    private Vector3 target;
    private Vector3 startPos;

    [SerializeField] private bool posDirection = true;
    [SerializeField] private float distance = 7.97f;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Vertical");
        startPos = transform.position;
        target = new Vector3(transform.position.x, transform.position.y + (posDirection ? 1 : -1) * distance, transform.position.z);
    }

    // Update is called once per frame

    private void OnMouseDown()
    {


        Vector3 a = transform.position;
        Vector3 b = target;
        //Vector3 b = target.position;
        if (b == startPos)
        {
            transform.DetachChildren();
        }
        transform.position = b;
        //target.position = a;
        target = a;

    }
    private void OnCollisionEnter(Collision collision)
    {
        collision.transform.parent = transform;
    }
    private void OnCollisionExit(Collision collision)
    {
        collision.transform.parent = null;
    }
}



   



