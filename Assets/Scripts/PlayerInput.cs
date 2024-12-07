using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] Camera cam;
    InputSystem_Actions action;
    NavMeshAgent agent;

    void Awake()
    {
        action = new InputSystem_Actions();
        agent = GetComponent<NavMeshAgent>();
    }

    void OnEnable()
    {
        action.Player.Move.performed += ClickMove;
        action.Enable();
    }

    void ClickMove(InputAction.CallbackContext context)
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        Ray ray = cam.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            agent.SetDestination(hit.point);
        }
    }

    void OnDisable()
    {
        action.Player.Move.performed -= ClickMove;
        action.Disable();
    }
}
