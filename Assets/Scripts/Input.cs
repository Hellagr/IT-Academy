using UnityEngine;
using UnityEngine.InputSystem;

public class Input : MonoBehaviour
{
    [SerializeField] CreationOtherCubes creationOtherCubes;
    InputSystem_Actions action;

    void Awake()
    {
        action = new InputSystem_Actions();
    }

    void OnEnable()
    {
        action.Player.ToStopObject.performed += StopObject;
        action.Enable();
    }

    void StopObject(InputAction.CallbackContext context)
    {


        //cubeCreateion.RecreateCube();
        creationOtherCubes.CreateARandomCube();

        //Debug.Log($"static {cubeCreateion.previousObject.transform.position}");
        //Debug.Log($"moving{cubeCreateion.movingObject.transform.position}");

    }

    void OnDisable()
    {
        action.Player.ToStopObject.performed -= StopObject;
        action.Disable();
    }
}
