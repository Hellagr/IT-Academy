using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    CharacterController characterController;
    public Camera characterCamera;
    public InputSystem_Actions InputSystemActions;
    public float speed = 5.0f;
    float gravity = -9.81f;
    float speedYaxis = -9.81f;
    float jumpSpeed = 6f;
    bool isJumping = false;
    bool isRunnig = false;

    Vector2 moveInput;

    void Awake()
    {
        InputSystemActions = new InputSystem_Actions();
        characterController = GetComponent<CharacterController>();

        InputSystemActions.Player.Move.performed += context => moveInput = context.ReadValue<Vector2>();
        InputSystemActions.Player.Move.canceled += context => moveInput = Vector2.zero;
        InputSystemActions.Player.Sprint.performed += context => isRunnig = true;
        InputSystemActions.Player.Sprint.canceled += context => isRunnig = false;
        InputSystemActions.Player.Jump.performed += Jump;
    }

    void Jump(InputAction.CallbackContext context)
    {
        if (!isJumping)
        {
            isJumping = true;
            speedYaxis = jumpSpeed;
        }
    }

    void OnEnable()
    {
        InputSystemActions.Enable();
    }

    void OnDisable()
    {
        InputSystemActions.Disable();
    }

    void Update()
    {
        MovementAndRotation();

        if (isJumping)
        {
            JumpAction();
        }
    }

    private void MovementAndRotation()
    {
        Vector3 vector3 = new Vector3(moveInput.x, 0, moveInput.y);
        Vector3 totated = Quaternion.Euler(0.0f, characterCamera.transform.rotation.eulerAngles.y, 0.0f) * vector3.normalized;
        Vector3 verticalVelocity = Vector3.up * speedYaxis;
        float currentSpeed = isRunnig ? speed * 2 : speed;
        characterController.Move((verticalVelocity + totated * currentSpeed) * Time.deltaTime);
    }

    private void JumpAction()
    {
        if (!characterController.isGrounded)
        {
            speedYaxis += gravity * Time.deltaTime;
        }
        else
        {
            isJumping = false;
            speedYaxis = gravity;
        }
    }
}
