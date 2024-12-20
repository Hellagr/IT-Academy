using System;
using System.Reflection.Emit;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class TogglersHandler : MonoBehaviour
{
    protected Toggle toggle;
    protected GameObject toggleMenu;
    protected TextMeshProUGUI textMeshProUGUI;
    protected Text labelText;

    protected void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(ChangeState);
        toggleMenu = GeneralDataManager.Instance.TogglerMenuHierarchy;
        textMeshProUGUI = GeneralDataManager.Instance.TMPro;
        labelText = GetComponentInChildren<Text>();
    }

    protected void ChangeState(bool toggleIsOn)
    {
        Toggle[] toggles = toggleMenu.GetComponentsInChildren<Toggle>();

        foreach (Toggle tog in toggles)
        {
            if (tog != toggle)
            {
                tog.isOn = false;
            }
        }
        toggle.isOn = toggleIsOn;
        ChangeTheText(toggleIsOn);
    }

    protected void ChangeTheText(bool currentToggleIsOn)
    {
        if (currentToggleIsOn)
        {
            textMeshProUGUI.text = $"{labelText.text}";
        }
        else
        {
            textMeshProUGUI.text = "";
        }
    }

    protected void OnDestroy()
    {
        if (toggle != null)
        {
            toggle.onValueChanged.RemoveListener(ChangeState);
        }
    }
}

