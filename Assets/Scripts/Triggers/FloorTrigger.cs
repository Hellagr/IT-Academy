using UnityEngine;

public enum DirectionOfCreatingWall
{
    UP,
    DOWN
}

public class FloorTrigger : MonoBehaviour
{
    BoxCollider boxCollider;
    float boundMaxX;


    void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        boundMaxX = boxCollider.bounds.max.x;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.PLAYER))
        {
            GameManager.Instance.DestroyFloor(transform.parent.parent.gameObject);
        }

    }

    private void OnTriggerExit(Collider other)
    {

        Vector3 exitTriggerPoint = boxCollider.ClosestPoint(other.transform.position);
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
