using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class BaseNode : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject doggoPosition;
    [SerializeField] Rigidbody rbDog;
    [SerializeField] float speed;
    [SerializeField] float minJumpDistance;
    [SerializeField] float maxJumpDistance;
    Jump doggoDoesJump;
    bool groundCheck = false;
    float distanceToPlayer;
    float timer;

    private void Update()
    {
        doggoDoesJump = new Jump(this);
        CalculatePlayerDistance();
        doggoJump();
    }

    private void CalculatePlayerDistance()
    {
        Debug.Log("Doggo Move");
        distanceToPlayer = Vector3.Distance(gameObject.transform.position, player.transform.position);
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    private void doggoJump()
    {
        Debug.Log("Cool Jump from Doggo");
        if (distanceToPlayer > minJumpDistance && distanceToPlayer < maxJumpDistance && groundCheck == true)
        {
            doggoDoesJump.DoggoJumpedLOL(rbDog, distanceToPlayer, timer);
            groundCheck = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            groundCheck = true;
        }
    }

}
