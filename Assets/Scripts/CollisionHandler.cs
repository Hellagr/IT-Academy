using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] GameObject currentObject;
    Vector3 collisionObject;

    void OnCollisionStay(Collision collision)
    {
        Debug.Log($"cur OBJ{currentObject.transform}");
        Debug.Log($"collision OBJ{collision.gameObject.transform}");
    }
}
