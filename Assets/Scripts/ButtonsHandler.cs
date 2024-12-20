using UnityEngine;
using UnityEngine.UI;

public abstract class ButtonsHandler : MonoBehaviour
{
    [SerializeField] GameObject menuToOpen;
    GameObject mainMenu;
    Button backToMainMenuButton;
    Button button;

    protected void Start()
    {
        mainMenu = GeneralDataManager.Instance.MainMenu;
        backToMainMenuButton = GeneralDataManager.Instance.BackToMainMenuButton;

        if (mainMenu == null)
        {
            Debug.LogError("mainMenu is not found!");
        }

        if (backToMainMenuButton == null)
        {
            Debug.LogError("backToMainMenuButtom is not found!");
        }

        button = GetComponent<Button>();
        button.onClick.AddListener(OpenMenu);
        backToMainMenuButton.onClick.AddListener(GoBackToMainMenu);
    }

    protected void OpenMenu()
    {
        mainMenu.SetActive(false);
        menuToOpen.SetActive(true);
        backToMainMenuButton.gameObject.SetActive(true);
    }

    protected void GoBackToMainMenu()
    {
        menuToOpen.SetActive(false);
        mainMenu.SetActive(true);
        backToMainMenuButton.gameObject.SetActive(false);
    }
}
