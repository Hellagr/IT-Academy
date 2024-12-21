using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] float lookSensitivity = 6.0f;
    InputSystem_Actions action;
    Vector2 lookInput;

    void Awake()
    {
        action = new InputSystem_Actions();
    }

    void OnEnable()
    {
        action.Player.Look.performed += Look;
        action.Player.Look.canceled += CanceledLook;
        action.Enable();
    }

    private void Look(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    private void CanceledLook(InputAction.CallbackContext context)
    {
        lookInput = Vector2.zero;
    }

    void OnDestroy()
    {
        action.Player.Look.performed -= Look;
        action.Player.Look.canceled -= CanceledLook;
        action.Disable();
    }

    void Update()
    {
        float mouseX = lookInput.x * lookSensitivity * Time.deltaTime;
        transform.Rotate(new Vector3(0, mouseX, 0));
    }
}
