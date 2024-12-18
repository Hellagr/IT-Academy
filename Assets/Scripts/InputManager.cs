using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] PrefabCreator prefabCreator;
    InputSystem_Actions action;

    void Awake()
    {
        action = new InputSystem_Actions();
    }

    void OnEnable()
    {
        action.Player.Space.performed += SpaceAction;
        action.Enable();
    }

    void SpaceAction(InputAction.CallbackContext context)
    {
        prefabCreator.CreateRandomPrefab();
    }

    void OnDisable()
    {
        action.Player.Space.performed -= SpaceAction;
        action.Disable();
    }
}
