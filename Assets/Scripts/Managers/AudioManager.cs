using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    public AudioSource activeAudioSource;
    [SerializeField] Slider masterVolumeSlider;

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


// Def want to change in future probably
    public void AudioStart(AudioSource audioSource)
    {
        activeAudioSource = audioSource;
        audioSource.Play();
    }
    public void AudioStop()
    {
        activeAudioSource.Stop();
    }

    public void ChangeVolume()
    {
        AudioListener.volume = masterVolumeSlider.value;
    }
}
