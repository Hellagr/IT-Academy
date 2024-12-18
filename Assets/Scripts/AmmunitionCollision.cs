//using System.Collections.Generic;
//using UnityEngine;

//public class AmmunitionCollision : Projectile
//{
//[SerializeField] GameObject ammunitionCollision;
//[SerializeField] List<AudioImpact> soundsImpactList = new List<AudioImpact>();
//[SerializeField] float timeToSetInactive = 3f;
//[SerializeField] float timeToDestroyVFX = 3f;
//Transform parentObject;
//Vector3 positionForEffect;
//Vector3 rotationForEffect;

//void OnEnable()
//{
//    Invoke("SetInActive", timeToSetInactive);
//}

//protected override void OnCollisionEnter(Collision collision)
//{
//    base.OnCollisionEnter(collision);

//    ContactPoint contact = collision.contacts[0];
//    positionForEffect = contact.point;
//    rotationForEffect = contact.normal;
//    parentObject = collision.transform;

//    var effectForCollision = Instantiate(ammunitionCollision, positionForEffect, Quaternion.identity);

//    effectForCollision.transform.forward = rotationForEffect + new Vector3(0.01f, 0.01f, 0.01f);
//    effectForCollision.transform.SetParent(parentObject);
//    effectForCollision.GetComponent<ParticleSystem>().Play();

//    var audioClip = effectForCollision.AddComponent<AudioSource>();
//    audioClip.spatialBlend = 1.0f;
//    PlaySoundOfImpact(audioClip);

//    Destroy(effectForCollision, timeToDestroyVFX);
//}

//private void PlaySoundOfImpact(AudioSource audioClip)
//{
//    audioClip.PlayOneShot(soundsImpactList[0].audioClip);
//}

//void SetInActive()
//{
//    gameObject.SetActive(false);
//}
//}