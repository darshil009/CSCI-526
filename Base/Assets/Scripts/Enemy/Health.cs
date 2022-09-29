using UnityEngine;

namespace Enemy
{
    public class Health : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        
        private void OnTriggerEnter(Collider other)
        {
            PlayerScript playerScript = other.GetComponent<PlayerScript>();
            if (playerScript!=null)
            {
                playerScript.decreaseHealth(30);
                Destroy(gameObject);
            }
            
        }
    }
}
