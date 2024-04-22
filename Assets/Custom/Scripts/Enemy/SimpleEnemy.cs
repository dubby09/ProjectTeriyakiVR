using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{
    public float health;
    public float maxHealth;

    public float attackSpeed = 2.0f;
    public float nextAttack = 0;
    public int strength;

    public bool isDead = false;

    public EnemyHealthBar enemyHealthBar;

    /*
    PlayerInfo playerInfo;
*/
    /*
        private void OnCollisionEnter(Collision collision)
        {
            if (Time.time > nextAttack && collision.gameObject.CompareTag("Player"))
            {
                nextAttack = Time.time + attackSpeed;

                playerInfo.playerHealth -= strength;
            }


        }*/


    public void TakeDamage(float damage)
    {
        health -= damage;
        enemyHealthBar.UpdateHealthBar(this);
        if (health <= 0 && !isDead)
        {
            isDead = false;
            Debug.Log("Killed enemy!");
            // Update enemies left
            FindObjectsOfType<EnemiesLeft>()[0].DecreaseCount();
        }
    }
}
