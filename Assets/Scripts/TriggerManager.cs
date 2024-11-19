using System;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    FloorTrigger floorTrigger;
    void Awake()
    {
        floorTrigger = GetComponent<FloorTrigger>();
    }

    private void OnEnable()
    {
        // MainPlateTrigger.MainPlateCreateFloorAction += CreateFloor;

    }

    private void OnDisable()
    {
        //MainPlateTrigger.MainPlateCreateFloorAction -= CreateFloor;
    }

    public void DestroyExtraFloor(int floorID)
    {

        Debug.Log("working");
    }

    void CreateFloor(string floorDirection)
    {
        if (floorDirection == "minus")
        {

            Debug.Log("minus");

        }
        else
        {
            Debug.Log("plus");
        }
    }
}
