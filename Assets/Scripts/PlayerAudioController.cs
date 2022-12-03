using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void AudioPlayer(AudioClip clip)
    {
        audioSource.PlayOneShot(clip, 1F);
    }
    public void AudioPlayerSoft(AudioClip clip)
    {
        audioSource.PlayOneShot(clip, 0.25F);
    }
}
