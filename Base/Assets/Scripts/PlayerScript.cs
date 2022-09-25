using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System.Linq;

public class PlayerScript : MonoBehaviour
{
    AnalyticsManager analyticsManager;
    private CharacterController _controller;
    private InventoryManager _inventoryManager;
    [SerializeField] private InventoryUI _inventoryUI;

    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private Camera followCamera;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float  itemsRadius = 3f;
    [SerializeField] private LayerMask itemMask;

    private Vector3 _playerVelocity;
    private bool _groundedPlayer;
    private float maxSpeed;
    public static float PlayerHealth;
    public static float playerSpeed;
    private SentToGoogle sg;
    private bool isInCollision = false;
    private Collider collidedWith = null;
    private bool canMove;

    private Item itemInHand;

    private void Awake()
    {
        _inventoryManager = new UnStackedInventoryManager();
        _inventoryUI.SetInventoryManager(_inventoryManager);
        _inventoryManager.SetInventoryUI(_inventoryUI);
        _inventoryManager.itemAddedEvent+=OnItemPickUp;
        _inventoryUI.OnItemRemovedFromUIEvent += OnItemDrop;
        itemInHand = null;
    }


    public Item getItemInHand()
    {
        return itemInHand;   
    }

    public void setItemInHand(Item item)
    {
        if(itemInHand!=null)
        {
            _inventoryManager.AddItem(item);
        }
        itemInHand = item;
        
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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            ray.origin = Camera.main.transform.position;
            // Debug.DrawRay(ray.origin,ray.direction,Color.green);

        if(Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray.origin,ray.direction, out hit,GameDetails.pickDropDistance,itemMask)){
                if(hit.transform.GetComponent<Light>().enabled){
                    Debug.Log("Hit " + hit.transform.gameObject.name + " "+hit.point);
                    Item item = ItemFactory.fromTag(hit.transform.tag);         
                    _inventoryManager.AddItem(item);
                    Destroy(hit.transform.gameObject);
                }

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

    // private void OnTriggerEnter(Collider other) {
    //      Debug.Log("trigger enter");
    //     if(other.tag.EndsWith("lb"))
    //     {
    //         other.GetComponent<Light>().enabled = true;
    //     }
    // }
    // private void OnTriggerExit(Collider other) {
    //     Debug.Log("trigger exit");
    //     if(other.tag.EndsWith("lb"))
    //         other.GetComponent<Light>().enabled = false;
    // }


    private void OnItemPickUp(object sender, Item item)
    {
        switch(item.GetItemType())
        {
            case Item.ItemType.Block05LB:
                GameDetails.currentTotalWeight+=0.5f;
                break;
            case Item.ItemType.Block1LB:
                GameDetails.currentTotalWeight+=1f;
                break;
            case Item.ItemType.Block2LB:
                GameDetails.currentTotalWeight+=2f;
                break;
            case Item.ItemType.Block3LB:
                GameDetails.currentTotalWeight+=3f;
                break;
            case Item.ItemType.Block5LB:
                GameDetails.currentTotalWeight+=5f;
                break;
            default:
                break;
                //Do something
        }
    }

    private void OnItemDrop(object sender, Item item)
    {
        Debug.Log("On item drop");
        switch(item.GetItemType())
        {
            case Item.ItemType.Block05LB:
                GameDetails.currentTotalWeight-=0.5f;
                break;
            case Item.ItemType.Block1LB:
                GameDetails.currentTotalWeight-=1f;
                break;
            case Item.ItemType.Block2LB:
                GameDetails.currentTotalWeight-=2f;
                break;
            case Item.ItemType.Block3LB:
                GameDetails.currentTotalWeight-=3f;
                break;
            case Item.ItemType.Block5LB:
                GameDetails.currentTotalWeight-=5f;
                break;

        }

    }
    public InventoryManager getInventoryManager()
    {
        return _inventoryManager;
    }
}