using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTakeDamage : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshText;
    [SerializeField] int health;
    float invincibleFrame = 3f;
    float maxInvincibleTime = 3f;
    bool invincible = false;
    

    // Start is called before the first frame update
    void Start()
    {
        textMeshText.text = health.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerDead();
        InvincibleTimer();
    }

    public void TakingDamage()
    {
        if (invincible == false)
        {
            health--;
            textMeshText.text = health.ToString();
            invincible = true;
            invincibleFrame = maxInvincibleTime;
        }
    }

    private void PlayerDead()
    {
        if (health == 0)
        {
            Destroy(gameObject);
            Application.Quit();
        }
    }

    private void InvincibleTimer()
    {
        invincibleFrame -= Time.deltaTime;
        if (invincibleFrame <= 0)
        {
            invincible = false;
        }
    }
}
