using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    public InputSystem_Actions inputSystemActions { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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
