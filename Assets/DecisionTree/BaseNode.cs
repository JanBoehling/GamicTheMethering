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
    Run doggoDoesRun;
    bool groundCheck = false;
    float distanceToPlayer;
    float timer;
    bool stopRunning = false;
    bool timerFinished = false;

    private void Start()
    {
        doggoDoesRun = new Run(this);
        doggoDoesJump = new Jump(this);
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        CalculatePlayerDistance();
        DoggoIsOnTheMove();
        doggoJump();
        
    }

    private void CalculatePlayerDistance()
    {
        distanceToPlayer = Vector3.Distance(gameObject.transform.position, player.transform.position);
    }

    private void DoggoIsOnTheMove()
    {
        if (stopRunning == false)
        {
            Debug.Log("Doggo Move");
            doggoDoesRun.DoggoMove(distanceToPlayer, player, doggoPosition, speed, timer);
        }

    }

    private void doggoJump()
    {
        if (distanceToPlayer > minJumpDistance && distanceToPlayer <= maxJumpDistance && groundCheck == true)
        {
            stopRunning = true;
            if (timerFinished == false)
            {
                Debug.Log("Timer is Set");
                timer = 3f;
                timerFinished = true;
            }

            if (timer <= 0)
            {
                Debug.Log("Cool Jump from Doggo");
                doggoDoesJump.DoggoJumpedLOL(rbDog, distanceToPlayer, timer);
                stopRunning = false;
                groundCheck = false;
                timerFinished = false;
            }
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
