using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Input Field")]
    [SerializeField] InputActionAsset actionAsset;
    CharacterController characterController;
    InputActionMap actionMap;
    InputAction movement;
    InputAction look;
    InputAction jump;

    [Header("Player Rotation")]
    [SerializeField] Transform cameraFocusTransform;
    Vector2 moveinput;
    Vector2 lookinpit;
    Vector3 jumpVelocity;

    [Header("Player Movement Settings")]
    [SerializeField] float speed = 20f;
    [SerializeField] float lookSensitivity = 3f;
    [SerializeField] float repulsiveForce = 9.81f;
    [SerializeField] float jumpTime = 1f;
    float gravity = -9.81f;
    float xRotation = 0f;
    bool isJumping = false;
    private Coroutine jumpCoroutine;

    [Header("Player Touch Field")]
    [SerializeField] GameObject mobileUI;
    [SerializeField] RectTransform joystickRectTransform;
    [SerializeField] RectTransform jumpForMobileRectTransform;

    public CharacterController Controller { get { return characterController = characterController ?? GetComponent<CharacterController>(); } }

    void Awake()
    {
        actionMap = actionAsset.FindActionMap("Player");
        movement = actionMap.FindAction("Move");
        look = actionMap.FindAction("Look");
        jump = actionMap.FindAction("Jump");

        movement.Enable();
        movement.performed += context => moveinput = context.ReadValue<Vector2>();
        movement.canceled += context => moveinput = Vector2.zero;

        look.Enable();
        look.performed += context => lookinpit = context.ReadValue<Vector2>();
        look.canceled += context => lookinpit = Vector2.zero;

        jump.Enable();
        jump.performed += OnJump;
    }

    void OnJump(InputAction.CallbackContext context)
    {
        if (isJumping)
        {
            return;
        }

        if (Controller.isGrounded)
        {
            jumpCoroutine = StartCoroutine(Jump());
        }
    }

    IEnumerator Jump()
    {
        isJumping = true;
        jumpVelocity.y = Mathf.Sqrt(repulsiveForce * -2 * gravity);
        yield return new WaitForSeconds(jumpTime);
        isJumping = false;
        jumpVelocity.y = 0;
    }

    void Update()
    {
        Move();

        if (Pointer.current.device is Touchscreen)
        {
            if(!mobileUI.activeSelf)
            {
                mobileUI.SetActive(true);
            }

            Vector2 touchpoint = Touchscreen.current.primaryTouch.position.ReadValue();

            bool isJoystickToched = RectTransformUtility.RectangleContainsScreenPoint(joystickRectTransform, touchpoint, null);
            bool isJumpButtonToched = RectTransformUtility.RectangleContainsScreenPoint(jumpForMobileRectTransform, touchpoint, null);

            if (!isJoystickToched && !isJumpButtonToched)
            {
                Rotate();
            }
        }
        else
        {
            if (mobileUI.activeSelf)
            {
                mobileUI.SetActive(false);
            }
            Rotate();
        }
    }

    private void Rotate()
    {
        float mouseX = lookinpit.x * lookSensitivity * Time.deltaTime;
        float mouseY = lookinpit.y * lookSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -30f, 74f);
        cameraFocusTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    private void Move()
    {
        var gravityVelocity = Vector3.up * gravity;
        var playerVelocity = transform.TransformDirection(InputToDirection() * speed);
        var velocity = jumpVelocity + playerVelocity + gravityVelocity;
        Controller.Move(velocity * Time.deltaTime);
    }

    private Vector3 InputToDirection()
    {
        return new Vector3(moveinput.x, 0, moveinput.y);
    }
}
