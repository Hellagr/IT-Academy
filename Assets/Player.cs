using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    float gravity = -9.81f;
    float speed = 20f;
    float lookSensitivity = 10f;
    float xRotation = 0f;

    [SerializeField] InputActionAsset actionAsset;
    InputActionMap actionMap;
    InputAction movement;
    InputAction look;

    CharacterController characterController;
    [SerializeField] GameObject camera;
    Camera playerCamera;
    Vector2 MoveInput;
    Vector2 LookInput;

    public CharacterController Controller { get { return characterController = characterController ?? GetComponent<CharacterController>(); } }

    void Awake()
    {
        actionMap = actionAsset.FindActionMap("Player");
        movement = actionMap.FindAction("Move");
        look = actionMap.FindAction("Look");
        playerCamera = camera.GetComponent<Camera>();
    }

    void OnEnable()
    {
        movement.Enable();
        movement.performed += context => MoveInput = context.ReadValue<Vector2>();
        movement.canceled += context => MoveInput = Vector2.zero;

        look.Enable();
        look.performed += context => LookInput = context.ReadValue<Vector2>();
        look.canceled += context => LookInput = Vector2.zero;
    }

    void OnDisable()
    {
        movement.Disable();
        look.Disable();
    }

    void Update()
    {
        Vector3 playerPosition = new Vector3(MoveInput.x * speed, gravity, MoveInput.y * speed);
        Controller.Move(transform.TransformDirection(playerPosition) * Time.deltaTime);

        float mouseX = LookInput.x * lookSensitivity * Time.deltaTime;
        float mouseY = LookInput.y * lookSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
