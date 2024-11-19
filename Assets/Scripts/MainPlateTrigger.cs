using System;
using UnityEngine;
using UnityEngine.Animations;

public static class DirectionOfCreatingWall
{
    public const string UP = "Up";
    public const string DOWN = "Down";
}

public class MainPlateTrigger : MonoBehaviour
{
    /* 
     1. Floor 
     * calculate of it's own height
     2. Game manager
    * create a next floor
    * destroy unnecessary floor
     3. Main plate trigger
    * show the direction for creating a floor
     */

    BoxCollider boxCollider;
    Vector3 exitTriggerPoint;
    int parentID;
    float boundMaxX;
    float boundMaxZ;


    void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        boundMaxX = boxCollider.bounds.max.x;
        boundMaxZ = boxCollider.bounds.max.z;
    }

    void OnTriggerEnter(Collider other)
    {
        parentID = transform.parent.parent.gameObject.GetInstanceID();

        if (other.CompareTag(Tags.PLAYER))
        {
            GameManager.Instance.DestroyFloor(parentID);
            //Debug.Log("destroy");
        }

    }

    private void OnTriggerExit(Collider other)
    {
        exitTriggerPoint = boxCollider.ClosestPoint(other.transform.position);
        Vector3 parentGlobalPosition = transform.parent.parent.position;

        if (other.CompareTag(Tags.PLAYER))
        {
            if (Mathf.Approximately(boundMaxX, exitTriggerPoint.x))
            {
                GameManager.Instance.CreateFloor(parentGlobalPosition, DirectionOfCreatingWall.DOWN);
            }
            else
            {
                GameManager.Instance.CreateFloor(parentGlobalPosition, DirectionOfCreatingWall.UP);
            }
        }
    }
}
