using System.Collections.Generic;
using UnityEngine;

public static class Tags
{
    public const string PLAYER = "Player";
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] GameObject floorPrefab;
    [SerializeField] Transform spawner;
    LinkedList<GameObject> linkedList;
    FloorPropeties floorPropeties;
    float heightOfFloor;

    private void Awake()
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

    private void Start()
    {
        floorPropeties = spawner.GetChild(0).GetComponent<FloorPropeties>();
        heightOfFloor = floorPropeties.Height;
    }

    private void OnEnable()
    {
        if (spawner.childCount == 0)
        {
            GameObject firstObject = Instantiate(floorPrefab, transform.position, Quaternion.identity, spawner);
            linkedList = new LinkedList<GameObject>();
            linkedList.AddFirst(firstObject);
        }
    }

    public void CreateFloor(Vector3 parentGlobalPosition, DirectionOfCreatingWall directionOfCreating)
    {
        GameObject newFloor;
        if (directionOfCreating == DirectionOfCreatingWall.DOWN)
        {
            newFloor = Instantiate(floorPrefab, parentGlobalPosition + new Vector3(0, -heightOfFloor, 0), Quaternion.identity, spawner);
        }
        else
        {
            newFloor = Instantiate(floorPrefab, parentGlobalPosition + new Vector3(0, heightOfFloor, 0), Quaternion.identity, spawner);
        }
        linkedList.AddLast(newFloor);
    }

    public void DestroyFloor(GameObject floorForDelete)
    {
        if (linkedList.First.Next != null)
        {
            if (linkedList.First.Value == floorForDelete)
            {
                Destroy(linkedList.First.Next.Value);
                linkedList.RemoveLast();
            }
            else
            {
                Destroy(linkedList.First.Value);
                linkedList.RemoveFirst();
            }
        }
    }
}
