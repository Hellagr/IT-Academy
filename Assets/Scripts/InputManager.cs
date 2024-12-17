using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public InputSystem_Actions inputSystemActions { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        inputSystemActions = new InputSystem_Actions();
    }

    void OnEnable()
    {
        inputSystemActions.Enable();
    }

    void OnDisable()
    {
        inputSystemActions.Disable();
    }
}
