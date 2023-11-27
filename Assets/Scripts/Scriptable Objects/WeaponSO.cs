using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Weapon")]
public class WeaponSO : ScriptableObject
{
    [Header("Weapon")]
    public GameObject WeaponPrefab;
    public Transform BulletAnchor;
    public AmmunitionSO Ammo;

    [Header("Settings")]
    public float Range;

    private Camera cam;

    public void Shoot<T>() where T : MonoBehaviour
    {
        cam = Camera.main;
        var direction = cam.ScreenPointToRay(Input.mousePosition).direction;

        for (int i = 0; i < Ammo.BulletCount; i++)
        {
            var deviation = new Vector3()
            {
                x = UnityEngine.Random.Range(-Ammo.BulletDeviation, Ammo.BulletDeviation),
                y = UnityEngine.Random.Range(-Ammo.BulletDeviation, Ammo.BulletDeviation),
                z = UnityEngine.Random.Range(-Ammo.BulletDeviation, Ammo.BulletDeviation),
            };

            if (!Physics.Raycast(BulletAnchor.position, direction + deviation, out var hit, Range, LayerMask.GetMask("Shootable"))) return;

            if (!hit.transform.TryGetComponent<T>(out var target)) return;

            // target.DealDamage(Ammo.DamagePerBullet);
        }
    }
}
