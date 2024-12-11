using System.Collections.Generic;
using UnityEngine;

public class AmmunitionCollision : MonoBehaviour
{
    [SerializeField] List<GameObject> ammunitionCollision;
    [SerializeField] List<float> timeToDestroyAmmo;
    [SerializeField] float timeToDestroyVFX = 3f;
    Transform parentObject;
    Vector3 positionForEffect;
    Vector3 rotationForEffect;

    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        positionForEffect = contact.point;
        rotationForEffect = contact.normal;
        parentObject = collision.transform;

        var effectForCollision = Instantiate(ammunitionCollision[0], positionForEffect, Quaternion.identity);
        effectForCollision.transform.forward = rotationForEffect + new Vector3(0.01f, 0.01f, 0.01f);
        effectForCollision.transform.SetParent(parentObject);
        effectForCollision.GetComponent<ParticleSystem>().Play();

        Destroy(effectForCollision, timeToDestroyVFX);

        Destroy(gameObject, timeToDestroyAmmo[0]);
    }
}
