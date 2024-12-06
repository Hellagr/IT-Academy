using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    [SerializeField] float speed = 0.5f;
    CubeGenerator cubeGenerator;

    void Start()
    {
        cubeGenerator = GetComponent<CubeGenerator>();
    }

    void Update()
    {
        if (cubeGenerator.movingObject != null)
        {
            cubeGenerator.movingObject.transform.Translate(cubeGenerator.directionOfObject * speed * Time.deltaTime, Space.Self);
        }
    }
}
