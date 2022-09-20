using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

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

    void OnTriggerEnter(Collider c)
    {
          if(c.CompareTag("Bullet"))
          {
              decreaseHealth(10);
              Destroy(c.gameObject);
          }
    }
    public InventoryManager getInventoryManager()
    {
        return _inventoryManager;
    }
}