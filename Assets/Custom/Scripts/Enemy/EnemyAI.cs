using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float walkPointRange = 8.0f;
    public float patrolSpeed = 1.3f;
    public float chaseSpeed = 2.0f;
    public float idleTime = 3.0f;
    public NavMeshAgent agent;
    public Transform player;
    public Animator animator;
    public LayerMask whatIsGround, whatIsPlayer;
    public float sightRange, attackRange;
    private bool playerInSightRange, playerInAttackRange;
    RagdollController ragdollController;

    // animation hashes
    int isPatrollingHash = Animator.StringToHash("isPatrolling");
    int isIdleHash = Animator.StringToHash("isIdle");
    int isChasingHash = Animator.StringToHash("isChasing");
    int isAttackingHash = Animator.StringToHash("isAttacking");

    // state variables
    string currentState = "PatrolState";
    public Vector3 walkPoint;
    bool walkPointSet;
    float idleEndTime;
    float ragdollEndTime;
    bool hasRecovered = true;
    bool ragdollStill = false;

    public string CurrentState { set { currentState = value; } }
    public float RagdollEndTime { set { ragdollEndTime = value; } }
    public bool RagdollStill { set { ragdollStill = value; } }

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        ragdollController = GetComponent<RagdollController>();
        idleEndTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        CheckSwitchState();
        CheckForPlayer();
        UpdateCurrentState(currentState);
    }

    // ====== state methods ======
    void UpdateCurrentState(string state)
    {
        switch (state)
        {
            case "PatrolCycle":
                PatrolCycle();
                break;
            case "ChasePlayer":
                ChasePlayer();
                break;
            case "AttackPlayer":
                AttackPlayer();
                break;
            case "Ragdoll":
                Ragdoll();
                break;
            case "Recover":
                Recover();
                break;
            default:
                break;
        }
    }

    void CheckSwitchState()
    {
        if (ragdollController.RagdollState == false)
        {
            if (!playerInSightRange && !playerInAttackRange) PatrolCycle();
            else if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            else if (playerInSightRange && playerInAttackRange) AttackPlayer();
        }
        else
        {
            Ragdoll();
        }
    }

    void PatrolCycle()
    {
        animator.SetBool(isChasingHash, false);
        animator.SetBool(isAttackingHash, false);
        agent.speed = patrolSpeed;
        if (Time.time >= idleEndTime)
        {
            // if no walk point is set, calls function to generate a new one
            if (!walkPointSet) SearchWalkPoint();
            if (walkPointSet)
            {
                agent.SetDestination(walkPoint);
                animator.SetBool(isPatrollingHash, true); 
                animator.SetBool(isIdleHash, false);
            }
            // calculate the distance to the target walk point
            Vector3 distanceToWalkPoint = transform.position - walkPoint;

            // use calculated distance to check when the enemy reaches the walk point, and reset the walk point
            if (distanceToWalkPoint.magnitude < 1f)
            {
                walkPointSet = false;
                animator.SetBool(isIdleHash, true);
                animator.SetBool(isPatrollingHash, false);
                idleEndTime = Time.time + idleTime;
            }
        }
    }

    void ChasePlayer()
    {
        animator.SetBool(isPatrollingHash, false);
        animator.SetBool(isIdleHash, false);
        animator.SetBool(isAttackingHash, false);
        animator.SetBool(isChasingHash, true);
        walkPointSet = false;
        agent.speed = chaseSpeed;
        agent.SetDestination(player.position);
    }

    void AttackPlayer()
    {

    }

    void Ragdoll()
    {
        if (Time.time >= ragdollEndTime && ragdollStill)
        {
            // TODO: transition out of ragdoll with an animation before coninuing normal function
            //currentState = "Recover";
        }
    }

    void Recover()
    {

    }

    void CheckForPlayer()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
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
}