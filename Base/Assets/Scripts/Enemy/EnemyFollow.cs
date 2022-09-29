using System.Collections;
using UnityEngine;
using UnityEngine.AI;

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
        private Vector3 startPos;
        private Collider[] results;
        WaitForSeconds wait;
        
        void Start()
        {
            wait = new WaitForSeconds(0.1f);
            results = new Collider[1];
            var multiplier = 1;
            if (navMesh.CompareTag("Enemy_health")) multiplier = 2;
            startPos = navMesh.transform.position;
            radius = GameDetails.EnemyVisionRadius * multiplier;
            canSeePlayer = false;
            navMesh.autoRepath = false;
            StartCoroutine(CheckIfPlayerVisible());
        }

        private IEnumerator CheckIfPlayerVisible()
        {
            while (true)
            {
                CheckForPlayer();
                yield return wait;
            }
        }

        private void CheckForPlayer()
        {
            var size = Physics.OverlapSphereNonAlloc(startPos, radius, results, playerMask);

            if (size!= 0)
            {
                Transform target = results[0].transform;
                float distanceToTarget = Vector3.Distance(startPos, target.position);
                canSeePlayer = distanceToTarget <= radius;
            }
            else canSeePlayer = false;
        }


        // Update is called once per frame
        void Update()
        {
            if (canSeePlayer) MoveToPlayer();
            else navMesh.SetDestination(startPos);
        }

        void MoveToPlayer()
        {
            var position = player.position;
            var newPos = new Vector3(position.x - 0.2f, position.y - 0.2f, position.z - 0.2f);
            navMesh.SetDestination(newPos);
        }

    }
}