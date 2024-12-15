using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float speed = 8f;
    [SerializeField] float JumpHeight = 5f;
    [SerializeField] GameManager gameManager;
    private InputSystem_Actions action;
    private Rigidbody rb;
    private Vector3 movementWithRotation;
    public Vector2 moveInput { get; private set; }

    public bool isJumping { get; private set; } = false;

    public void Jumping()
    {
        isJumping = false;
    }

    public void SetMoveInput()
    {
        moveInput = Vector2.zero;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        action = InputManager.Instance.inputSystemActions;
    }

    void Start()
    {
        gameManager.SetPlayerSpawnPosition(transform.position);
    }

    void OnEnable()
    {
        action.Player.Move.performed += Move;
        action.Player.Move.canceled += CancelMove;
        action.Player.Jump.performed += Jump;
    }


    private void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void CancelMove(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (!isJumping)
        {
            isJumping = true;
            rb.AddForce(Vector3.up * JumpHeight, ForceMode.Impulse);
        }
    }

    void OnDisable()
    {
        action.Player.Move.performed -= Move;
        action.Player.Move.canceled -= CancelMove;
        action.Player.Jump.performed -= Jump;
    }

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y);
        transform.rotation = Quaternion.Euler(0.0f, cam.transform.rotation.eulerAngles.y, 0.0f);
        Quaternion rotation = transform.rotation;
        movementWithRotation = rotation * movement.normalized;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movementWithRotation * speed + new Vector3(0, rb.linearVelocity.y, 0);
    }

    public void EnableControl()
    {
        this.enabled = true;
    }
}