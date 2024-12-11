using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class Tags
{
    public const string PLAYER = "Player";
}

public enum Ammo
{
    Ball,
    Pistol,
    Grenade
}
public class AmmoType : MonoBehaviour
{
    [SerializeField] private List<Ammo> ammoList;
    [SerializeField] PlayerShoot playerShoot;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.PLAYER))
        {
            playerShoot.SetAmmoType(ammoList[0]);
        }
    }
}
