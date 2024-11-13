using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public static Movement Instance { get; private set; }

    [Header("Player input")]
    [SerializeField] Camera characterCamera;
    public InputSystem_Actions inputSystemActions { get; private set; }
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
    public Animator animator { get; private set; }
    float animationBlendSpeed = 0.2f;
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

        inputSystemActions = new InputSystem_Actions();
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        inputSystemActions.Player.Move.performed += OnMove;
        inputSystemActions.Player.Move.canceled += CancelMove;
        inputSystemActions.Player.Sprint.performed += StartRunning;
        inputSystemActions.Player.Sprint.canceled += EndRunning;
        inputSystemActions.Player.Jump.performed += Jump;
    }

    //void OnEnable()
    //{
    //    inputSystemActions.Player.Enable();
    //}

    void OnDisable()
    {
        inputSystemActions.Player.Disable();
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

    void Update()
    {
        Move();

        if (isJumping)
        {
            JumpAction();
        }

        if (!isJumping && !controller.isGrounded)
        {
            freeFall = true;
            animator.SetBool("FreeFall", true);
        }
    }

    void Move()
    {
        Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y);
        Vector3 movementWithCameraRotation = Quaternion.Euler(0.0f, characterCamera.transform.rotation.eulerAngles.y, 0.0f) * movement.normalized;

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

        if (!isJumping && controller.isGrounded)
        {
            animator.SetTrigger("Landing");
        }
    }
}
