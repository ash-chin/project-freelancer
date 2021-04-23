using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Audio;
using UnityEngine;

public class WhaleSound : MonoBehaviour
{
    public Sound[] whaleSounds;
    AudioSource whaleAudioSrc;

    private void Awake()
    {
        whaleAudioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!whaleAudioSrc.isPlaying)
        {
            whaleAudioSrc.clip = whaleSounds[UnityEngine.Random.Range(0, 2)].clip;
            whaleAudioSrc.Play();
        }
    }
}
