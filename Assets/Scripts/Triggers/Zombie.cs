using UnityEngine;

public class Zombie : MonoBehaviour
{
    BoxCollider boxCollider;
    void OnEnable()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.PLAYER))
        {
            if (Random.Range(1, 3) == 1)
            {
                AudioManager.Instance.PlaySound(Sounds.Zombie, transform);
                boxCollider.enabled = false;
            }
        }
    }
}
