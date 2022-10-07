using UnityEngine;

namespace Enemy
{
    public class Health : MonoBehaviour
    {
        // Start is called before the first frame update
         Vector3 startPos;
        void Start()
        {
            startPos = this.transform.position;
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
                this.transform.position = startPos;
                EnemyFollow.canSeePlayer = false;
                // Destroy(gameObject);
            }

            //else if (other.CompareTag("Lava"))
            //{
            //    Destroy(gameObject);
            //}
        }
    }
}
