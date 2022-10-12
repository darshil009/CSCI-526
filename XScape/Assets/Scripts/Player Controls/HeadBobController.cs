using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobController : MonoBehaviour
{

    [SerializeField] private bool enable = true;

    [SerializeField, Range(0, 1f)] private float amplitude = 0.015f;
    [SerializeField, Range(0, 30)] private float frequency = 10.0f;

    [SerializeField] private Transform camera;
    [SerializeField] private Transform cameraHolder;

    private float toggleSpeed = 3.0f;
    private Vector3 startPos;
    private CharacterController controller;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        startPos = camera.localPosition;
    }

    private Vector3 FootSteps()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.deltaTime * frequency) * amplitude;
        pos.x += Mathf.Cos(Time.deltaTime * frequency) * amplitude * 2;
        return pos;
    }

    void ResetPosition()
    {
        if (camera.localPosition == startPos) return;
        camera.localPosition = Vector3.Lerp(camera.localPosition, startPos, 1 * Time.deltaTime);
    }

    void checkMotion()
    {
        float speed = new Vector3(controller.velocity.x, 0, controller.velocity.z).magnitude;
        ResetPosition();
        if (speed < toggleSpeed) return;
        if (!controller.isGrounded) return;

        PlayMotion(FootSteps());
    }

    private Vector3 FocusTarget()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y * cameraHolder.localPosition.y,
            transform.position.z);
        pos += cameraHolder.forward * 15.0f;
        return pos;
    }
    
    private void PlayMotion(Vector3 motion){
        camera.localPosition += motion; 
    }
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!enable) return;
        checkMotion();
        camera.LookAt(FocusTarget());
    }
}
