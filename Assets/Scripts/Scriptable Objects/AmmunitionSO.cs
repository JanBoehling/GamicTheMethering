using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Ammunition")]
public class AmmunitionSO : ScriptableObject
{
    public uint BulletCount = 1;
    public float DamagePerBullet;
    public float BulletDeviation;
}
