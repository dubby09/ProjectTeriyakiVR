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
    /*
    PlayerInfo playerInfo;

    private void Awake()
    {
        playerInfo = GameObject.FindWithTag("Player").GetComponent<PlayerInfo>();
    }*/
/*
    private void OnCollisionEnter(Collision collision)
    {
        if (Time.time > nextAttack && collision.gameObject.CompareTag("Player"))
        {
            nextAttack = Time.time + attackSpeed;

            playerInfo.playerHealth -= strength;
        }


    }*/
}
