using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.AI;

public class PlayerScript : MonoBehaviour
{
    PlayerScript playerScript;
    AnalyticsManager analyticsManager;

    [SerializeField] private InventoryUI _inventoryUI;

    [Range(0, 360)] [SerializeField] private float rotationSpeed;
    [SerializeField] private Camera followCamera;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float jumpButtonGracePeriod = 0.2f;
    [SerializeField] private float itemsRadius = 3f;
    [SerializeField] private LayerMask itemMask;
    [SerializeField] private TextMeshProUGUI healthLoss;
    [SerializeField] private NavMeshSurface navMeshSurface;
    [SerializeField] public static float maxSpeed;
    public event EventHandler<Item> firstLightEvent;
    public static float PlayerHealth;
    public static float playerSpeed;
    public bool stopMovement;

    private Vector3 _playerVelocity;
    private SentToGoogle sg;
    private CharacterController _controller;
    private InventoryManager _inventoryManager;
    private bool isInCollision = false;
    private Collider collidedWith = null;
    public static string sess_id;
    private bool _groundedPlayer;
    
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;
    private Vector3 rotation;
    public static int died;
    private bool canMove;

    private Item itemInHand;

    public GameOverScript gameOverScript = new GameOverScript();

    private static DateTime JanFirst1970 = new DateTime(1970, 1, 1);
    public static string getTime()
    {
        return ((long)((DateTime.Now.ToUniversalTime() - JanFirst1970).TotalMilliseconds + 0.5)).ToString();
    }

    private void Awake()
    {
        _inventoryManager = new UnStackedInventoryManager();
        _inventoryUI.SetInventoryManager(_inventoryManager);
        _inventoryManager.SetInventoryUI(_inventoryUI);
        _inventoryManager.itemAddedEvent += OnItemPickUp;
        _inventoryUI.OnItemRemovedFromUIEvent += OnItemDrop;
        itemInHand = null;

        if (PlayerPrefs.HasKey("session_id"))
        {
            sess_id = PlayerPrefs.GetString("session_id");
            Debug.Log("inside if ..." + sess_id);

        }
        else
        {
            sess_id = getTime();

            PlayerPrefs.SetString("session_id", sess_id);
            PlayerPrefs.Save();
            Debug.Log("Inside else..." + sess_id);
        }

    }


    public Item getItemInHand()
    {
        return itemInHand;
    }

    public void setItemInHand(Item item)
    {
        if (itemInHand != null)
        {
            _inventoryManager.AddItem(item);
        }

        itemInHand = item;
    }

    private void Start()
    {
        stopMovement = false;
        canMove = true;
        analyticsManager = new AnalyticsManager();
        analyticsManager.Reset(1);
        sg = new SentToGoogle();
        playerSpeed = maxSpeed;
        PlayerHealth = 100;
        GameDetails.currentTotalWeight = 0;
        _controller = GetComponent<CharacterController>();

        //GameObject.Find("Block2LBfromEnemy").SetActive(false);
    }

    private void Update()
    {

        if (canMove)
            playerSpeed = Mathf.Max(0.2f, (maxSpeed - (0.4f * GameDetails.currentTotalWeight)));
        MovePlayer();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        ray.origin = Camera.main.transform.position;
        //  Debug.DrawRay(ray.origin,ray.direction*100,Color.green);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray.origin, ray.direction, out hit, GameDetails.pickItemDistance, itemMask))
            {
                // Debug.Log("Hit " + hit.transform.gameObject.name + " " + hit.point);
                    if (hit.transform.GetComponent<Light>().enabled)
                    {
                        Item item = ItemFactory.fromTag(hit.transform.tag);
                        _inventoryManager.AddItem(item);
                        Destroy(hit.transform.gameObject);
                    }
            }
        }
    }

    public void freezePlayer(float seconds)
    {
        playerSpeed = 1f;
        canMove = false;
        StartCoroutine(UnFreezePlayer(seconds));
    }

    private IEnumerator UnFreezePlayer(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        canMove = true;
    }

    public void decreaseHealth(int health)
    {
        float tw = Weights.total_weights;
        PlayerHealth -= health;
        displayPopUp(health);
        if (PlayerHealth <= 0)
        {
            List<int> tl = TimerCounDown.timeList;
            List<int> hl = TimerCounDown.healthList;
            List<int> wl = TimerCounDown.weightList;
            // Debug.Log("=====================>>>>>>>>>>>>>>>>>>>>>>>>>");
            // Debug.Log("tl =" + tl);
            //analyticsManager.RegisterEvent(GameEvent.HEALTH_LOST, PlayerHealth);
            //IDictionary<string, string> analytics = analyticsManager.Publish();
            StartCoroutine(sg.Post("1", tl, hl, wl, "0", "3", (10 - tw).ToString(), null, died, sess_id));
            // analyticsManager.RegisterEvent(GameEvent.HEALTH_LOST, PlayerHealth);
            // IDictionary<string, string> analytics = analyticsManager.Publish();
            // StartCoroutine(sg.Post(analytics["level"], analytics["time"], analytics["health"], tw.ToString()));
            Time.timeScale = 0;
            gameOverScript.gameOverDisplay();
            //SceneManager.LoadScene("Scenes/SampleScene");

        }
    }


    private void MovePlayer()
    {

        if (stopMovement) return;
        
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

        if (Input.GetButtonDown("Jump"))
        {
            jumpButtonPressedTime = Time.time;
        }


        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if(GameDetails.pause)
        {
            horizontalInput = verticalInput = 0f;
        }
        Vector3 movementInput = Quaternion.Euler(0, followCamera.transform.eulerAngles.y, 0) *
                                new Vector3(0, 0, verticalInput);
        Vector3 movementDirection = movementInput.normalized;
        _controller.Move(movementDirection * (playerSpeed * Time.deltaTime));


        rotation = new Vector3(0, horizontalInput * rotationSpeed * Time.deltaTime, 0);
        transform.Rotate(rotation);

        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
        {

            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
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
            died = 2;
            Destroy(c.gameObject);
        }
        else if (c.CompareTag("Lava"))
        {
            decreaseHealth(100);
            died = 3;
        }
    }

    public InventoryManager getInventoryManager()
    {
        return _inventoryManager;
    }

    private void OnItemPickUp(object sender, Item item)
    {
        GameDetails.firstItemPickedUp = true;
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
        navMeshSurface.BuildNavMesh();
    }

    private void OnItemDrop(object sender, Item item)
    {
        GameDetails.firstItemDropped = true;
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
            navMeshSurface.BuildNavMesh();

    }

    void displayPopUp(int amt)
    {
        String amount = "-" + amt + " HP";
        healthLoss.text = amount;
        StartCoroutine(disappearPopup());
    }

    public IEnumerator disappearPopup()
    {
        yield return new WaitForSeconds(0.75f);
        healthLoss.text = "";
    }
}
