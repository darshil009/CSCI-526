using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [Header("Velocity Variables")]
    [SerializeField] private float velocityChange = 6.0f;
    [SerializeField] private float jumpChange = 7.0f;

    private Rigidbody _playerRigidBody;

    // Start is called before the first frame update
    private void Start()
    {
        _playerRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");
        var velocity = _playerRigidBody.velocity;
        _playerRigidBody.velocity =
            new Vector3(horizontalInput * velocityChange, velocity.y, verticalInput * velocityChange);
        if (Input.GetButtonDown("Jump"))
        {
            velocity.y = jumpChange;
            _playerRigidBody.velocity = velocity;
        }
    }
}