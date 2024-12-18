using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct AmmoData
{
    public Ammo ammoType;
    public Projectile projectilePrefab;
    public float ammoForcePower;
    public List<Projectile> ammoPool;
}

public class BulletManager : Singleton<BulletManager>
{
    [SerializeField] List<AmmoData> ammoDatas = new List<AmmoData>();
    public float ammoForcePower { get; private set; }
    public Ammo ammoType { get; private set; }

    void Start()
    {
        ammoForcePower = ammoDatas[0].ammoForcePower;
    }

    public Projectile GetCurrentPrefab()
    {
        var ammunitionType = ammoDatas.Find(element => element.ammoType == ammoType);
        return SetCurrentPrefab(ammunitionType);
    }

    Projectile SetCurrentPrefab(AmmoData ammunitionType)
    {
        Projectile foundPrefab = ammunitionType.ammoPool.Find(prefab => !prefab.gameObject.activeInHierarchy);

        if (foundPrefab == null)
        {
            Projectile newObj = Instantiate(ammunitionType.projectilePrefab);
            newObj.gameObject.SetActive(false);
            ammunitionType.ammoPool.Add(newObj);
            return newObj;
        }

        return foundPrefab;
    }

    public void SetAmmoType(Ammo ammo)
    {
        AmmoData ammoData = ammoDatas.Find(data => data.ammoType == ammo);
        ammoForcePower = ammoData.ammoForcePower;
        ammoType = ammo;
    }
}
