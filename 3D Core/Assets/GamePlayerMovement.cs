using UnityEngine;

public class GamePlayerMovement : MonoBehaviour
{
    public CharacterController player;

    public float speed = 6f;

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            player.Move(direction * speed * Time.deltaTime);
        }
    }
}
