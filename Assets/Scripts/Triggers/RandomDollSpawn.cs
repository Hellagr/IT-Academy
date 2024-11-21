using UnityEngine;

public class RandomDollSpawn : MonoBehaviour
{
    Vector3[] positionVariants;

    private void Awake()
    {
        positionVariants = new Vector3[]
        {
            new Vector3 (18.67f,5.66f,-0.059f),
            new Vector3(-5.63f,2.51f,-1.69f),
            new Vector3(-8.67f,-2.99f,-27.55f),
            new Vector3(15.95f,-8.45f,-13.04f)
        };
    }


    void OnEnable()
    {
        var randomElement = Random.Range(0, 4);

        Debug.Log(randomElement);

        transform.position = transform.parent.TransformPoint(positionVariants[randomElement]);
    }
}
