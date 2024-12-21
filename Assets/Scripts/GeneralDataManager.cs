using System.Collections.Generic;
using UnityEngine;

public class GeneralDataManager : MonoBehaviour
{
    public static GeneralDataManager Instance;
    [SerializeField] private Transform parentTransformWithAllShips;
    [SerializeField] private Camera miniCamera;
    [SerializeField] private List<GameObject> ships = new List<GameObject>();
    [SerializeField] ShipChanger shipChanger;
    public int currentShip { get; private set; }

    public Transform ParentTransformWithAllShips
    {
        get
        {
            return parentTransformWithAllShips;
        }
    }

    public Camera MiniCamera
    {
        get
        {
            return miniCamera;
        }
    }

    public List<GameObject> Ships
    {
        get
        {
            return ships;
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
            Destroy(Instance);
        }
    }

    void OnEnable()
    {
        shipChanger.shipIsChanged += GetNumberOfShip;
    }

    private void GetNumberOfShip(int numberOfShip)
    {
        currentShip = numberOfShip;
    }

    void OnDisable()
    {
        shipChanger.shipIsChanged -= GetNumberOfShip;
    }
}
