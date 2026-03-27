using System;
using UnityEngine;

public abstract class InteractableObjectScript : MonoBehaviour
{
    [SerializeField] float weight;
    public float itemThrownPower = 0.1f;
    public bool isThrownOrDropped;
    [SerializeField] SphereCollider itemSoundBubble;
    public float itemThrownSoundMultiplier = 0.2f;
    public float itemSoundBubbleSizeMultiplier = 2;
    [SerializeField] AudioClip dropSfx;
    [SerializeField] AudioSource audioSource;

    public abstract void Interact();

    void OnCollisionEnter(Collision collision)
    {
        itemSoundBubble.enabled = true;
        audioSource.volume = Mathf.Clamp(AudioManager.instance.sfxVolume * (itemThrownPower * (itemThrownSoundMultiplier * weight)), 0.01f, 1);
        itemSoundBubble.radius = audioSource.volume * itemSoundBubbleSizeMultiplier;
        // Want held objects to have extra "heft" to them
        if (GetComponent<HeldItemScript>())
        {
            audioSource.volume = Mathf.Clamp(audioSource.volume * 2, 0.01f, 1);
            itemSoundBubble.radius *= 2;
        }
        audioSource.Play();
        isThrownOrDropped = false;
        itemThrownPower = 0.1f; // reset to default value, mostly for if item is dropped not thrown
    }

    void Start()
    {
        audioSource.clip = dropSfx;
    }
}
