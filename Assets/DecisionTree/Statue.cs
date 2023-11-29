using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Statue : MonoBehaviour
{
    [SerializeField] NavMeshAgent statueAgent;

    public void IsVisible()
    {
        statueAgent.isStopped = true;
    }

    public void IsInvisible() 
    {
        statueAgent.isStopped= false;
    }
}
