using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    [SerializeField] NavMeshAgent agentZombie;
    [SerializeField] Transform player;
    float timeToStop = 5f;
    float maxTimeInterval = 5f;

    private void Start()
    {
        agentZombie.SetDestination(player.position);
    }

    private void Update()
    {
        agentZombie.SetDestination(player.position);
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
}
