using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipChanger : MonoBehaviour
{
    [SerializeField] Button leftSide;
    [SerializeField] Button rightSide;
    public int activeShipNumber { get; private set; }
    public event Action<int> shipIsChanged;

    List<GameObject> ships;

    void Start()
    {
        ships = GeneralDataManager.Instance.Ships;
        ships[0].SetActive(true);
        leftSide.onClick.AddListener(SwapToLeft);
        rightSide.onClick.AddListener(SwapToRight);
    }

    private void SwapToLeft()
    {
        ships[activeShipNumber].SetActive(false);
        if (activeShipNumber - 1 > -1)
        {
            activeShipNumber--;
        }
        else
        {
            activeShipNumber = ships.Count - 1;
        }
        ships[activeShipNumber].SetActive(true);
        shipIsChanged?.Invoke(activeShipNumber);
    }

    private void SwapToRight()
    {
        ships[activeShipNumber].SetActive(false);
        if (activeShipNumber + 1 < ships.Count)
        {
            activeShipNumber++;
        }
        else
        {
            activeShipNumber = 0;
        }
        ships[activeShipNumber].SetActive(true);
        shipIsChanged?.Invoke(activeShipNumber);
    }

    void OnDestroy()
    {
        leftSide.onClick.RemoveListener(SwapToLeft);
        rightSide.onClick.RemoveListener(SwapToRight);
    }
}
