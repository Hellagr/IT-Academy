using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [Header("Player input")]
    [SerializeField] Camera characterCamera;
    InputSystem_Actions inputSystemActions;
    CharacterController controller;

    [Header("Input data")]
    [SerializeField] float walkingSpeed = 1.5f;
    [SerializeField] float sprintSpeed = 4f;
    [SerializeField] float sensetivity = 4f;
    Vector2 moveInput;
    Vector2 lookInput;
    float rotationAngle = 0.0f;
    float rotationSpeed = 0.2f;

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

        inputSystemActions.Player.Enable();
        inputSystemActions.Player.Move.performed += OnMove;
        inputSystemActions.Player.Move.canceled += CancelMove;
        inputSystemActions.Player.Sprint.performed += StartRunning;
        inputSystemActions.Player.Sprint.canceled += EndRunning;
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

    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y);
        Vector3 movementWithCameraRotation = Quaternion.Euler(0.0f, characterCamera.transform.rotation.eulerAngles.y, 0.0f) * movement.normalized;

        float currentSpeed = isRunning ? sprintSpeed : walkingSpeed;

        controller.Move(movementWithCameraRotation * currentSpeed * Time.deltaTime);

        SmoothRotationForPlayerModel(movementWithCameraRotation);
    }

    void SmoothRotationForPlayerModel(Vector3 vectorOfMovement)
    {
        if (vectorOfMovement.sqrMagnitude > 0.0f)
        {
            rotationAngle = Mathf.Atan2(vectorOfMovement.x, vectorOfMovement.z) * Mathf.Rad2Deg;
            playerMoveSpeed = isRunning ? 1f : 0.5f;
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
}
