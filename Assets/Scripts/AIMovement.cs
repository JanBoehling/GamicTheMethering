using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    [SerializeField] NavMeshAgent agentZombie;
    [SerializeField] Transform player;


    private void Update()
    {
        agentZombie.SetDestination(player.position);
    }
}
