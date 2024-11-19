using UnityEngine;

public class FloorPropeties : MonoBehaviour
{
    public GameObject lowestObj;
    public GameObject highestObj;
    MeshCollider colliderLowestObj;
    MeshCollider colliderHighestObj;
    float lowestYpoint;
    float highestYpoint;

    public float Height { get; private set; }

    void Awake()
    {
        colliderLowestObj = lowestObj.GetComponent<MeshCollider>();
        colliderHighestObj = highestObj.GetComponent<MeshCollider>();
        lowestYpoint = colliderLowestObj.bounds.min.y;
        highestYpoint = colliderHighestObj.bounds.max.y;

        Height = Mathf.Abs(highestYpoint) + Mathf.Abs(lowestYpoint);
    }
}
