using UnityEngine;

namespace Enemy
{
    public class Health : EnemyFollow
    {
        // Start is called before the first frame update
        private EnemyFollow follow;
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
                playerScript.decreaseHealth(10);
            }
            Destroy(gameObject);
        }
    }
}
