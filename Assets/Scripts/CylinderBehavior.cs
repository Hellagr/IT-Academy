using UnityEngine;

public class CylinderBehavior : MonoBehaviour
{
    [SerializeField] float angleRotationPerFrame = 1.0f;
    void Update()
    {
        transform.RotateAround(transform.position, Vector3.forward, angleRotationPerFrame);
    }
}
