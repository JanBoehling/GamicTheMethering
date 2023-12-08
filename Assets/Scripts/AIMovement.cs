using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour, IEnemy
{
    NavMeshAgent agentZombie;
    GameObject player;
    float timeToStop = 10f;
    float maxTimeInterval = 10f;
    [SerializeField] private int health = 3;
    Animator animatorZombie;
    PlayerTakeDamage playerDamage;

    private void Awake()
    {
        animatorZombie = GetComponent<Animator>();
        agentZombie = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerDamage = player.GetComponent<PlayerTakeDamage>();
    }
    public int Health
    { 
        get => health;
        set
        {
            health = value;
            if (health <= 0) Destroy(gameObject);
        }
    }

    private void Start()
    {
        agentZombie.SetDestination(player.transform.position);
        animatorZombie.SetInteger("e", 1);
        Attack();
    }

    private void Update()
    {
        agentZombie.SetDestination(player.transform.position);
        timeToStop -= Time.deltaTime;
        

        if (timeToStop <= 3)
        {
            animatorZombie.SetInteger("e", 0);
            StopMoving();
        }
    }

    /// <summary>
    /// Stop Mopving stops the Zombie every 10 seconds and than starts a countdown till when he may move again 
    /// </summary>
    private void StopMoving()
    {
            agentZombie.isStopped = true;

            if (timeToStop <= 0)
            {
                timeToStop = maxTimeInterval;
                agentZombie.isStopped = false;
                animatorZombie.SetInteger("e", 1);
            }
    }

    public void TakeDamage(float damage)
    {
        Health -= (int)damage;
    }

    private void Attack()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 ownPosition = gameObject.transform.position;
        float attackDistance = 5f;
        float distanceToPlayer = Vector3.Distance(playerPosition, ownPosition);

        if (attackDistance >= distanceToPlayer)
        {
            Debug.Log("Attack");
            animatorZombie.SetInteger("e", 2);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == other.CompareTag("Player"))
        {
            playerDamage.TakingDamage();
        }
    }
}
