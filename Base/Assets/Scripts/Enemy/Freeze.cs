using UnityEngine;

public class Freeze : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float freezeDuration;

    private void OnTriggerEnter(Collider other)
    {
        PlayerScript playerScript = other.GetComponent<PlayerScript>();
        if (playerScript!=null)
        {
            Destroy(gameObject);
            playerScript.freezePlayer(freezeDuration);
        }
        
    }
}
