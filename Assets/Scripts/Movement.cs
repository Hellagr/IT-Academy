using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [Header("Player input")]
    [SerializeField] Camera characterCamera;
    InputSystem_Actions inputSystemActions;
    CharacterController controller;

    [Header("Input data")]
    [SerializeField] float walkingSpeed = 2f;
    [SerializeField] float sprintSpeed = 6f;
    Vector2 moveInput;
    Vector2 lookInput;
    float currentSpeed = 1.5f;
    float rotationAngle = 0.0f;
    float rotationSpeed = 0.2f;
    float gravity = -9.81f;
    float speedY = -9.81f;
    float jumpSpeed = 6f;
    bool isJumping = false;

    [Header("Animator")]
    Animator animator;
    float animationBlendSpeed = 0.2f;
    float playerMoveSpeed = 0;
    bool isRunning = false;

    void Awake()
    {
        inputSystemActions = new InputSystem_Actions();
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        if (characterCamera == null) Debug.LogError("characterCamera is not assigned!");
        if (controller == null) Debug.LogError("CharacterController component is missing!");
        if (animator == null) Debug.LogError("Animator component is missing!");

        inputSystemActions.Player.Enable();
        inputSystemActions.Player.Move.performed += OnMove;
        inputSystemActions.Player.Move.canceled += CancelMove;
        inputSystemActions.Player.Sprint.performed += StartRunning;
        inputSystemActions.Player.Sprint.canceled += EndRunning;
        inputSystemActions.Player.Jump.performed += Jump;
    }

    void OnMove(InputAction.CallbackContext contex)
    {
        moveInput = contex.ReadValue<Vector2>();
    }

    void CancelMove(InputAction.CallbackContext contex)
    {
        moveInput = Vector2.zero;
    }

    void StartRunning(InputAction.CallbackContext context)
    {
        isRunning = true;
    }

    void EndRunning(InputAction.CallbackContext context)
    {
        isRunning = false;
    }

    void Jump(InputAction.CallbackContext context)
    {

        if (!isJumping)
        {
            isJumping = true;
            animator.SetTrigger("Jump");
            speedY = jumpSpeed;
        }
    }

    void Update()
    {
        Move();

        if (isJumping)
        {
            JumpAction();
        }

        if (!isJumping && !controller.isGrounded && speedY == gravity)
        {
            FreeFall();
        }
    }

    void FreeFall()
    {
        animator.SetBool("FreeFall", true);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.2f, LayerMask.GetMask("Default")))
        {
            animator.SetTrigger("Landing");
        }
    }

    void Move()
    {
        Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y);
        Vector3 movementWithCameraRotation = Quaternion.Euler(0.0f, characterCamera.transform.rotation.eulerAngles.y, 0.0f) * movement.normalized;

        if (!isJumping && controller.isGrounded)
        {
            currentSpeed = isRunning ? sprintSpeed : walkingSpeed;
            animator.SetBool("FreeFall", false);
        }

        Vector3 verticalVelocity = Vector3.up * speedY;

        controller.Move((verticalVelocity + movementWithCameraRotation * currentSpeed) * Time.deltaTime);

        SmoothRotationForPlayerModel(movementWithCameraRotation);
    }

    void SmoothRotationForPlayerModel(Vector3 vectorOfMovement)
    {
        if (vectorOfMovement.sqrMagnitude > 0.0f)
        {
            rotationAngle = Mathf.Atan2(vectorOfMovement.x, vectorOfMovement.z) * Mathf.Rad2Deg;
            playerMoveSpeed = currentSpeed == sprintSpeed ? 1f : 0.5f;
        }
        else
        {
            playerMoveSpeed = 0.0f;
        }

        animator.SetFloat("Speed", Mathf.Lerp(animator.GetFloat("Speed"), playerMoveSpeed, animationBlendSpeed));

        Quaternion currentRotation = controller.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0.0f, rotationAngle, 0.0f);

        controller.transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, rotationSpeed);
    }

    void JumpAction()
    {
        if (!controller.isGrounded)
        {
            speedY += gravity * Time.deltaTime;
        }

        animator.SetFloat("SpeedY", speedY / jumpSpeed);

        if (speedY < 0.1)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.3f, LayerMask.GetMask("Default")))
            {
                isJumping = false;
                animator.SetTrigger("Landing");
                speedY = gravity;
            }
        }
    }
}
