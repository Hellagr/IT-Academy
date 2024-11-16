using UnityEngine;
using UnityEngine.InputSystem;

public class LifeHandler : MonoBehaviour
{
    InputHandler inputHandler;
    InputSystem_Actions InputSysActions;
    CharacterController characterController;
    Animator animator;
    Vector3 ellenStartPosition;

    void Awake()
    {
        InputSysActions = new InputSystem_Actions();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        inputHandler = GetComponent<InputHandler>();

        InputSysActions.Player.Respawn.performed += Respawn;
        InputSysActions.Player.Death.performed += Death;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            transform.position = new Vector3(transform.position.x, hit.point.y + 0.01f, transform.position.z);
            ellenStartPosition = transform.position;
        }
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
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Death"))
        {
            if (stateInfo.normalizedTime > 0.9f)
            {
                if (characterController.enabled)
                {
                    characterController.enabled = false;
                    transform.position = ellenStartPosition;
                }

                characterController.enabled = true;
                animator.SetBool("isAlive", true);
            }
        }
    }

    void Death(InputAction.CallbackContext context)
    {
        if (characterController.isGrounded)
        {
            inputHandler.enabled = false;
            animator.SetBool("isAlive", false);
        }
    }

    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Spawn"))
        {
            if (stateInfo.normalizedTime > 0.9f)
            {
                inputHandler.enabled = true;
                animator.SetBool("isAlive", true);
                animator.SetBool("FreeFall", false);
                animator.ResetTrigger("Landing");
            }
        }
    }
}
