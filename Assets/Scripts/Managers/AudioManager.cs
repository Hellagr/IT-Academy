using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct AudioAction
{
    public Ammo ammoType;
    public AudioClip audioClip;
}

[Serializable]
public struct AudioImpact
{
    public Ammo ammoType;
    public AudioClip audioClip;
}

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] List<AudioAction> soundsActionList = new List<AudioAction>();
    public AudioSource audioSource { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            gameObject.AddComponent<AudioSource>();
            audioSource = GetComponent<AudioSource>();
        }
    }

    public void PlayActionAudio(Ammo ammoType)
    {
        var sound = soundsActionList.Find(a => a.ammoType == ammoType);
        audioSource.PlayOneShot(sound.audioClip);
    }
}
