using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    PlayerMovement playerMovement;
    CapsuleCollider colliderPlayer;
    Rigidbody rb;

    void Start()
    {
        colliderPlayer = GetComponent<CapsuleCollider>();
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            playerMovement.Jumping();
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (colliderPlayer.enabled)
            {
                colliderPlayer.enabled = false;
            }

            if (!rb.isKinematic)
            {
                rb.isKinematic = true;
            }

            if (playerMovement.enabled)
            {
                playerMovement.enabled = false;
                playerMovement.SetMoveInput();
                gameManager.ActivateRagdoll();
            }
        }
    }
}
