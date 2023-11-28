using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Jump : MonoBehaviour
{
    float jumpHeight = 50f;
    float jumpForward = 10f;
    private BaseNode baseNode;
    float maxTimerInterval = 3f;

    private void Update()
    {
        Debug.Log("Is called Update");
    }
    public Jump(BaseNode baseNode)
    {
        this.baseNode = baseNode;
    }

    public void DoggoJumpedLOL(Rigidbody dogRB, float distanceToPlayer, float timer)
    {
        Debug.Log("Dog Jumped");
        dogRB.AddRelativeForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
    }
}
