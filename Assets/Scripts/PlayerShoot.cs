using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
struct AmmoData
{
    public Ammo ammoType;
    public GameObject ammoPrefab;
    public float ammoForcePower;
}

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] Transform gunPointImitation;
    [SerializeField] Camera cam;
    [SerializeField] List<AmmoData> ammoDatas = new List<AmmoData>();
    [SerializeField] TMP_Text AmmoType_text;
    AmmunitionCollision ammunitionCollision;
    InputSystem_Actions action;
    GameObject ammoPrefab;
    float ammoForcePower;

    public Ammo ammoType { get; private set; }

    void Start()
    {
        ammoPrefab = ammoDatas[0].ammoPrefab;
        ammoForcePower = ammoDatas[0].ammoForcePower;
        AmmoType_text.text = "Ammo type:\n" + $"{ammoType}";
    }

    void OnEnable()
    {
        action = InputManager.Instance.inputSystemActions;
        action.Player.Attack.performed += Shoot;
    }

    void Shoot(InputAction.CallbackContext context)
    {

        Vector3 posForAmmo = gunPointImitation.transform.position;
        GameObject ammunition = Instantiate(ammoPrefab, posForAmmo, Quaternion.identity);

        Rigidbody rb = ammunition.GetComponent<Rigidbody>();
        Vector3 angleForShoot = Quaternion.Euler(cam.transform.rotation.eulerAngles.x, cam.transform.rotation.eulerAngles.y, 0.0f) * (Vector3.forward * ammoForcePower);
        rb.AddForce(angleForShoot, ForceMode.Impulse);
    }

    public void SetAmmoType(Ammo ammo)
    {
        AmmoData ammoData = ammoDatas.Find(data => data.ammoType == ammo);
        ammoPrefab = ammoData.ammoPrefab;
        ammoForcePower = ammoData.ammoForcePower;
        ammoType = ammo;
        AmmoType_text.text = "Ammo type:\n" + $"{ammo}";
    }
    void OnDisable()
    {
        action.Player.Attack.performed -= Shoot;
    }
}