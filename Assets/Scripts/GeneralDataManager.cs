using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GeneralDataManager : MonoBehaviour
{
    public static GeneralDataManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI tMPro;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject togglerMenuHierarchy;
    [SerializeField] private Button backToMainMenuButton;

    public TextMeshProUGUI TMPro
    {
        get
        {
            return tMPro;
        }
    }

    public GameObject MainMenu
    {
        get
        {
            return mainMenu;
        }
    }

    public GameObject TogglerMenuHierarchy
    {
        get
        {
            return togglerMenuHierarchy;
        }
    }

    public Button BackToMainMenuButton
    {
        get
        {
            return backToMainMenuButton;
        }
    }

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
    }
}
