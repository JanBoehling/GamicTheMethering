using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class BaseNode : MonoBehaviour, IEnemy
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject doggoPosition;
    [SerializeField] Rigidbody rbDog;
    [SerializeField] float speed;
    [SerializeField] float minJumpDistance;
    [SerializeField] float maxJumpDistance;
    [SerializeField] float directionSpeed;
    Jump doggoDoesJump;
    Run doggoDoesRun;
    float distanceToPlayer;
    float timer;
    float doggoBaseSpeed;
    bool stopRunning = false;
    bool timerFinished = false;
    bool hasJumped = false;
    bool groundCheck = false;

    [SerializeField] private int health = 3;
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
        doggoDoesRun = new Run(this);
        doggoDoesJump = new Jump(this);
        doggoBaseSpeed = speed;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        CalculatePlayerDistance();
        DoggoIsOnTheMove();
        doggoJump();
        RotateTowards();
        
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
                speed += 80f;
                doggoDoesJump.DoggoJumpedLOL(rbDog, distanceToPlayer, timer);
                stopRunning = false;
                groundCheck = false;
                timerFinished = false;
            }
        }
    }

    private void RotateTowards()
    {
        var lookRotation = Quaternion.LookRotation(player.transform.position - doggoPosition.transform.position);
        transform.rotation = Quaternion.Slerp(doggoPosition.transform.rotation, lookRotation, Time.deltaTime * directionSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            groundCheck = true;
            speed = doggoBaseSpeed;
        }
    }

    public void TakeDamage(float damage)
    {
        Health--;
    }
}
