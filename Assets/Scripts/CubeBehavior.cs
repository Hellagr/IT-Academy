using System.Collections;
using UnityEngine;

public class CubeBehavior : MonoBehaviour
{
    [SerializeField] float timeToChangePos = 2f;
    Coroutine couroutine;
    Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void OnEnable()
    {
        StartCoroutine(ChangePosition());
    }

    IEnumerator ChangePosition()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeToChangePos);
            transform.position = startPos + new Vector3(Random.Range(0, 3), Random.Range(0, 3), Random.Range(0, 3));
        }
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }
}
