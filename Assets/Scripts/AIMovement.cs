using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour, IEnemy
{
    NavMeshAgent agentZombie;
    GameObject player;
    float timeToStop = 5f;
    float maxTimeInterval = 5f;
    [SerializeField] private int health = 3;

    private void Awake()
    {
        agentZombie = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
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
    }

    private void Update()
    {
        agentZombie.SetDestination(player.transform.position);
        timeToStop -= Time.deltaTime;
        
        //Debug.Log(timeToStop);

        if (timeToStop <= 3)
        {
            //Debug.Log("Time Reached Zero");
            StopMoving();
        }
    }

    private void StopMoving()
    {
            agentZombie.isStopped = true;
            //Debug.Log("StayingStill:" + timeToStop);

            if (timeToStop <= 0)
            {
                //Debug.Log("End freezing");
                timeToStop = maxTimeInterval;
                agentZombie.isStopped = false;
            }
    }

    public void TakeDamage(float damage)
    {
        Health -= (int)damage;
    }
}
