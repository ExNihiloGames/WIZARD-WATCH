using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioMixerGroup musicMixerGroup;
    [SerializeField] private AudioMixerGroup effectsMixerGroup;
    [SerializeField] private Sound[] sounds;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        foreach (Sound sound in sounds)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            sound.source = source;
            source.clip = sound.audioClip;
            source.loop = sound.isLoop;
            source.volume = sound.volume;
            source.playOnAwake = sound.playOnAwake;

            switch (sound.audioType)
            {
                case Sound.AudioTypes.soundEffects:
                    source.outputAudioMixerGroup = effectsMixerGroup;
                    break;
                case Sound.AudioTypes.music:
                    source.outputAudioMixerGroup = musicMixerGroup;
                    break;
            }

            if (source.playOnAwake)
            {
                source.Play();
            }
        }
    }

    private Sound GetSound(string clipName)
    {
        Sound s = Array.Find(sounds, dummySound => dummySound.clipName == clipName);
        if (s == null)
        {
            Debug.LogError("Sound " + clipName + " does not exist");
        }
        else
        {

            Debug.Log("sound found");
        }
        return s;
    }

    public void Play(string clipName)
    {
        GetSound(clipName).source.Play();
    }

    public void Stop(string clipName)
    {
        GetSound(clipName).source.Stop();
    }
}
