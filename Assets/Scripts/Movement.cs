using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Movement : MonoBehaviour
{
    CharacterController characterController;
    [SerializeField] Camera characterCamera;
    [SerializeField] InputSystem_Actions InputSystemActions;
    [SerializeField] float speed = 5.0f;
    [SerializeField] float runnigSpeed = 10.0f;
    float gravity = -9.81f;
    float speedYaxis = -9.81f;
    float jumpSpeed = 6f;
    bool isJumping = false;
    bool isRunnig = false;

    Vector2 moveInput;
    float pressingTime;
    float timeForAudioOfWalking = 0.8f;
    float timeForAudioOfRunning = 0.4f;
    float timeHasPassed = 0f;
    bool isMoving = false;
    bool isLeftStep = true;

    void Awake()
    {
        InputSystemActions = new InputSystem_Actions();
        characterController = GetComponent<CharacterController>();

        InputSystemActions.Player.Move.performed += OnMovement;
        InputSystemActions.Player.Move.canceled += MovementCalceled;
        InputSystemActions.Player.Sprint.performed += context => isRunnig = true;
        InputSystemActions.Player.Sprint.canceled += context => isRunnig = false;
        InputSystemActions.Player.Jump.performed += Jump;
    }

    void OnMovement(InputAction.CallbackContext context)
    {
        isMoving = true;
        moveInput = context.ReadValue<Vector2>();
    }

    void MovementCalceled(InputAction.CallbackContext context)
    {
        isMoving = false;
        timeHasPassed = 0f;
        moveInput = Vector2.zero;
    }

    void Jump(InputAction.CallbackContext context)
    {
        if (!isJumping)
        {
            isJumping = true;
            speedYaxis = jumpSpeed;
        }
    }

    void OnEnable()
    {
        InputSystemActions.Enable();
    }

    void OnDisable()
    {
        InputSystemActions.Disable();
    }

    void Update()
    {
        MovementAndRotation();

        if (isJumping)
        {
            JumpAction();
        }
    }

    private void MovementAndRotation()
    {
        Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y);
        Vector3 rotation = Quaternion.Euler(0.0f, characterCamera.transform.rotation.eulerAngles.y, 0.0f) * movement.normalized;
        transform.rotation = Quaternion.Euler(0.0f, characterCamera.transform.rotation.eulerAngles.y, 0.0f);
        Vector3 verticalVelocity = Vector3.up * speedYaxis;
        float currentSpeed = isRunnig ? runnigSpeed : speed;

        AudioStep(currentSpeed, transform);

        characterController.Move((verticalVelocity + rotation * currentSpeed) * Time.deltaTime);
    }

    private void AudioStep(float currentSpeed, Transform playerTransform)
    {
        if (isMoving)
        {
            timeHasPassed += Time.deltaTime;

            float speedOfAudioMovement = currentSpeed == runnigSpeed ? timeForAudioOfRunning : timeForAudioOfWalking;

            pressingTime = Mathf.Clamp01(timeHasPassed / speedOfAudioMovement);

            if (timeHasPassed > speedOfAudioMovement)
            {
                AudioManager.Instance.CreateAStep(playerTransform, isLeftStep);
                if (isLeftStep)
                {
                    isLeftStep = false;
                }
                else
                {
                    isLeftStep = true;
                }

                timeHasPassed = 0f;
            }
        }
    }

    private void JumpAction()
    {
        if (!characterController.isGrounded)
        {
            speedYaxis += gravity * Time.deltaTime;
        }
        else
        {
            isJumping = false;
            speedYaxis = gravity;
        }
    }
}
