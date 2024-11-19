using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public static class Tags
{
    public const string PLAYER = "Player";
}

class Node
{
    public int id;
    public GameObject objectReference;
    public Node next;
    public Node(int id, GameObject referenceToObject, Node next)
    {
        this.id = id;
        this.objectReference = referenceToObject;
        this.next = next;
    }
}

class LinkedList
{
    public Node Head;
    public LinkedList()
    {
        Head = null;
    }
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] GameObject floorPrefab;
    [SerializeField] Transform spawner;
    LinkedList linkedList;
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

        if (spawner.childCount == 0)
        {
            linkedList = new LinkedList();
            GameObject firstObject = Instantiate(floorPrefab, transform.position, Quaternion.identity, spawner);

            linkedList.Head = new Node(spawner.GetChild(0).GetInstanceID(), firstObject, null);

            Debug.Log(spawner.GetChild(0).GetInstanceID());

        }
    }

    private void Start()
    {
        floorPropeties = spawner.GetChild(0).GetComponent<FloorPropeties>();
        //Debug.Log(floorPrefab.GetComponent<FloorPropeties>().Height);
        heightOfFloor = floorPropeties.Height;

    }

    public void CreateFloor(Vector3 parentGlobalPosition, string directionOfCreating)
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
        linkedList.Head.next = new Node(newFloor.GetInstanceID(), newFloor, null);
        Debug.Log(newFloor.GetInstanceID());
    }

    public void DestroyFloor(int idFloorPlayerStanding)
    {
        if (linkedList.Head.next != null)
        {
            if (linkedList.Head.id == idFloorPlayerStanding)
            {

                Debug.Log("equal");

                Destroy(linkedList.Head.next.objectReference);
                linkedList.Head.next = null;
            }
            else
            {
                Debug.Log("not equal");
                Destroy(linkedList.Head.objectReference);
                linkedList.Head = linkedList.Head.next;
            }
        }
    }
}
