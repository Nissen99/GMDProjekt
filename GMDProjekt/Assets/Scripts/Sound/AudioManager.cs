using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (var sound in sounds)
        {
            sound.Source = gameObject.AddComponent<AudioSource>();
            sound.Source.clip = sound.Clip;
            sound.Source.volume = sound.Volume;
            sound.Source.pitch = sound.Pitch;
            sound.Source.loop = sound.Loop;
        }

        var masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        SetMasterVolume(masterVolume);
    }

    private void Start()
    {
        Play(AUDIOCLIPS.THEME);
    }

    public void Play(string nameOfSoundToPlay)
    {
        var soundToPlay = Array.Find(sounds, sound => sound.Name == nameOfSoundToPlay);
        if (soundToPlay == null)
        {
            Debug.LogError($"Could not find sound: {nameOfSoundToPlay}");
            return;
        }

        soundToPlay.Source.Play();
    }

    public void SetMasterVolume(float masterVolume)
    {
        foreach (var sound in sounds)
        {
            sound.Source.volume = masterVolume * sound.Volume;
        }
    }
}