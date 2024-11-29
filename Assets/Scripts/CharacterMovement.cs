using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] Grid terrainCells;
    public Vector2 newVector;
    public Vector2 moveInput;

    InputSystem_Actions action;
    Rigidbody2D rb;
    Vector2 forward = Vector2.up;
    Vector2 right = Vector2.right;
    float speed = 2.0f;

    void Awake()
    {
        action = new InputSystem_Actions();
        rb = GetComponent<Rigidbody2D>();

        float cellXscale = terrainCells.cellSize.x;
        float cellYscale = terrainCells.cellSize.y;
        float rightAngle = 90;

        float leg1 = cellXscale / 2;
        float leg2 = cellYscale / 2;

        float hypotenuse = Mathf.Sqrt((Mathf.Pow(leg1, 2)) + (Mathf.Pow(leg2, 2)));
        float mediane = hypotenuse / 2;

        float leg1SmallRightAngle = leg2 / 2;

        float offsetAngle = Mathf.Acos(leg1SmallRightAngle / mediane);

        float angleInDegree = Mathf.Rad2Deg * offsetAngle;

        forward = RotateVector(forward, -angleInDegree);
        right = RotateVector(right, -(rightAngle - angleInDegree));

        Vector2 RotateVector(Vector2 originalVector, float angleInDegrees)
        {
            float angleInRad = Mathf.Deg2Rad * angleInDegrees;

            float cosAngle = Mathf.Cos(angleInRad);
            float sinAngle = Mathf.Sin(angleInRad);

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
        rb.linearVelocity = forward * moveInput.y + right * moveInput.x;
    }

}
