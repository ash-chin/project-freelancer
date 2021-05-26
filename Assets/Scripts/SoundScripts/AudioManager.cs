//Sources: https://www.youtube.com/watch?v=6OT43pvUyfY
/* The audio manager is specifically for sounds that are not attached to a game object
 * if you are looking for something related to whale audio then look on the whale object in the 
 * ZacsTestEnvironment scene at least until we finalize the changes to the whale prefab :)
 */
using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] music;
    //public Sound[] sounds;
    public static AudioManager instance;
    public AudioSource currentTrackSrc;
    
    [HideInInspector]
    public int currentTrackNumber = 0;

    void Awake()
    {
        /* 
         * This if else block says if there is not an instance of the audio manager in the scene
         *  then instantiate one otherwise there already is one so delete this one
         */
        if (instance == null)
            instance = this;

        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        /*
        foreach (Sound s in sounds)
        {
            //this line instantiates an AudioSource component on the AudioManager and assigns that to the 
            //current sound's source variable
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }*/

        foreach (Sound s in music)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = 1f;
            s.source.loop = s.loop;
            s.source.playOnAwake = true;
        }
    }

    void Start()
    {

        Sound s = music[0];
        s.source.Play();
        currentTrackSrc = s.source;
    }

    void Update()
    {
        /*
        if (!playerAudioSrc.isPlaying)
        {
            if (playerTrackNumber < 3)
                playerTrackNumber++;

            else
                playerTrackNumber = 0;

            playerAudioSrc.clip = music[playerTrackNumber].clip;
            playerAudioSrc.Play();
        }
        */
       
        if (!currentTrackSrc.isPlaying)
        {
            if (currentTrackNumber < 3)
                currentTrackNumber++;

            else
                currentTrackNumber = 0;

            music[currentTrackNumber].source.Play();

            currentTrackSrc = music[currentTrackNumber].source;
            
        }
    }


    public void PlaySong(string name)
    {
        Sound s = Array.Find(music, sound => sound.name == name);
        if (s == null)
            return;
        s.source.Play();
    }
}
