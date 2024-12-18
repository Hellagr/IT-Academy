using UnityEngine;

public class SphereBehavior : MonoBehaviour
{
    [SerializeField] float scaleMultiplyer = 3f;
    [SerializeField] float speed = 0.1f;

    void Update()
    {
        float value = Mathf.MoveTowards(transform.localScale.x, scaleMultiplyer, speed * Time.deltaTime);
        transform.localScale = new Vector3(value, value, value);
    }
}
