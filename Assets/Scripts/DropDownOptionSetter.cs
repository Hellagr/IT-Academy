using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropDownOptionSetter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    TMP_Dropdown dropdown;

    void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        dropdown.onValueChanged.AddListener(GetChangeDropDownValue);
    }

    private void GetChangeDropDownValue(int numberOfChoosenVariant)
    {
        textMeshProUGUI.text = dropdown.options[numberOfChoosenVariant].text;
    }

    void OnDestroy()
    {
        dropdown.onValueChanged.RemoveListener(GetChangeDropDownValue);
    }
}
