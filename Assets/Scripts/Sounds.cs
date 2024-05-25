using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

[Serializable]
public class Sounds
{
    public string name;
    public AudioClip clip;
    [HideInInspector]
    public AudioSource source;
    [Range(0f, 1f)]
    public float volume;
    [Range(0f, 1f)]
    public float pitch;
    public bool loop;
}