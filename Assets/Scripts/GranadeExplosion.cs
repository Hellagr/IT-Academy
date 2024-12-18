//using UnityEngine;

//public class GranadeExplosion : MonoBehaviour
//{
//    [SerializeField] ExplosionVFX explosionVFX;
//    [SerializeField] float radius = 5f;
//    [SerializeField] float explosionForce = 50f;

//    void OnCollisionEnter(Collision collision)
//    {
//        Collider[] objectsAround = Physics.OverlapSphere(transform.position, radius);

//        ContactPoint contact = collision.contacts[0];
//        var positionForEffect = contact.point;

//        explosionVFX.PlayExplosion(positionForEffect);

//        foreach (Collider collider in objectsAround)
//        {
//            Rigidbody rb = collider.GetComponent<Rigidbody>();
//            if (rb != null)
//            {
//                rb.AddExplosionForce(explosionForce, transform.position, radius);
//            }
//        }

//        gameObject.SetActive(false);
//    }
//}