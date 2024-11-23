using UnityEngine;
using UnityEngine.Audio;

public class Lockpicking : MonoBehaviour
{
    [SerializeField] AudioMixerSnapshot farToDoor;
    [SerializeField] AudioMixerSnapshot closeToDoor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.PLAYER))
        {
            closeToDoor.TransitionTo(0.5f);

            var child = transform.GetChild(0);
            var audio = child.GetComponent<AudioSource>();
            audio.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tags.PLAYER))
        {
            farToDoor.TransitionTo(0.5f);
        }
    }
}