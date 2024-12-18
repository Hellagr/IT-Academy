using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] GameObject ammunitionCollision;
    [SerializeField] List<AudioImpact> soundsImpactList = new List<AudioImpact>();
    [SerializeField] float timeToSetInactive;
    [SerializeField] float timeToDestroyVFX;
    protected TrailRenderer trailRenderer;
    Rigidbody rigidBody;
    Transform parentObject;
    Vector3 positionForEffect;
    Vector3 rotationForEffect;

    private void Awake()
    {
        if (rigidBody == null)
        {
            rigidBody = GetComponent<Rigidbody>();
        }

        trailRenderer = GetComponent<TrailRenderer>();
    }

    protected virtual void OnEnable()
    {
        Invoke("SetInActive", timeToSetInactive);
    }

    public virtual void Shoot(Vector3 direction, float force)
    {
        rigidBody.linearVelocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
        rigidBody.AddForce(direction * force, ForceMode.Impulse);
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        positionForEffect = contact.point;
        rotationForEffect = contact.normal;
        parentObject = collision.transform;

        var effectForCollision = Instantiate(ammunitionCollision, positionForEffect, Quaternion.identity);

        effectForCollision.transform.forward = rotationForEffect + new Vector3(0.01f, 0.01f, 0.01f);
        effectForCollision.transform.SetParent(parentObject);
        effectForCollision.GetComponent<ParticleSystem>().Play();

        var audioClip = effectForCollision.AddComponent<AudioSource>();
        audioClip.spatialBlend = 1.0f;
        PlaySoundOfImpact(audioClip);

        Destroy(effectForCollision, timeToDestroyVFX);
    }

    protected virtual void PlaySoundOfImpact(AudioSource audioClip)
    {
        audioClip.PlayOneShot(soundsImpactList[0].audioClip);
    }

    protected virtual void SetInActive()
    {
        gameObject.SetActive(false);
    }
}
