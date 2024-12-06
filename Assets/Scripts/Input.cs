using UnityEngine;
using UnityEngine.InputSystem;

public class Input : MonoBehaviour
{
    CubeDivider cubeDivider;
    GameManager gameManager;
    InputSystem_Actions action;

    void Awake()
    {
        gameManager = GetComponent<GameManager>();
        cubeDivider = GetComponent<CubeDivider>();
        action = new InputSystem_Actions();
    }

    void OnEnable()
    {
        action.Player.ToStopObject.performed += StopObject;
        action.Enable();
    }

    public void StopObject(InputAction.CallbackContext context)
    {
        if (gameManager.enabled)
        {
            cubeDivider.CutTheMovingObject();
            gameManager.ResetTimer();
        }
    }

    void OnDisable()
    {
        action.Player.ToStopObject.performed -= StopObject;
        action.Disable();
    }
}