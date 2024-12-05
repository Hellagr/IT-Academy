using UnityEngine;
using UnityEngine.InputSystem;

public class Input : MonoBehaviour
{
    GameManager gameManager;
    InputSystem_Actions action;

    void Awake()
    {
        gameManager = GetComponent<GameManager>();
        action = new InputSystem_Actions();
    }

    void OnEnable()
    {
        action.Player.ToStopObject.performed += StopObject;
        action.Enable();
    }

    void StopObject(InputAction.CallbackContext context)
    {
        if (gameManager.enabled)
        {
            gameManager.CutTheMovingObject();
        }
    }

    void OnDisable()
    {
        action.Player.ToStopObject.performed -= StopObject;
        action.Disable();
    }
}