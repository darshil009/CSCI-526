using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] public NavMeshAgent navMesh;
        [SerializeField] public Transform player;
        [SerializeField] public LayerMask playerMask;
        [SerializeField] public LayerMask boxMask;
        private float radius;
        private bool canSeePlayer;
        //private Vector3 startPosition;

        //Attacking
        public float sightRange, attackRange;
        public float timeBetweenAttacks;
        bool alreadyAttacked;
        public GameObject projectile;

        void Start()
        {
            //radius = GameDetails.EnemyVisionRadius;
            radius = GameDetails.EnemyVisionRadius * 0.75f;
            canSeePlayer = false;
            navMesh.autoRepath = true;
            //startPosition = transform.position;
            StartCoroutine(CheckIfPlayerVisible());
        }

        private IEnumerator CheckIfPlayerVisible()
        {

            WaitForSeconds wait = new WaitForSeconds(0.1f);

            while (true)
            {
                yield return wait;
                CheckForPlayer();
            }
        }

        private void CheckForPlayer()
        {
            Collider[] rangeCheck = Physics.OverlapSphere(transform.position, radius, playerMask);
            Collider[] collision = Physics.OverlapSphere(transform.position, 0.5f, playerMask);

            if (collision.Length != 0)
            {
                
                GameObject itemObj = Instantiate(InventoryResourceManager.GetPrefab(Item.ItemType.Block05LB)) as GameObject;
                itemObj.GetComponent<BoxCollider>().isTrigger = false;
                itemObj.GetComponent<Rigidbody>().isKinematic = true;
                itemObj.GetComponent<BoxCollider>().isTrigger = true;
                itemObj.transform.GetComponent<Rigidbody>().isKinematic = false;
                itemObj.transform.GetComponent<Rigidbody>().velocity = new Vector3(0f,0f,0f);
                itemObj.transform.GetComponent<Rigidbody>().angularVelocity = new Vector3(0f,0f,0f);
                itemObj.layer = 6;
                itemObj.transform.position = navMesh.transform.position;
                Destroy(navMesh.GameObject());
            }
            if (rangeCheck.Length != 0)
            {
                float distanceToTarget = Vector3.Distance(transform.position, player.position);
                canSeePlayer = distanceToTarget <= radius;
                Debug.Log(canSeePlayer + " " + distanceToTarget + ' ' + radius);
            }
            else canSeePlayer = false;
        }


        // Update is called once per frame
        void Update()
        {
            if (canSeePlayer) ShootGun();
            //else navMesh.SetDestination(startPosition);
        }

        void ShootGun()
        {
            transform.LookAt(player);
            if (!alreadyAttacked)
            {
                //Attack code here
                Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.detectCollisions = true;
                rb.tag = "Bullet";
                rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
                rb.AddForce(transform.up * 8f, ForceMode.Impulse);
                
                Destroy(rb.gameObject, 1);
                
                //End of attack code

                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
                //Destroy(rb);
            }
        }

        private void ResetAttack()
        {
            alreadyAttacked = false;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, sightRange);
        }

    }
}