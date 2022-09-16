using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Oscillator : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3[] coinPositions = {new Vector3(4.08f,0.74f,0f),
        new Vector3(2.48f,-3.52f,0f), 
        new Vector3(-4.19f,0.12f,0f)
        }; 
    private float moveSpeed = 100f;
    private float rotateSpeed = 100f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int index = name[name.Length - 1]-'0'-1;
        transform.RotateAround(coinPositions[index], Vector3.forward, moveSpeed * Time.deltaTime);
        //transform.Translate(Vector3.up*moveSpeed*Time.deltaTime);
        //transform.Rotate(Vector3.forward * Time.deltaTime*rotateSpeed);
        

    }
}