using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalMovement : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    // Start is called before the first frame update
    public void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public void addForce()
    {
        rb.AddForce(new Vector3(5, 5, 5));

    }

    public void Update()
    {
        
        //transform.position += new Vector3(5, 5, 5);
        //rb.AddForce(transform.forward * 10f * Time.deltaTime * 50 - rb.velocity, ForceMode.VelocityChange);
    }
    private void FixedUpdate()
    {
        
    }

}
