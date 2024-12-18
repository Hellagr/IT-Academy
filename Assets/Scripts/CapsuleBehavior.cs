using UnityEngine;

public class CapsuleBehavior : MonoBehaviour
{
    [SerializeField] float valueToMove = 3;
    Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float xPos = Mathf.PingPong(Time.time, valueToMove);
        transform.position = Vector3.right * (xPos - valueToMove / 2);
    }
}
