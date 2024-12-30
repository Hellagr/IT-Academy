using UnityEngine;
using UnityEngine.UI;

public class PressingAcoloredButton : MonoBehaviour, IChangeColor
{
    [SerializeField] Material material;
    Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ChangeColorOfShip);
    }

    public void ChangeColorOfShip() 
    {
        var currentShipId = GeneralDataManager.Instance.currentShip;
        var ships = GeneralDataManager.Instance.Ships;
        var currentShip = ships[currentShipId];
        currentShip.GetComponent<MeshRenderer>().material = material;
    }
}