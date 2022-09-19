using System;
using UnityEngine;
using UnityEngine.AI;

public class ChasePlayerEnemy : MonoBehaviour
{

    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public Vector3 startPosition;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        startPosition = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) ChasePlayer();
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        //if (!playerInSightRange && !playerInAttackRange)
        //{
        //    agent.SetDestination(startPosition);
        //}
    }

    // Start is called before the first frame update
    void Start()
    {

    }
}
