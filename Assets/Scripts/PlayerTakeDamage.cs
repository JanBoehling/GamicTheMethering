using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerTakeDamage : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshText;
    [SerializeField] int health;

    // Start is called before the first frame update
    void Start()
    {
        textMeshText.text = health.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakingDamage()
    {

        health--;
        textMeshText.text = health.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            TakingDamage();
        }
    }
}
