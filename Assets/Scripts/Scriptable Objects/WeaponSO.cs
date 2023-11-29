using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Weapon")]
public class WeaponSO : ScriptableObject
{
    [Header("Settings")]
    public float Range;

    [Header("Weapon")]
    public GameObject MuzzleFlash;
    public GameObject WeaponPrefab;
    public AmmunitionSO Ammo;

    private Transform bulletAnchor;
    private Camera cam;

    public Transform BulletAnchor => bulletAnchor;

    public void Shoot(System.Action onTargetShotAction = null)
    {
        cam = Camera.main;
        bulletAnchor = WeaponPrefab.transform.Find("BulletAnchor").transform;

        var start = cam.transform.position + bulletAnchor.InverseTransformPoint(bulletAnchor.position);
        var direction = cam.ScreenPointToRay(Input.mousePosition).direction;
        

        for (int i = 0; i < Ammo.BulletCount; i++)
        {
            var deviation = new Vector3()
            {
                x = Random.Range(-Ammo.BulletDeviation, Ammo.BulletDeviation),
                y = Random.Range(-Ammo.BulletDeviation, Ammo.BulletDeviation),
                z = Random.Range(-Ammo.BulletDeviation, Ammo.BulletDeviation),
            };

            Debug.DrawRay(start, (direction + deviation).normalized * Range, Color.red, 5f);

            if (!Physics.Raycast(start, direction + deviation, out var hit, Range, LayerMask.GetMask("Shootable"))) return;
            
            if (!hit.transform.TryGetComponent<DebugEnemyDamage>(out var target)) return;

            target.TakeDamage(Ammo.DamagePerBullet);
            onTargetShotAction?.Invoke();
        }
    }

    
}
