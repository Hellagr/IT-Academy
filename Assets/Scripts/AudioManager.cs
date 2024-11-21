using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Sounds
{
    WomanScream,
    Footsteps,
    Zombie
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    [SerializeField] GameObject stepPrefab;
    [SerializeField] Dictionary<Sounds, AudioClip> audioClips;

    [SerializeField] AudioClip WomanScream;
    [SerializeField] AudioClip FoorSteps;
    [SerializeField] AudioClip Zombie;

    AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
        audioClips = new Dictionary<Sounds, AudioClip>()
        {
            {Sounds.WomanScream, WomanScream},
            {Sounds.Footsteps, FoorSteps},
            {Sounds.Zombie, Zombie}
        };
    }

    public void CreateAStep(Transform playerTransform, bool isStepLeft)
    {
        Vector3 leftStep = new Vector3(-0.5f, -1f, 0);
        Vector3 rightStep = new Vector3(0.5f, -1f, 0);

        Vector3 stepInLocal = isStepLeft ? leftStep : rightStep;
        Vector3 stepInGlobal = playerTransform.transform.TransformPoint(stepInLocal);

        //var need = playerTransform.TransformPoint(stepInLocal);


        GameObject step = Instantiate(stepPrefab, stepInGlobal, playerTransform.transform.rotation);
        Destroy(step, 1f);
    }

    public void PlaySound(Sounds sound, Transform positionOfTrigger)
    {
        if (audioSource != null)
        {
            float yAxis;
            if (Random.Range(0, 2) == 0)
            {
                yAxis = -12f;
            }
            else
            {
                yAxis = 12f;
            }

            GameObject tempObj = new GameObject();
            AudioSource clip = tempObj.AddComponent<AudioSource>();
            clip.spatialBlend = 1f;
            clip.minDistance = 1f;
            clip.maxDistance = 15f;

            tempObj.transform.SetParent(positionOfTrigger, true);
            Vector3 audioSpot = new Vector3(19, yAxis, 0);
            tempObj.transform.position = positionOfTrigger.TransformPoint(audioSpot);

            clip.PlayOneShot(audioClips[sound]);
            Destroy(tempObj, 2f);
        }
    }
}
