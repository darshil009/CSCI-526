using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System.Linq;

public class PlayerScript : MonoBehaviour
{
    private CharacterController _controller;
    private InventoryManager _inventoryManager;
    [SerializeField] private InventoryUI _inventoryUI;
    
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private Camera followCamera;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;

    private Vector3 _playerVelocity;
    private bool _groundedPlayer;
    public static float PlayerHealth;
    public static float playerSpeed;
    PlayerScript playerScript;


    private GameObject weightObject = null;
    public string tagname;

    public string[] weight_tags = { "1lb", "2lb", "3lb", "5lb" };
    

    private void Awake()
    {
        _inventoryManager = new UnStackedInventoryManager();
        _inventoryUI.SetInventoryManager(_inventoryManager);
        _inventoryManager.SetInventoryUI(_inventoryUI);
    }

    private void Start()
    {
        playerSpeed = 5f;
        PlayerHealth = 100;
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        
        MovePlayer();

        if (Input.GetKeyDown(KeyCode.E) && weightObject != null)
        {
            if (tagname.Equals("1lb"))
            {
                getInventoryManager().AddItem(new Block1());
                decreaseSpeed(1);
                Destroy(weightObject);
            }
            else if (tagname.Equals("2lb"))
            {
                getInventoryManager().AddItem(new Block2());
                decreaseSpeed(2);
                Destroy(weightObject);
            }
            else if (tagname.Equals("3lb"))
            {
                getInventoryManager().AddItem(new Block3());
                decreaseSpeed(3);
                Destroy(weightObject);
            }
            else if (tagname.Equals("5lb"))
            {
                getInventoryManager().AddItem(new Block5());
                decreaseSpeed(5);
                Destroy(weightObject);
            }
        }
    }

    //
    public async void freezePlayer(int seconds)
    {
        var temp = playerSpeed;
        playerSpeed = 0.5f;
        await Task.Delay(seconds);
        playerSpeed = temp;
    }
    //

    public void decreaseHealth(int health)
    {
        PlayerHealth -= health;
        Debug.Log("Player Health: " + PlayerHealth);
        if (PlayerHealth <= 0) SceneManager.LoadScene("Scenes/SampleScene");
    }

    public void decreaseSpeed(int weight)
    {
        // playerSpeed -= (0.75f*weight);
        if (playerSpeed<0)
        {
            playerSpeed = 0;
        }
    }
    
    public void IncreaseSpeed(int weight)
    {
        playerSpeed += (0.75f*weight);
        if (playerSpeed > 5)
        {
            playerSpeed = 5;
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

    void OnTriggerEnter(Collider c)
    {

        if (c.CompareTag("Bullet"))
          {
              decreaseHealth(10);
              Destroy(c.gameObject);
          }

          if(weight_tags.Contains(c.gameObject.tag))
          {
            // playerScript = c.GetComponent<PlayerScript>();
            weightObject = c.gameObject;
                tagname = c.gameObject.tag;
          }
    }
    void OnTriggerExit(Collider c)
    {
        if (weight_tags.Contains(c.gameObject.tag))
        {
            weightObject = null;
            tagname = "";
        }
    }


    public InventoryManager getInventoryManager()
    {
        return _inventoryManager;
    }
}