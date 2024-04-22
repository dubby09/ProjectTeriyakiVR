using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    PlayerInfo playerInfo;
    // if this were a more nuanced project, this would instead be a base class "enemy"
    // so that all the different attack colliders would behave properly to their enemy
    // type, but this isn't so lole
    SimpleEnemy enemyInfo;
    public AudioSource source;

    private void Awake()
    {
        playerInfo = GameObject.FindWithTag("Player").GetComponent<PlayerInfo>();
        enemyInfo = gameObject.GetComponentInParent<SimpleEnemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Time.time > enemyInfo.nextAttack && other.gameObject.CompareTag("Player"))
        {
            source.Play();

            enemyInfo.nextAttack = Time.time + enemyInfo.attackSpeed;

            playerInfo.playerHealth -= enemyInfo.strength;

            if (playerInfo.playerHealth <= 0)
            {
                FindObjectOfType<GameManager>().DeathScreen();
            }
        }
    }
}
