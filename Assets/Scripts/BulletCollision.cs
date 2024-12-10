using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    [SerializeField] GameObject hitEffectPrefab;
    public Transform parentObject { get; private set; }
    public Vector3 positionForEffect { get; private set; }
    public Vector3 rotationForEffect { get; private set; }

    public void SetParentObject(Transform parentTransform)
    {
        parentObject = parentTransform;
    }
    public void SetPositionForEffect(Vector3 positionEffect)
    {
        positionForEffect = positionEffect;
    }
    public void SetRotationForEffect(Vector3 rotationEffect)
    {
        rotationForEffect = rotationEffect;
    }

    void OnCollisionEnter(Collision collision)
    {
        var decal = Instantiate(hitEffectPrefab, positionForEffect, Quaternion.identity);
        decal.transform.forward = rotationForEffect + new Vector3(0.01f, 0.01f, 0.01f);
        decal.transform.SetParent(parentObject);
        decal.GetComponent<ParticleSystem>().Play();

        Destroy(decal, 3f);

        Destroy(gameObject);
    }
}
