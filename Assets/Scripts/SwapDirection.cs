using UnityEngine;
using UnityEngine.InputSystem;

public class SwapDirection : MonoBehaviour
{
    InputSystem_Actions inputActions;
    SpriteRenderer spriteRenderer;


    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        spriteRenderer.flipX = !spriteRenderer.flipX;
        //MoveSprite.Instance.isRight = !MoveSprite.Instance.isRight;
    }
}
