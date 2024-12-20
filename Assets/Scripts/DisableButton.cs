using UnityEngine;
using UnityEngine.UI;

public class DisableButton : MonoBehaviour
{
    [SerializeField] GameObject buttonGroup;
    Button[] actionButtons;
    Button button;
    bool isAllButtonsDisabled = false;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ToggleAllButtons);

        if (buttonGroup != null)
        {
            actionButtons = buttonGroup.GetComponentsInChildren<Button>();
        }
        else
        {
            Debug.LogError("buttonGroup is not assigned in the Inspector.");
        }
    }

    private void ToggleAllButtons()
    {
        foreach (var btn in actionButtons)
        {
            btn.interactable = !btn.interactable;
        };
        isAllButtonsDisabled = true;
    }

    void OnDisable()
    {
        if (isAllButtonsDisabled)
        {
            ToggleAllButtons();
        }
    }

    protected void OnDestroy()
    {
        if (button != null)
        {
            button.onClick.RemoveListener(ToggleAllButtons);
        }
    }
}
