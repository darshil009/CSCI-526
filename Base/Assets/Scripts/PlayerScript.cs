using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;
using static GameDetails;

public class PlayerScript : MonoBehaviour
{
    private CharacterController _controller;
    private InventoryManager _inventoryManager;
    [SerializeField] private InventoryUI _inventoryUI;
    private float playerSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private Camera followCamera;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;

    private Vector3 _playerVelocity;
    private bool _groundedPlayer;

    private void Awake()
    {
        _inventoryManager = new InventoryManager();
        _inventoryUI.SetInventoryManager(_inventoryManager);
    }

    private void Start()
    {
        NumGems.Green = 0;
        NumGems.Blue = 0;
        NumGems.Red = 0;
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        MovePlayer();
    }

    public void decreaseHealth(int health)
    {
        PlayerHealth -= health;
        Debug.Log("Player Health: " + PlayerHealth);
        if (PlayerHealth <= 0) SceneManager.LoadScene("SampleScene");
    }

    public void decreaseSpeed(int weight)
    {
        playerSpeed -= (0.75f*weight);
        if (playerSpeed<0)
        {
            playerSpeed = 0;
        }
    }

    private void MovePlayer()
    {
        //Prevent double jumps
        _groundedPlayer = _controller.isGrounded;
        if (_groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementInput = Quaternion.Euler(0, followCamera.transform.eulerAngles.y, 0) *
                                new Vector3(horizontalInput, 0, verticalInput);
        Vector3 movementDirection = movementInput.normalized;

        _controller.Move(movementDirection * (playerSpeed * Time.deltaTime));

        if (movementDirection != Vector3.zero)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && _groundedPlayer)
        {
            _playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        _playerVelocity.y += gravityValue * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);
    }

    // void OnTriggerEnter(Collider c)
    // {
    //       if(c.CompareTag(SpikeBallTag)){
    //         SceneManager.LoadScene("SampleScene");
    //       }
    //       if (c.CompareTag(BlueGemsTag))
    //       {
    //           NumGems.Blue++;
    //       }
    //       else if (c.CompareTag((RedGemsTag)))
    //       {
    //           NumGems.Red++;
    //       }
    //       else if (c.CompareTag(GreenGemsTag))
    //       {
    //           NumGems.Green++;
    //       }
    //
    //
    //       // Debug.Log("Blue:" + NumGems.Blue + " Red:" + NumGems.Red + " Green:" + NumGems.Green);
    // }
    public InventoryManager getInventoryManager()
    {
        return _inventoryManager;
    }
}