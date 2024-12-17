using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct AmmoData
{
    public Ammo ammoType;
    public GameObject ammoPrefab;
    public float ammoForcePower;
    public List<GameObject> ammoPool;
}

public class BulletManager : Singleton<BulletManager>
{
    [SerializeField] List<AmmoData> ammoDatas = new List<AmmoData>();
    GameObject ammoPrefab;
    public float ammoForcePower { get; private set; }
    public Ammo ammoType { get; private set; }

    void Start()
    {
        ammoPrefab = ammoDatas[0].ammoPrefab;
        ammoForcePower = ammoDatas[0].ammoForcePower;
    }

    public GameObject GetCurrentPrefab()
    {
        var ammunitionType = ammoDatas.Find(element => element.ammoType == ammoType);
        return SetCurrentPrefab(ammunitionType);
    }

    GameObject SetCurrentPrefab(AmmoData ammunitionType)
    {
        var foundPrefab = ammunitionType.ammoPool.Find(prefab => !prefab.activeInHierarchy);

        if (foundPrefab == null)
        {
            var newObj = Instantiate(ammunitionType.ammoPrefab);
            newObj.SetActive(false);
            ammunitionType.ammoPool.Add(newObj);
            return newObj;
        }

        return foundPrefab;
    }

    public void SetAmmoType(Ammo ammo)
    {
        AmmoData ammoData = ammoDatas.Find(data => data.ammoType == ammo);
        ammoPrefab = ammoData.ammoPrefab;
        ammoForcePower = ammoData.ammoForcePower;
        ammoType = ammo;
    }
}
