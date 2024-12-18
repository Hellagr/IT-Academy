using System.Collections.Generic;
using UnityEngine;

public class PrefabCreator : MonoBehaviour
{
    [SerializeField] List<GameObject> prefabList = new List<GameObject>();
    int currentNumber = 0;
    GameObject prefab;

    public void CreateRandomPrefab()
    {
        if (prefab != null)
        {
            Destroy(prefab);
        }

        var randomNumberOfPrefab = Random.Range(0, prefabList.Count);

        do
        {
            randomNumberOfPrefab = Random.Range(0, prefabList.Count);
        }
        while (randomNumberOfPrefab == currentNumber);

        prefab = Instantiate(prefabList[randomNumberOfPrefab]);
        currentNumber = randomNumberOfPrefab;
    }
}
