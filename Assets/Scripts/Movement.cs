using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] float highOfJump = 40f;
    [SerializeField] float speed = 50.0f;
    public Rigidbody2D rb;
    public Vector2 moveInput;
    public bool isHitted = false;
    public bool isJumping = false;
    InputSystem_Actions action;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        action = new InputSystem_Actions();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        action.Player.Move.performed += Move;
        action.Player.Move.canceled += Move_canceled;
        action.Player.Jump.performed += Jump;
        action.Enable();
    }

    void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        spriteRenderer.flipX = moveInput.x > 0 ? false : true;
    }

    void Move_canceled(InputAction.CallbackContext obj)
    {
        moveInput = Vector2.zero;
    }

    void Jump(InputAction.CallbackContext context)
    {
        if (!isJumping)
        {
            isJumping = true;
            rb.AddForce(new Vector2(rb.linearVelocity.x / speed, highOfJump), ForceMode2D.Impulse);
        }
    }

    public void OnDisable()
    {
        action.Player.Move.performed -= Move;
        action.Player.Move.canceled -= Move_canceled;
        action.Player.Jump.performed -= Jump;
        action.Disable();
    }

    void Update()
    {
        if (!isHitted)
        {
            rb.linearVelocity = new Vector2(moveInput.x * speed, rb.linearVelocity.y);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.GROUND))
        {
            isJumping = false;
        }
    }

    public void EnableMovement()
    {
        isHitted = false;
    }
}
