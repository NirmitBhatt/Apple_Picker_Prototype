using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public const string GAME_BACKGROUND_AUDIO = "GameBackground";

    private const string APPLE_SPAWN_AUDIO = "AppleSpawn";
    private const string BASKET_BREAK_AUDIO = "BasketBreak";
    private const string APPLE_CATCH_AUDIO = "AppleCatch";

    [SerializeField] private Sounds[] sounds;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        GetSingletonInstance();
        GetAudioSourceArray();
    }

    private void OnEnable()
    {
        AppleSpawner.AppleSpawned += PlayAppleSpawnAudio;
        BasketController.AppleCollect += PlayAppleCatchAudio;
        GameController.OnBasketBreak += PlayBasketBreakAudio;
        GameController.OnGameStart += PlayGameBackgroundAudio;
        GameController.OnGameOver += StopGameBackgroundAudio;
    }

    
    private void OnDisable()
    {
        AppleSpawner.AppleSpawned -= PlayAppleSpawnAudio;
        BasketController.AppleCollect -= PlayAppleCatchAudio;
        GameController.OnBasketBreak -= PlayBasketBreakAudio;
        GameController.OnGameStart -= PlayGameBackgroundAudio;
        GameController.OnGameOver -= StopGameBackgroundAudio;
    }

    private void PlayAppleSpawnAudio()
    {
        PlayAudio(APPLE_SPAWN_AUDIO);
    }

    private void PlayBasketBreakAudio()
    {
        PlayAudio(BASKET_BREAK_AUDIO);
    }

    private void PlayGameBackgroundAudio()
    {
        PlayAudio(GAME_BACKGROUND_AUDIO);
    }

    private void PlayAppleCatchAudio()
    {
        PlayAudio(APPLE_CATCH_AUDIO);
    }

    private void StopGameBackgroundAudio()
    {
        StopAudio(GAME_BACKGROUND_AUDIO);
    }

    private void GetAudioSourceArray()
    {
        foreach (Sounds sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
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

    public void SetVolume(string name, float volume)
    {
        Sounds s = Array.Find(sounds, sounds => sounds.name == name);
        if (s != null && s.source != null)
        {
            s.source.volume = volume;
        }
        else
        {
            Debug.LogWarning("Audio Source: " + name + " not found or has no audio source!");
        }
    }
}
