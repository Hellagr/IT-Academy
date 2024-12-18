using UnityEngine;

public class GrenadeCollision : Projectile
{
    [SerializeField] float radius = 5f;
    [SerializeField] float explosionForce = 50f;

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        Collider[] objectsAround = Physics.OverlapSphere(transform.position, radius);
        ContactPoint contact = collision.contacts[0];
        var positionForEffect = contact.point;

        foreach (Collider collider in objectsAround)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, radius);
            }
        }

        gameObject.SetActive(false);
    }
}
