using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    CapsuleCollider mainCollider;
    public GameObject rig;
    Animator animator;
    bool _ragdollState;

    // Start is called before the first frame update
    void Start()
    {
        GetRagdollComponents();
        mainCollider = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
        DisableRagdollMode();
    }

    public bool RagdollState { get { return _ragdollState; } set { _ragdollState = value; } }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("MeleeWeapon"))
        {
            EnableRagdollMode();
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
        _ragdollState = true;
        animator.enabled = false;

        foreach (Collider collider in ragdollColliders)
        {
            collider.enabled = true;
        }

        foreach (Rigidbody rb in ragdollRBs)
        {
            rb.isKinematic = false;
        }

        mainCollider.enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    void DisableRagdollMode()
    {
        _ragdollState = false;
        animator.enabled = true;

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
