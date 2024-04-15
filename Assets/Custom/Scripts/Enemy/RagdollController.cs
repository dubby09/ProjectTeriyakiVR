using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RagdollController : MonoBehaviour
{
    CapsuleCollider mainCollider;
    public GameObject rig;
    Animator animator;
    bool _ragdollState;
    EnemyAI enemyAI;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        GetRagdollComponents();
        mainCollider = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        enemyAI = GetComponent<EnemyAI>();
        DisableRagdollMode();
    }

    public bool RagdollState { get { return _ragdollState; } set { _ragdollState = value; } }

    // Update is called once per frame
    void Update()
    {
        if (_ragdollState == true && ragdollRBs[0].IsSleeping())
        {
            enemyAI.RagdollStill = true;
        }
        else
        {
            enemyAI.RagdollStill = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("MeleeWeapon"))
        {
            EnableRagdollMode();
            enemyAI.CurrentState = EnemyStates.Ragdoll;
        }
    }

    Collider[] ragdollColliders;
    Rigidbody[] ragdollRBs;

    void GetRagdollComponents()
    {
        ragdollColliders = rig.GetComponentsInChildren<Collider>();
        ragdollRBs = rig.GetComponentsInChildren<Rigidbody>();
    }

    void EnableRagdollMode()
    {
        enemyAI.HasRecovered = false;
        _ragdollState = true;
        animator.enabled = false;
        agent.enabled = false;

        foreach (Collider collider in ragdollColliders)
        {
            collider.enabled = true;
        }

        foreach (Rigidbody rb in ragdollRBs)
        {
            rb.isKinematic = false;
            rb.WakeUp();
        }

        mainCollider.enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    public void DisableRagdollMode()
    {
        _ragdollState = false;
        animator.enabled = true;
        agent.enabled = true;

        foreach(Collider collider in ragdollColliders)
        {
            collider.enabled = false;
        }

        foreach(Rigidbody rb in ragdollRBs)
        {
            rb.isKinematic = true;
        }

        mainCollider.enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
    }
}
