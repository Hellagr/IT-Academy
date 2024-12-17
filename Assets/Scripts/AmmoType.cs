using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum Ammo
{
    Ball,
    Pistol,
    Grenade
}

public class AmmoType : MonoBehaviour
{
    [SerializeField] private List<Ammo> ammoList;
    [SerializeField] TMP_Text AmmoType_text;

    void Start()
    {
        AmmoType_text.text = "Ammo type:\n" + $"{BulletManager.Instance.ammoType}";
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            BulletManager.Instance.SetAmmoType(ammoList[0]);
            AmmoType_text.text = "Ammo type:\n" + $"{ammoList[0]}";
        }
    }
}
