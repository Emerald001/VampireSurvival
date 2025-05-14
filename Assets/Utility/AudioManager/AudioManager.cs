using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private List<Sound> soundslist = new();

    private void Awake() {
        foreach (Sound s in soundslist) {
            if (s.sourceObject != null)
                s.Source = s.sourceObject.AddComponent<AudioSource>();
            else
                s.Source = gameObject.AddComponent<AudioSource>();

            s.Source.clip = s.clip;

            s.Source.volume = s.volume;
            s.Source.pitch = s.pitch;
            s.Source.loop = s.loop;
            s.Source.maxDistance = s.range;

            s.Source.outputAudioMixerGroup = s.mixerGroup;
            s.Source.spatialBlend = s.spacialBlend;
        }
    }

    public void PlayAudio(string name) => soundslist.Find(sound => sound.name == name)?.Source.Play();

    public void PlayLoopedAudio(string name, bool onOrOff) {
        if (onOrOff)
            soundslist.Find(sound => sound.name == name)?.Source.Play();
        else
            soundslist.Find(sound => sound.name == name)?.Source.Stop();
    }
}

[System.Serializable]
public class Sound {
    public AudioSource Source { get; set; }

    [Header("Audio")]
    public string name;
    public AudioClip clip;
    public AudioMixerGroup mixerGroup;
    [Tooltip("Can be Null if it should be on the player.")]
    public GameObject sourceObject = null;

    [Header("Audio Settings")]
    [Range(0, 1f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;
    [Range(0, 1f)]
    public float spacialBlend;
    [Tooltip("Only applicable if the Spatial blend is above 0.5.")]
    [Range(0, 500)]
    public float range;
    public bool loop;
}
