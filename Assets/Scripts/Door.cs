using UnityEngine;

public static class Tags
{
    public const string PLAYER = "Player";
}

public class Door : MonoBehaviour
{
    [SerializeField] float moveDistance = 4f;
    Vector3 startPos;
    Vector3 endPos;
    float time = 0f;
    bool isOpen = false;

    void Start()
    {
        startPos = transform.position;
        endPos = transform.position + new Vector3(0, moveDistance, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.PLAYER))
        {
            isOpen = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tags.PLAYER))
        {
            isOpen = false;
        }
    }

    void Update()
    {
        if (isOpen)
        {
            time += Time.deltaTime;
        }
        else
        {
            time -= Time.deltaTime;
        }

        time = Mathf.Clamp01(time);
        transform.position = Vector3.Lerp(startPos, endPos, time);
    }
}