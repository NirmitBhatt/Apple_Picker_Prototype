using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sounds[] sounds;
    public static AudioManager instance;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        GetSingletonInstance();
        GetAudioSourceArray();
    }

    private void GetAudioSourceArray()
    {
        foreach (Sounds s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void GetSingletonInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        };
    }

    public void PlayAudio(string name)
    {
        Sounds s = Array.Find(sounds, sounds => sounds.name == name);
        if (s == null)
        {
            Debug.LogWarning("Audio Source: " +name+ " not found!");
            return;
        }
        else
        {
            s.source.Play();
        }
    }

    public void StopAudio(string name)
    {
        Sounds s = Array.Find(sounds, sounds => sounds.name == name);
        s.source?.Stop();
    }

    public void SetPitch(string name, float pitch)
    {
        Sounds s = Array.Find(sounds, sounds => sounds.name == name);
        if (s != null && s.source != null)
        {
            s.source.pitch = pitch;
        }
        else
        {
            Debug.LogWarning("Audio Source: " + name + " not found or has no audio source!");
        }
    }
}
