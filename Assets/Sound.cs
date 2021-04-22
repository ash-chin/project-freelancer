using UnityEngine.Audio;
using UnityEngine;

//if you want this class to appear in the inspector you must include the line below for reasons I do not understand :)
[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    //The range lines allow for sliders to be created in the inspector
    [Range(0f, 1f)]
    public float volume;

    [Range(.1f, 3)]
    public float pitch;

    //we dont want this to be changed during testing so hide it
    [HideInInspector]
    public AudioSource source;
}
