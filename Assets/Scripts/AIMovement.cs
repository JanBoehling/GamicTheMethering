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
    float timeToStop = 10f;
    float maxTimeInterval = 10f;
    [SerializeField] private int health = 3;
    [SerializeField] Animator animatorZombie;

    private void Awake()
    {
        //animatorZombie = GetComponent<Animator>();
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
        animatorZombie.SetInteger("e", 1);
    }

    private void Update()
    {
        agentZombie.SetDestination(player.transform.position);
        timeToStop -= Time.deltaTime;
        
        //Debug.Log(timeToStop);

        if (timeToStop <= 3)
        {
            animatorZombie.SetInteger("e", 0);
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
                animatorZombie.SetInteger("e", 1);
            }
    }

    public void TakeDamage(float damage)
    {
        Health -= (int)damage;
    }
}
