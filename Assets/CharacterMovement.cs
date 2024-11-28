
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal.Internal;

public class CharacterMovement : MonoBehaviour
{
    public Vector2 newVector;

    InputSystem_Actions action;
    Rigidbody2D rb;
    Vector2 moveInput;
    float speed = 2.0f;

    Vector2 forward = Vector2.up;
    Vector2 right = Vector2.right;
    float offsetAngle = -60f;

    public Vector3 start = Vector3.zero;  // ��������� ����� �����
    public float length = 5f;
    public float lineThickness = 0.1f;

    void Awake()
    {
        action = new InputSystem_Actions();
        rb = GetComponent<Rigidbody2D>();

        forward = RotateVector(forward, offsetAngle);
        right = RotateVector(right, offsetAngle / 2);

        Vector2 RotateVector(Vector2 originalVector, float angleInDegrees)
        {
            float angleInRadians = angleInDegrees * Mathf.Deg2Rad;
            float cosAngle = Mathf.Cos(angleInRadians);
            float sinAngle = Mathf.Sin(angleInRadians);

            float x = originalVector.x * cosAngle - originalVector.y * sinAngle;
            float y = originalVector.x * sinAngle + originalVector.y * cosAngle;

            return new Vector2(x, y);
        }
    }

    void OnEnable()
    {
        action.Player.Move.performed += Move;
        action.Player.Move.canceled += CancelMove;
        action.Enable();
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
        action.Disable();
    }

    void Update()
    {
        //Debug.DrawLine(gameObject.transform.position, forward * moveInput.y + right * moveInput.x, Color.blue);
        //Debug.DrawLine(gameObject.transform.position, forward * moveInput.y + right * moveInput.x, Color.red);


        Debug.Log(transform.position);


        rb.linearVelocity = forward * moveInput.y + right * moveInput.x;
    }
}
