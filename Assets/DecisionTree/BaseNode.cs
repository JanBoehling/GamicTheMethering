using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseNode : MonoBehaviour
{
    [SerializeField] float minJumpDistance;
    [SerializeField] float distanceToPlayer;
    [SerializeField] GameObject player;
    [SerializeField] Jump doggoDoesJump;

    private void Update()
    {
        CalculatePlayerDistance();
    }

    private void CalculatePlayerDistance()
    {
        distanceToPlayer = Vector3.Distance(gameObject.transform.position, player.transform.position);
    }

    private void doggoJump()
    {
        if (distanceToPlayer < minJumpDistance)
        {
            doggoDoesJump.DoggoJumpedLOL();
        }
    }

}
