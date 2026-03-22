using System;
using UnityEngine;
using UnityEngine.UI;

// Soley for major game sounds / 2d sounds
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    [SerializeField] AudioSource audioSource;
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;

    SoundType activeSoundType;

    public float musicVolume = 1;
    public float sfxVolume = 1;

    public enum SoundType
    {
        GENERAL,
        MUSIC,
        SFX
    }

    [SerializeField] AudioSource buttonAudioSource;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void Sound(AudioClip audioClip, bool isPlaying, bool isLooping, SoundType soundType)
    {
        audioSource.loop = isLooping;
        if (isPlaying)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
            return;
        }

        activeSoundType = soundType;
    }


    public void ChangeVolume()
    {
        AudioListener.volume = masterVolumeSlider.value;
        musicVolume = musicVolumeSlider.value;
        sfxVolume = sfxVolumeSlider.value;
        buttonAudioSource.volume = (float)Math.Round(sfxVolumeSlider.value);
        MenuManager.instance.UpdateSettingsMenu();
        if (activeSoundType == SoundType.MUSIC)
        {
            audioSource.volume = musicVolume;
        }
        else if (activeSoundType == SoundType.SFX)
        {
            audioSource.volume = sfxVolume;
        }
    }

    public void ButtonSFX()
    {
        buttonAudioSource.Play();
    }
}
