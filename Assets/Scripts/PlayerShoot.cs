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
    [SerializeField] BulletCollision bulletCollision;
    [SerializeField] TMP_Text AmmoType_text;
    InputSystem_Actions action;
    GameObject ammoPrefab;
    public Transform parentForEffect { get; private set; }
    public Vector3 positionForEffect { get; private set; }
    public Vector3 rotationForEffect { get; private set; }
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
        CalculateTransformForEffect();

        Vector3 posForAmmo = gunPointImitation.transform.position;
        GameObject ammunition = Instantiate(ammoPrefab, posForAmmo, Quaternion.identity);

        Rigidbody rb = ammunition.GetComponent<Rigidbody>();
        Vector3 angleForShoot = Quaternion.Euler(cam.transform.rotation.eulerAngles.x, cam.transform.rotation.eulerAngles.y, 0.0f) * (Vector3.forward * ammoForcePower);
        rb.AddForce(angleForShoot, ForceMode.Impulse);

        if (ammunition.GetComponent<BulletCollision>() != null)
        {
            BulletCollision bulletCollision = ammunition.GetComponent<BulletCollision>();
            bulletCollision.SetParentObject(parentForEffect);
            bulletCollision.SetPositionForEffect(positionForEffect);
            bulletCollision.SetRotationForEffect(rotationForEffect);
        }

        Destroy(ammunition, 3f);
    }

    void CalculateTransformForEffect()
    {
        RaycastHit hit;
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 surfaceNormal = hit.normal;

            parentForEffect = hit.transform;
            positionForEffect = hit.point;
            rotationForEffect = surfaceNormal;
        }
    }

    void OnDisable()
    {
        action.Player.Attack.performed -= Shoot;
    }

    public void SetAmmoType(Ammo ammo)
    {
        AmmoData ammoData = ammoDatas.Find(data => data.ammoType == ammo);
        ammoPrefab = ammoData.ammoPrefab;
        ammoForcePower = ammoData.ammoForcePower;
        ammoType = ammo;
        AmmoType_text.text = "Ammo type:\n" + $"{ammo}";
    }
}