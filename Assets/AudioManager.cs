using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public GameObject playerGameObject, whaleGameObject;
    public Sound[] music;
    public Sound[] sounds;
    public Sound[] whaleSounds;

    AudioSource playerAudioSrc, whaleAudioSrc;
    int playerTrackNumber = 0;
    int whaleTrackNumber = 0;
    void Awake()
    {
        playerAudioSrc = playerGameObject.GetComponent<AudioSource>();

        playerAudioSrc.Play();

        whaleAudioSrc = whaleGameObject.GetComponent<AudioSource>();

        whaleAudioSrc.Play();

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    private void Update()
    {
        if (!playerAudioSrc.isPlaying)
        {
            if (playerTrackNumber < 3)
                playerTrackNumber++;

            else
                playerTrackNumber = 0;

            playerAudioSrc.clip = music[playerTrackNumber].clip;
            playerAudioSrc.Play();
        }

        if(!whaleAudioSrc.isPlaying)
        {
            whaleAudioSrc.clip = whaleSounds[UnityEngine.Random.Range(0, 2)].clip;
            whaleAudioSrc.Play();
        }
    }


    public void Play(string name)
    {

    }
}
