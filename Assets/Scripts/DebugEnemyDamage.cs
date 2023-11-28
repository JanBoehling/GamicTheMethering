using UnityEngine;

public class DebugEnemyDamage : MonoBehaviour
{
    public float Health = 3f;

    public void TakeDamage(float damage) => Health -= damage;
}
