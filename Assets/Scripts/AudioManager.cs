using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioClip wooshAudio, menuMusicDesktop, menuMusicAndroid;
    public SoundManager soundManager;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioMixerGroup master;
    [SerializeField] private AudioMixerGroup sEffects;
    [SerializeField] private AudioMixerGroup music;
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get { return instance; }
        private set { instance = value; }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            soundManager = new SoundManager(audioMixer, master, sEffects, music);
        } else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
#if UNITY_ANDROID
        soundManager.PlayAudioClip(menuMusicAndroid, 0.01f, true, true, AudioChannel.Music);
#elif UNITY_STANDALONE_WIN
        soundManager.PlayAudioClip(menuMusicDesktop, 0.01f, true, true, AudioChannel.Music);
#endif
    }

}
