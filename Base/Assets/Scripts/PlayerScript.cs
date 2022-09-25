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
    [SerializeField] private float  itemsRadius = 3f;
    [SerializeField] private LayerMask itemMask;

    public static float PlayerHealth;
    public static float playerSpeed;

    private Vector3 _playerVelocity;
    private SentToGoogle sg;
    private CharacterController _controller;
    private InventoryManager _inventoryManager;
    private bool isInCollision = false;
    private Collider collidedWith = null;

    private bool _groundedPlayer;
    private float maxSpeed;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;

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
        GameDetails.currentTotalWeight = 0;
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
            //  Debug.DrawRay(ray.origin,ray.direction*100,Color.green);

        if(Input.GetMouseButtonDown(0))
        {   Debug.Log("Hit left mouse");
            if (Physics.Raycast(ray.origin,ray.direction, out hit,GameDetails.pickDropDistance,itemMask)){
                Debug.Log("Hit " + hit.transform.gameObject.name + " "+hit.point);
                if(hit.transform.GetComponent<Light>().enabled){
                    
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
        if (PlayerHealth <= 0)
        {
            analyticsManager.RegisterEvent(GameEvent.HEALTH_LOST, PlayerHealth);
            IDictionary<string, string> analytics = analyticsManager.Publish();
            // Debug.Log(analytics["level"] + " " + analytics["time"] + " " + analytics["health"] + " " + tw);
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
        if (c.CompareTag("Bullet"))
        {
            decreaseHealth(10);
            Destroy(c.gameObject);
        }
    }
    public InventoryManager getInventoryManager()
    {
        return _inventoryManager;
    }

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
            }
    }

    private void OnItemDrop(object sender, Item item)
    {
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
                default:
                    break;
            }

    }
}
