using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum EnemyStates
{
    Patrolling,
    Idle,
    Ragdoll,
    Recover,
    Chasing,
    Attacking
}

public class EnemyAI : MonoBehaviour
{
    public float walkPointRange = 8.0f;
    public float patrolSpeed = 1.3f;
    public float chaseSpeed = 2.0f;
    public float maxWaitTime = 3.0f;
    public NavMeshAgent agent;
    public Transform player;
    public Animator animator;
    public LayerMask whatIsGround, whatIsPlayer;
    public float sightRange, attackRange;
    private bool playerInSightRange, playerInAttackRange;
    RagdollController ragdollController;
    SimpleEnemy simpleEnemy;

    // animation hashes
    int isPatrollingHash = Animator.StringToHash("isPatrolling");
    int isIdleHash = Animator.StringToHash("isIdle");
    int isChasingHash = Animator.StringToHash("isChasing");
    int isAttackingHash = Animator.StringToHash("isAttacking");
    int isRecoveringHash = Animator.StringToHash("isRecovering");

    // state variables
    EnemyStates currentState = EnemyStates.Patrolling;
    public Vector3 walkPoint;
    bool walkPointSet;
    float idleEndTime;
    bool hasRecovered = true;
    bool ragdollStill = false;

    public EnemyStates CurrentState { set { currentState = value; } }
    public bool RagdollStill { set { ragdollStill = value; } }
    public bool HasRecovered { set { hasRecovered = value; } }

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        ragdollController = GetComponent<RagdollController>();
        idleEndTime = Time.time;
        simpleEnemy = GetComponent<SimpleEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleAnimation();
        CheckSwitchState();
        UpdateCurrentState();
        //Debug.Log(currentState);
    }

    // ====== state methods ======
    void UpdateCurrentState()
    {
        switch (currentState)
        {
            case EnemyStates.Patrolling:
                Patrol();
                break;
            case EnemyStates.Idle:
                Idle();
                break;
            case EnemyStates.Chasing:
                ChasePlayer();
                break;
            case EnemyStates.Attacking:
                AttackPlayer();
                break;
            case EnemyStates.Ragdoll:
                //Ragdoll();
                break;
            case EnemyStates.Recover:
                Recover();
                break;
        }
    }

    void CheckSwitchState()
    {
        switch (currentState)
        {
            case EnemyStates.Patrolling:
                CheckForPlayer();
                if (playerInSightRange
                    && !playerInAttackRange
                    && hasRecovered) currentState = EnemyStates.Chasing;
                else if (playerInSightRange
                    && playerInAttackRange
                    && hasRecovered) currentState = EnemyStates.Attacking;
                else if (idleEndTime >= Time.time
                    && hasRecovered) currentState = EnemyStates.Idle;
                break;

            case EnemyStates.Chasing:
                CheckForPlayer();
                if (!playerInSightRange
                    && !playerInAttackRange
                    && hasRecovered) currentState = EnemyStates.Idle;
                else if (playerInSightRange
                    && playerInAttackRange
                    && hasRecovered) currentState = EnemyStates.Attacking; 
                break;

            case EnemyStates.Attacking:
                CheckForPlayer();
                if (!playerInSightRange
                    && !playerInAttackRange
                    && hasRecovered) currentState = EnemyStates.Patrolling;
                else if (playerInSightRange
                    && !playerInAttackRange
                    && hasRecovered) currentState = EnemyStates.Chasing;
                break;

            case EnemyStates.Idle:
                CheckForPlayer();
                if (playerInSightRange
                    && !playerInAttackRange
                    && hasRecovered) currentState = EnemyStates.Chasing;
                else if (playerInSightRange
                    && playerInAttackRange
                    && hasRecovered) currentState = EnemyStates.Attacking;
                else if (idleEndTime <= Time.time
                    && hasRecovered) currentState = EnemyStates.Patrolling;
                break;

            case EnemyStates.Ragdoll:
                if (ragdollStill)
                {
                    currentState = EnemyStates.Recover;
                    transform.position = ragdollController.GetPositionOfRagdoll().position;
                }
                    break;

            case EnemyStates.Recover:
                if (hasRecovered) currentState = EnemyStates.Idle;
                break;
        }
    }

    void Patrol()
    {
        agent.speed = patrolSpeed;
        // if no walk point is set, calls function to generate a new one
        if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet) agent.SetDestination(walkPoint);

        // calculate the distance to the target walk point
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // use calculated distance to check when the enemy reaches the walk point, and reset the walk point
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
            idleEndTime = Time.time + Random.Range(0, maxWaitTime);
        }
    }

    void Idle()
    {
        agent.SetDestination(transform.position);
    }

    void ChasePlayer()
    {
        walkPointSet = false;
        agent.speed = chaseSpeed;
        agent.SetDestination(player.position);
    }

    void AttackPlayer()
    {

    }

    void Recover()
    {
        if (simpleEnemy.health > 0) // Only recover if has health left
        {
            animator.SetBool(isRecoveringHash, true);
            ragdollController.DisableRagdollMode();
        }
    }

    void CheckForPlayer()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
    }

    void HandleAnimation()
    {
        switch (currentState)
        {
            case EnemyStates.Patrolling:
                animator.SetBool(isPatrollingHash, true);
                animator.SetBool(isIdleHash, false);
                animator.SetBool(isChasingHash, false);
                animator.SetBool(isAttackingHash, false);
                animator.SetBool(isRecoveringHash, false);
                break;
            case EnemyStates.Idle:
                animator.SetBool(isPatrollingHash, false);
                animator.SetBool(isIdleHash, true);
                animator.SetBool(isChasingHash, false);
                animator.SetBool(isAttackingHash, false);
                animator.SetBool(isRecoveringHash, false);
                break;
            case EnemyStates.Chasing:
                animator.SetBool(isPatrollingHash, false);
                animator.SetBool(isIdleHash, false);
                animator.SetBool(isChasingHash, true);
                animator.SetBool(isAttackingHash, false);
                break;
            case EnemyStates.Attacking:
                animator.SetBool(isPatrollingHash, false);
                animator.SetBool(isIdleHash, false);
                animator.SetBool(isChasingHash, false);
                animator.SetBool(isAttackingHash, true);
                break;
            case EnemyStates.Recover:
                animator.SetBool(isPatrollingHash, false);
                animator.SetBool(isIdleHash, false);
                animator.SetBool(isChasingHash, false);
                animator.SetBool(isAttackingHash, false);
                animator.SetBool(isRecoveringHash, true);
                break;
        }

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

    void FinishedRecoverAnimation()
    {
        hasRecovered = true;
    }
}