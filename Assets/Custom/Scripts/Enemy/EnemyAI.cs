using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
	public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange = 8.0f;
    public NavMeshAgent agent;
    public Transform player;
    public Animator animator;
    public LayerMask whatIsGround, whatIsPlayer;

    // states
    public float sightRange, attackRange;
    private bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // check a sphere around the enemy for  the player
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();

        //animator.SetBool("moving", agent.isStopped);
        //if (playerInSightRange && playerInAttackRange) AttackPLayer();
    }

    private void Patrolling()
    {
        agent.speed = 1.5f;
        // if no walk point is set, calls function to generate a new one
        if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet) agent.SetDestination(walkPoint);

        // calculate the distance to the target walk point
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // use calculated distance to check when the enemy reaches the walk point, and reset the walk point
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        Vector3 newPoint;
        if (RandomPoint(transform.position, walkPointRange, out newPoint))
        {
            walkPointSet = true;
            walkPoint = newPoint;
            Debug.DrawRay(newPoint, Vector3.up, Color.blue, 1.0f);
        }

    }

    public float range = 10.0f;
	bool RandomPoint(Vector3 center, float range, out Vector3 result)
	{
		for (int i = 0; i < 30; i++)
		{
			Vector3 randomPoint = center + Random.insideUnitSphere * range;
			NavMeshHit hit;
			if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
			{
				result = hit.position;
				return true;
			}
		}
		result = Vector3.zero;
		return false;
	}

    void ChasePlayer()
    {
        agent.speed = 2.0f;
        agent.SetDestination(player.position);
    }
}
