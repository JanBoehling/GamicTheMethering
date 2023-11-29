using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootController : MonoBehaviour
{
    [SerializeField] private Transform weaponAnchor;
    [SerializeField] private KeyCode shootKey = KeyCode.Mouse0;
    [SerializeField] private KeyCode aimKey = KeyCode.Mouse1;
    [SerializeField] private List<WeaponSO> weapons;

    [SerializeField] private float zoomFOV = 30f;
    [SerializeField] private float zoomSpeed = 10f;
    private float bulletBaseDeviation;
    private float baseFOV;
    
    private Camera cam;
    private Camera weaponRenderer;

    private Coroutine equipWeaponCO;

    private AudioPlayer shotPlayer;

    [SerializeField] private int currentWeaponIndex;
    private int CurrentWeaponIndex
    {
        get => currentWeaponIndex;
        set
        {
            Debug.Log(value);
            if (currentWeaponIndex == value) return;
            currentWeaponIndex = weapons.Count < value ? value : 0;
            if (equipWeaponCO != null) StopCoroutine(equipWeaponCO);
            equipWeaponCO = StartCoroutine(EquipWeaponCO());
        }
    }

    private void Awake()
    {
        cam = Camera.main;
        weaponRenderer = GameObject.Find("WeaponRenderer").GetComponent<Camera>();
        baseFOV = cam.fieldOfView;

        shotPlayer = transform.Find("ShotPlayer").GetComponent<AudioPlayer>();
    }

    private void Start()
    {
        bulletBaseDeviation = weapons[currentWeaponIndex].Ammo.BulletDeviation;
    }

    private void Update()
    {
        if (Input.GetKeyDown(shootKey) && weapons[currentWeaponIndex] != null)
        {
            weapons[currentWeaponIndex].Shoot();
            shotPlayer.Play();
        }

        if (Input.GetKey(aimKey)) ZoomIn();
        else ZoomOut();
    }

    private void ZoomIn()
    {
        // Zoom in camera
        if (cam.fieldOfView > zoomFOV) cam.fieldOfView -= Time.deltaTime * zoomSpeed;
        else cam.fieldOfView = zoomFOV;

        // Zoom in weapon renderer
        if (weaponRenderer.fieldOfView > zoomFOV + (baseFOV - zoomFOV) * .5f) weaponRenderer.fieldOfView -= Time.deltaTime * zoomSpeed * .5f;
        else weaponRenderer.fieldOfView = zoomFOV + (baseFOV - zoomFOV) * .5f;

        weapons[currentWeaponIndex].Ammo.BulletDeviation = 0f;
    }

    private void ZoomOut()
    {
        // Zoom out camera
        if (cam.fieldOfView < baseFOV) cam.fieldOfView += Time.deltaTime * zoomSpeed;
        else cam.fieldOfView = baseFOV;

        // Zoom out weapon renderer
        if (weaponRenderer.fieldOfView < baseFOV) weaponRenderer.fieldOfView += Time.deltaTime * zoomSpeed * .5f;
        else weaponRenderer.fieldOfView = baseFOV;

        weapons[currentWeaponIndex].Ammo.BulletDeviation = bulletBaseDeviation;
    }

    private IEnumerator EquipWeaponCO()
    {
        if (CurrentWeaponIndex >= weapons.Count) yield return null;

        yield return new WaitForSeconds(1f);

        Destroy(weaponAnchor.GetChild(0).gameObject);
        Instantiate(weapons[CurrentWeaponIndex], weaponAnchor);

        bulletBaseDeviation = weapons[CurrentWeaponIndex].Ammo.BulletDeviation;
    }
}
