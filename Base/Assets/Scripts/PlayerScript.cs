using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System.Linq;

public class PlayerScript : MonoBehaviour
{
    PlayerScript playerScript;
    AnalyticsManager analyticsManager;
    
    [SerializeField] private InventoryUI _inventoryUI;
    
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private Camera followCamera;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float jumpButtomGracePeriod;

    public static float PlayerHealth;
    public static float playerSpeed;
    
    private Vector3 _playerVelocity;
    private SentToGoogle sg;
    private CharacterController _controller;
    private InventoryManager _inventoryManager;
    private Collider collidedWith = null;
    
    private bool _groundedPlayer;
    private float maxSpeed;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;
    private bool isInCollision = false;
    
    private bool canMove;

    public PlayerScript()
    {

    }

    private void Awake()
    {
        _inventoryManager = new UnStackedInventoryManager();
        _inventoryUI.SetInventoryManager(_inventoryManager);
        _inventoryManager.SetInventoryUI(_inventoryUI);
    }

    private void Start()
    {
        canMove = true;
        analyticsManager = new AnalyticsManager();
        analyticsManager.Reset(1);
        sg = new SentToGoogle();
        maxSpeed = 5f;
        playerSpeed = 5f;
        PlayerHealth = 100;
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        
        if (canMove)
            playerSpeed = Mathf.Max(0.2f, (maxSpeed - (0.75f * GameDetails.currentTotalWeight)));
        MovePlayer();

         
        if(isInCollision  && Input.GetKeyDown(KeyCode.E)){

        Debug.Log("On trigger with tag "+collidedWith);
        if(collidedWith.CompareTag("05lb"))
        {
            getInventoryManager().AddItem(new Block05());
            GameDetails.currentTotalWeight += 0.5f;
            Destroy(collidedWith.gameObject);
        }
        else if (collidedWith.CompareTag( "1lb"))
        {
            getInventoryManager().AddItem(new Block1());
            GameDetails.currentTotalWeight += 1;
            Destroy(collidedWith.gameObject);

        }
        else if (collidedWith.CompareTag( "2lb"))
        {
            getInventoryManager().AddItem(new Block2());
            GameDetails.currentTotalWeight += 2;
            Destroy(collidedWith.gameObject);
            
        }
        else if (collidedWith.CompareTag( "3lb"))
        {
            getInventoryManager().AddItem(new Block3());
            GameDetails.currentTotalWeight += 3;
            Destroy(collidedWith.gameObject);
        }
        else if (collidedWith.CompareTag( "5lb"))
        {
            getInventoryManager().AddItem(new Block5());
            GameDetails.currentTotalWeight += 5;
            Destroy(collidedWith.gameObject);
        }
        }

    }
    
    public async void freezePlayer(int seconds)
    {
        canMove = false;
        playerSpeed = 0.5f;
        await Task.Delay(seconds);
        canMove = true;
    }
    //

    public void decreaseHealth(int health)
    {
        float tw = Weights.total_weights;
        PlayerHealth -= health;
        Debug.Log("Player Health: " + PlayerHealth);
        if (PlayerHealth <= 0)
        {
            analyticsManager.RegisterEvent(GameEvent.HEALTH_LOST, PlayerHealth);
            IDictionary<string, string> analytics = analyticsManager.Publish();
            Debug.Log(analytics["level"] + " " + analytics["time"] + " " + analytics["health"] + " " + tw);
            // StartCoroutine(sg.Post("1", "2", "3"));
            StartCoroutine(sg.Post(analytics["level"], analytics["time"], analytics["health"], tw.ToString()));
            SceneManager.LoadScene("Scenes/SampleScene");
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

        if (_groundedPlayer)
        {
            lastGroundedTime = Time.time;
        }

        if (Input.GetButton("Jump"))
        {
            jumpButtonPressedTime = Time.time;
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

        if (Time.time - lastGroundedTime <= jumpButtomGracePeriod)
        {
            
            if (Time.time - jumpButtonPressedTime <= jumpButtomGracePeriod)
            {_playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
                jumpButtonPressedTime = null;
                lastGroundedTime = null;
            }
        }

        _playerVelocity.y += gravityValue * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);
    }

   void OnTriggerEnter(Collider c)
    {

        if(c.gameObject.tag.Contains("lb") ){
        isInCollision = true;
        collidedWith = c;
        Debug.Log("Collided with "+collidedWith);
        if (c.CompareTag("Bullet"))
        {
            decreaseHealth(10);
            Destroy(c.gameObject);
        }
        }
    }

    private void OnTriggerExit(Collider c) 
    {

        if(c.gameObject.tag.Contains("lb") ){
        isInCollision = false;
        collidedWith = null;
        }
    }


    public InventoryManager getInventoryManager()
    {
        return _inventoryManager;
    }
}