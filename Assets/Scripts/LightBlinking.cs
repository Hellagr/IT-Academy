using UnityEngine;

public class LightBlinking : MonoBehaviour
{
    Light lightComponent;

    void OnEnable()
    {
        lightComponent = GetComponent<Light>();
    }

    void Update()
    {
        var randomNumber = Random.Range(0, 2);

        if (randomNumber == 1)
        {
            lightComponent.intensity = 0;
        }
        else
        {
            lightComponent.intensity = 1;
        }
    }
}
