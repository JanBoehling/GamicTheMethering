using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Statue : MonoBehaviour, IEnemy
{
    [SerializeField] NavMeshAgent statueAgent;
    [SerializeField] Transform playerPosition;

    [SerializeField] private int health = 3;
    public int Health
    {
        get => health;
        set
        {
            health = value;
            if (health <= 0)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerVision>().enabled = false;
                Destroy(gameObject);
            }
        }
    }

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

    public void TakeDamage(float damage)
    {
        Health -= (int)damage;
    }
}
