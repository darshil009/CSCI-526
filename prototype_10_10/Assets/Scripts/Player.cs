using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public float PLAYER_SPEED = 3f;
    [SerializeField] private float JUMP_HEIGHT = 1.0f;
    [SerializeField] private float GRAVITY_VALUE = -9.81f;
    [SerializeField] private float JUMP_BUTTON_GRACE_PERIOD = 0.2f;
    [Range(0, 360)] [SerializeField] private float ROTATION_SPEED = 150f;


    private Vector3 playerVelocity;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;
    private CharacterController characterController;
    [SerializeField] private Camera followCamera;

    // Start is called before the first frame update
    private void Start()
    {
        characterController = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Debug.Log("movePlayer called");
        //Prevent double jumps
        if (characterController.isGrounded)
        {
            lastGroundedTime = Time.time;

            if(playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpButtonPressedTime = Time.time;
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementInput = Quaternion.Euler(0, followCamera.transform.eulerAngles.y, 0) *
                                new Vector3(0, 0, verticalInput);

        Vector3 movementDirection = movementInput.normalized;
        
        Debug.Log("movement: " + horizontalInput + ", " + verticalInput);

        characterController.Move(movementDirection * (PLAYER_SPEED * Time.deltaTime));


        Vector3 rotation = new Vector3(0, horizontalInput * ROTATION_SPEED * Time.deltaTime, 0);
        transform.Rotate(rotation);

        if (Time.time - lastGroundedTime <= JUMP_BUTTON_GRACE_PERIOD)
        {

            if (Time.time - jumpButtonPressedTime <= JUMP_BUTTON_GRACE_PERIOD)
            {
                playerVelocity.y += Mathf.Sqrt(JUMP_HEIGHT * -3.0f * GRAVITY_VALUE);
                jumpButtonPressedTime = null;
                lastGroundedTime = null;
            }
        }

        playerVelocity.y += GRAVITY_VALUE * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);

    }
    
}