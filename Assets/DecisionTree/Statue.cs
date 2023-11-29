using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Statue : MonoBehaviour
{
    [SerializeField] NavMeshAgent statueAgent;
    [SerializeField] Transform playerPosition;
    private void Awake()
    {
        statueAgent.SetDestination(playerPosition.position);
        statueAgent.updateRotation = false;
        transform.rotation = Quaternion.Euler(new Vector3(-90f, 0f, 0f));
    }

    public void IsVisible()
    {
        Debug.Log("Stopped");
        statueAgent.isStopped = true;
    }

    public void IsInvisible() 
    {
        Debug.Log("Moving");
        statueAgent.isStopped = false;
        statueAgent.SetDestination(playerPosition.position);
    }
}
