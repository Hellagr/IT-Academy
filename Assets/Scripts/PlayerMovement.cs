using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float speed = 8f;
    InputSystem_Actions action;
    Rigidbody rb;
    Vector3 movementIncludeRotation;
    Vector2 moveInput;
    Vector2 lookInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        action = InputManager.Instance.inputSystemActions;
        action.Player.Move.performed += Move;
        action.Player.Move.canceled += CancelMove;
    }

    void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    void CancelMove(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
    }

    void OnDisable()
    {
        action.Player.Move.performed -= Move;
        action.Player.Move.canceled -= CancelMove;
    }

    void Update()
    {
        Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y);
        Quaternion rotation = Quaternion.Euler(0.0f, cam.transform.rotation.eulerAngles.y, 0.0f);
        movementIncludeRotation = rotation * movement.normalized;
        transform.rotation = Quaternion.Euler(0.0f, cam.transform.rotation.eulerAngles.y, 0.0f);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movementIncludeRotation * speed + new Vector3(0, rb.linearVelocity.y, 0);
    }
}
