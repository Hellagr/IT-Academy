using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public static InputHandler Instance { get; private set; }

    [Header("Player input")]
    [SerializeField] Camera characterCamera;
    public InputSystem_Actions InputSystemActions { get; private set; }
    CharacterController controller;
    LifeHandler lifeHandler;

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
    public Animator animator;
    float animationBlendSpeed = 0.1f;
    float playerMoveSpeed = 0;
    bool isRunning = false;
    bool freeFall = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        InputSystemActions = new InputSystem_Actions();
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        lifeHandler = GetComponent<LifeHandler>();

        InputSystemActions.Player.Move.performed += OnMove;
        InputSystemActions.Player.Move.canceled += CancelMove;
        InputSystemActions.Player.Sprint.performed += StartRunning;
        InputSystemActions.Player.Sprint.canceled += EndRunning;
        InputSystemActions.Player.Jump.performed += Jump;
        InputSystemActions.Player.Attack.performed += Attack;
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
        if (!isJumping && !freeFall)
        {
            isJumping = true;
            animator.SetTrigger("Jump");
            speedY = jumpSpeed;
        }
    }

    void Attack(InputAction.CallbackContext context)
    {
        animator.SetInteger("AttackType", UnityEngine.Random.Range(0, 3));
        animator.SetTrigger("Attack");
    }

    void Update()
    {
        Move();

        if (isJumping)
        {
            JumpAction();
        }
    }

    void Move()
    {
        Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y);
        Vector3 movementWithCameraRotation = Quaternion.Euler(0.0f, characterCamera.transform.rotation.eulerAngles.y, 0.0f) * movement.normalized;

        if (!isJumping && !controller.isGrounded && moveInput != Vector2.zero)
        {
            freeFall = true;
            animator.SetBool("FreeFall", true);
        }

        if (!isJumping && controller.isGrounded)
        {
            currentSpeed = isRunning ? sprintSpeed : walkingSpeed;

            if (freeFall)
            {
                freeFall = false;
                animator.SetBool("FreeFall", false);
                animator.SetTrigger("Landing");
            }
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
            animator.SetFloat("SpeedY", speedY / jumpSpeed);
        }
        else
        {
            speedY = gravity;
            isJumping = false;
        }

        if (controller.isGrounded)
        {
            animator.SetTrigger("Landing");
        }
    }
}