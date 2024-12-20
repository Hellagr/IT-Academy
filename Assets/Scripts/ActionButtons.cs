using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionButtons : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tMPro;
    Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ChangeText);
    }

    private void ChangeText()
    {
        tMPro.text = $"{gameObject.name}";
    }
}
