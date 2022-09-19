using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Enemy
{
    public class EnemyFollow : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] public NavMeshAgent navMesh;
        [SerializeField] public Transform player;
        [SerializeField] public LayerMask playerMask;
        
        private float radius;
        private bool canSeePlayer;
        void Start()
        {
            radius = GameDetails.EnemyVisionRadius;
            canSeePlayer = false;
            navMesh.autoRepath = true;
            StartCoroutine(CheckIfPlayerVisible());
        }

        private IEnumerator CheckIfPlayerVisible()
        {
            
            WaitForSeconds wait = new WaitForSeconds(0.5f);
            
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
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                canSeePlayer = distanceToTarget <= radius;
                Debug.Log(canSeePlayer + " " + distanceToTarget + ' ' + radius);
            }
        }
        

        // Update is called once per frame
        void Update()
        {
            if (canSeePlayer) MoveToPlayer();
        }

        void MoveToPlayer()
        {
            var position = player.position;
            var newPos = new Vector3(position.x - 0.2f, position.y - 0.2f, position.z - 0.2f);
            navMesh.SetDestination(newPos);
        }
    }
}