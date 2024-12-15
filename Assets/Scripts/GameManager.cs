using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody[] ragdollRigidbodies;
    [SerializeField] Collider[] ragdollColliders;
    [SerializeField] Rigidbody playerObjectRigidBody;
    [SerializeField] CapsuleCollider playerObjectCollider;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] Transform playerTransform;

    InputSystem_Actions action;
    public Vector3 playerStartPosition { get; private set; }
    bool isColliderRagdollEnabled = false;
    bool isKinematicEnabled = true;

    public void SetPlayerSpawnPosition(Vector3 position)
    {
        playerStartPosition = position;
    }

    void Awake()
    {
        action = InputManager.Instance.inputSystemActions;
        ragdollRigidbodies = playerObjectRigidBody.GetComponentsInChildren<Rigidbody>();
        ragdollColliders = playerObjectCollider.GetComponentsInChildren<Collider>();
    }

    void OnEnable()
    {
        action.Player.Interact.performed += Respawn;
    }

    void OnDisable()
    {
        action.Player.Interact.performed -= Respawn;
    }

    public void ActivateRagdoll()
    {
        if (animator != null)
        {
            animator.enabled = false;
        }
        ToggleRagdoll();
    }

    void ToggleRagdoll()
    {
        isKinematicEnabled = !isKinematicEnabled;
        isColliderRagdollEnabled = !isColliderRagdollEnabled;

        foreach (var rb in ragdollRigidbodies)
        {
            rb.isKinematic = isKinematicEnabled;
        }

        foreach (var collider in ragdollColliders)
        {
            collider.enabled = isColliderRagdollEnabled;
        }
    }

    void Respawn(InputAction.CallbackContext context)
    {
        ToggleRagdoll();

        if (playerObjectRigidBody.isKinematic)
        {
            playerObjectRigidBody.isKinematic = false;
        }

        if (!playerObjectCollider.enabled)
        {
            playerObjectCollider.enabled = true;
        }

        if (animator != null)
        {
            animator.enabled = true;
        }

        playerMovement.EnableControl();

        playerObjectRigidBody.position = playerStartPosition;
    }
}
