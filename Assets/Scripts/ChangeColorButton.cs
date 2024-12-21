using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ButtonsColorName
{
    public const string RED = "Red";
    public const string YELLOW = "Yellow";
    public const string BLUE = "Blue";
    public const string GREEN = "Green";
}

public abstract class ChangeColorButton : MonoBehaviour
{
    [SerializeField] Material material;
    List<GameObject> ships;
    GameObject currentShip;
    int currentShipId;
    Button button;

    void Start()
    {
        ships = GeneralDataManager.Instance.Ships;
        button = GetComponent<Button>();
        button.onClick.AddListener(ChangeColorOfCurrentShip);
    }

    private void ChangeColorOfCurrentShip()
    {
        currentShipId = GeneralDataManager.Instance.currentShip;
        currentShip = ships[currentShipId];

        if (gameObject.name == ButtonsColorName.RED ||
            gameObject.name == ButtonsColorName.YELLOW ||
            gameObject.name == ButtonsColorName.BLUE ||
            gameObject.name == ButtonsColorName.GREEN)
        {
            currentShip.GetComponent<MeshRenderer>().material = material;
        }
        else
        {
            Debug.LogError($"This name of button, {gameObject.name}, doesn't exist!");
        }
    }

    void OnDestroy()
    {
        button.onClick.RemoveListener(ChangeColorOfCurrentShip);
    }
}
