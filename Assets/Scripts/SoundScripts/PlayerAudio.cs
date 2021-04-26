using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public Sound[] shutterSounds;
    private AudioSource playerShipAudioSource;

    private void Awake()
    {
        playerShipAudioSource = GetComponent<AudioSource>();
    }

    public void ShutterNoise()
    {
        if (!playerShipAudioSource.isPlaying)
        {
            playerShipAudioSource.clip = shutterSounds[UnityEngine.Random.Range(0, 6)].clip;
            playerShipAudioSource.Play();
        }
        
    }
}
