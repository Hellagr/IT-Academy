using UnityEngine;
using UnityEngine.InputSystem;

public class SwapDirection : MonoBehaviour
{
    InputSystem_Actions inputActions;
    public bool isRight = true;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        inputActions.Player.ChangeDirection.performed += FlipDirection;
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.ChangeDirection.performed -= FlipDirection;
        inputActions.Disable();
    }

    private void FlipDirection(InputAction.CallbackContext context)
    {
        transform.GetComponent<SpriteRenderer>().flipX = isRight;
        isRight = !isRight;
    }
}
