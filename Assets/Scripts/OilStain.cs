using UnityEngine;
using UnityEngine.AI;

public class OilStain : MonoBehaviour
{
    [SerializeField] NavMeshAgent playerNavMeshAgent;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.PLAYER))
        {
            playerNavMeshAgent.speed /= 2;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tags.PLAYER))
        {
            playerNavMeshAgent.speed *= 2;
        }
    }
}
