using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogRadius1 : MonoBehaviour
{
    [SerializeField] float jumpRadius;
    int LeftMouseButton = 0;
    [SerializeField] Rigidbody rb;

    private void Update()
    {     
            Collider[] colliderarray = Physics.OverlapSphere(transform.position, jumpRadius);
            foreach (Collider collider in colliderarray)
            {
                if (collider.CompareTag("Player"))
                {
                    
                }
            }
    }
}
