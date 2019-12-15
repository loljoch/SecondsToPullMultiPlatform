using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum AudioChannel
{
    Master,
    SoundEffects,
    Music
}


public class SoundManager
{
    private List<AudioSource> audioSources = new List<AudioSource>();
    private AudioMixer audioMixer;
    private AudioMixerGroup master;
    private AudioMixerGroup sEffects;
    private AudioMixerGroup music;

    public SoundManager(AudioMixer audioMixer, AudioMixerGroup master, AudioMixerGroup sEffects, AudioMixerGroup music)
    {
        this.audioMixer = audioMixer;
        this.master = master;
        this.sEffects = sEffects;
        this.music = music;
    }

    public void PlayAudioClip(AudioClip _clip, float _volume = 1, bool looping = false, bool music = false, AudioChannel audioChannel = AudioChannel.SoundEffects)
    {
        AudioSource audioSource = GetAudioSource();
        if (music)
        {
            Object.DontDestroyOnLoad(audioSource);
        }
        audioSource.outputAudioMixerGroup = GetAudioGroup(audioChannel);
        if (looping)
        {
            audioSource.loop = looping;
            audioSource.clip = _clip;
            audioSource.Play();
        } else
        {
            audioSource.PlayOneShot(_clip, _volume);
        }
    }

    private AudioMixerGroup GetAudioGroup(AudioChannel audioChannel)
    {
        switch (audioChannel)
        {
            case AudioChannel.Master:
                return master;
            case AudioChannel.SoundEffects:
                return sEffects;
            case AudioChannel.Music:
                return music;
            default:
                return null;
        }
    }

    private AudioSource CreateAudiosource()
    {
        GameObject _gameObject = new GameObject();
        AudioSource _audiosource = _gameObject.AddComponent<AudioSource>();
        audioSources.Add(_audiosource);
        return _audiosource;
    }

    private AudioSource GetAudioSource()
    {
        for (int i = 0; i < audioSources.Count; i++)
        {
            if (audioSources[i] != null)
            {
                if (!audioSources[i].isPlaying)
                {
                    return audioSources[i];
                }
            }
        }

        return CreateAudiosource();
    }
}
