using UnityEngine;

public class ExplosionVFX : MonoBehaviour
{
    ParticleSystem[] allVFX;

    void Start()
    {
        allVFX = GetComponentsInChildren<ParticleSystem>();
    }

    public void PlayExplosion()
    {
        transform.parent = null;

        foreach (ParticleSystem vfx in allVFX)
        {
            vfx.Play();
        }

        Destroy(gameObject, 5f);
    }
}
