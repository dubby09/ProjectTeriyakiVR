using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanCollisionController : MonoBehaviour
{
    [SerializeField]
    float panDamage = 10.0f;
    float panKnockbackModifier = 25.0f;
    AudioSource source;
    SimpleEnemy enemy;
    EnemyHealthBar enemyHealthBar;
    Rigidbody panRB;

    void Start()
    {
        source = GetComponent<AudioSource>();
        panRB = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            source.Play();
            enemy = collision.gameObject.GetComponent<SimpleEnemy>();
            enemy.health -= panDamage;
            enemyHealthBar = collision.gameObject.GetComponentInChildren<EnemyHealthBar>();
            enemyHealthBar.UpdateHealthBar(enemy);
            Collider hitCollider = ClosestColliderToPan(collision);
            hitCollider.gameObject.GetComponent<Rigidbody>().velocity = panRB.velocity * panKnockbackModifier;
        }
    }

    Collider ClosestColliderToPan(Collision c)
    {
        Collider bestCollider = null;
        float bestDistance = -1;
        Collider[] colliders = c.gameObject.GetComponent<RagdollController>().ragdollColliders;
        foreach (Collider collider in colliders)
        {
            float distance = Vector3.Distance(transform.position, collider.transform.position);

            if (distance < bestDistance || bestDistance == -1)
            {
                bestDistance = distance;
                bestCollider = collider;
            }
        }
        return bestCollider;
    }
}
