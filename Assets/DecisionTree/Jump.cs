using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Jump : MonoBehaviour
{
    float jumpHeight = 20;
    private BaseNode baseNode;
    float maxTimerInterval = 3f;

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
