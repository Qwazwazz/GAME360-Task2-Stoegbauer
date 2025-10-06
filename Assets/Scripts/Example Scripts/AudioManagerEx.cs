using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    SHOOT,
    COIN
}

[RequireComponent(typeof(AudioSource))]
public class AudioManagerEx : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundList;
    private AudioSource audioSource;
    private static AudioManagerEx instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(SoundType sound, float volume = 1)
    {
        instance.audioSource.PlayOneShot(instance.soundList[(int)sound], volume);
    }
}
