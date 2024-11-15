using UnityEngine;
using UnityEngine.InputSystem;

public class LifeHandler : MonoBehaviour
{
    InputSystem_Actions InputSysActions;
    Animator animator;
    CharacterController characterController;
    Vector3 ellenStartPosition;
    bool spawnIsAvaliavle = true;

    void Awake()
    {
        InputSysActions = new InputSystem_Actions();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        ellenStartPosition = transform.position;

        InputSysActions.Player.Respawn.performed += Respawn;
        InputSysActions.Player.Death.performed += Death;
    }

    void OnEnable()
    {
        InputSysActions.Player.Enable();
    }

    void OnDisable()
    {
        InputSysActions.Player.Disable();
    }

    void Respawn(InputAction.CallbackContext context)
    {
        if (!spawnIsAvaliavle)
        {
            spawnIsAvaliavle = true;
        }

        if (characterController.enabled)
        {
            characterController.enabled = false;
            transform.position = ellenStartPosition;
        }

        characterController.enabled = true;

        animator.SetBool("isAlive", true);
    }

    void Death(InputAction.CallbackContext context)
    {
        if (characterController.isGrounded)
        {
            InputHandler.Instance.InputSystemActions.Disable();
            animator.SetBool("isAlive", false);
        }
    }

    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (!stateInfo.IsName("Spawn") && !stateInfo.IsName("Death") && spawnIsAvaliavle)
        {
            InputHandler.Instance.InputSystemActions.Enable();
            animator.SetBool("isAlive", true);
            spawnIsAvaliavle = false;
        }
    }
}
