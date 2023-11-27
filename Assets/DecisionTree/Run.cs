using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : MonoBehaviour
{
    private BaseNode baseNode;

    public Run(BaseNode baseNode)
    {
        this.baseNode = baseNode;
    }

    public void DoggoMove(float distanceToPlayer, GameObject player, GameObject doggo, float speed, float timer)
    {
        doggo.transform.position = Vector3.MoveTowards(doggo.transform.position, player.transform.position, speed * Time.deltaTime);
    }
}
