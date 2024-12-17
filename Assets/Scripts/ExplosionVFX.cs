using System.Collections.Generic;
using UnityEngine;

public class ExplosionVFX : MonoBehaviour
{
    [SerializeField] List<AudioImpact> soundsImpactList = new List<AudioImpact>();
    ParticleSystem[] allVFX;

    void Start()
    {
        allVFX = GetComponentsInChildren<ParticleSystem>();
    }

    public void PlayExplosion(Vector3 positionForEffect)
    {
        transform.parent = null;
        transform.position = positionForEffect;

        var audioClip = gameObject.AddComponent<AudioSource>();
        audioClip.spatialBlend = 1.0f;
        audioClip.PlayOneShot(soundsImpactList[0].audioClip);

        foreach (ParticleSystem vfx in allVFX)
        {
            vfx.Play();
        }
    }
}
