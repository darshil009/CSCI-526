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
        [SerializeField] public LayerMask wallMaks;
        // [SerializeField] public LayerMask boxMask;
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
            
            if (rangeCheck.Length != 0)
            {
                Transform target = rangeCheck[0].transform;
                Vector3 directionToTarget = (target.position - transform.position).normalized;
                float distanceToTarget = Vector3.Distance(transform.position, player.position);
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, wallMaks) &&
                    distanceToTarget <= radius)
                    canSeePlayer = true;
                else canSeePlayer = false;
            }
            else canSeePlayer = false;
        }


        // Update is called once per frame
        void LateUpdate()
        {
            if (canSeePlayer) ShootGun();
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