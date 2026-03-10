using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

// Almost carbon copy of AudioManger but this one is soley for the main menu (I didn't want logic inteded for the main gameplay to be messing around in the main menu)
public class StartAudioManager : MonoBehaviour
{
    public static StartAudioManager instance { get; private set; }

    [SerializeField] AudioSource audioSource;
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;
    [SerializeField] TMP_Text masterVolumeText;
    [SerializeField] TMP_Text musicVolumeText;
    [SerializeField] TMP_Text sfxVolumeText;

    SoundType activeSoundType;

    public float musicVolume = 1;
    public float sfxVolume = 1;

    public enum SoundType
    {
        GENERAL,
        MUSIC,
        SFX
    }

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

    public void UpdateSettingsMenu()
    {
        masterVolumeText.text = Math.Round(masterVolumeSlider.value * 100).ToString();
        musicVolumeText.text = Math.Round(musicVolumeSlider.value * 100).ToString();
        sfxVolumeText.text = Math.Round(sfxVolumeSlider.value * 100).ToString();
    }

    public void ChangeVolume()
    {
        AudioListener.volume = masterVolumeSlider.value;
        musicVolume = musicVolumeSlider.value;
        sfxVolume = sfxVolumeSlider.value;
        UpdateSettingsMenu();
        if (activeSoundType == SoundType.MUSIC)
        {
            audioSource.volume = musicVolume;
        }
        else if (activeSoundType == SoundType.SFX)
        {
            audioSource.volume = sfxVolume;
        }
    }
}
