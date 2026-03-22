using System;
using UnityEngine;

public abstract class InteractableObjectScript : MonoBehaviour
{
    [SerializeField] float weight;
    public float itemThrownPower = 0.1f;
    public bool isThrownOrDropped;
    [SerializeField] SphereCollider itemSoundBubble;
    public float itemThrownSoundMultiplier = 0.2f;
    public float itemSoundBubbleSizeMultiplier = 5;
    //[SerializeField] sound effect

    public abstract void Interact();

    void OnCollisionEnter(Collision collision)
    {
        itemSoundBubble.enabled = true;
        Debug.Log("collision + object: " + this.name);
        Debug.Log("throw or dropped");
        GetComponent<AudioSource>().volume = Mathf.Clamp(AudioManager.instance.sfxVolume * (itemThrownPower * (itemThrownSoundMultiplier * weight)), 0.1f, 1);
        GetComponent<AudioSource>().Play();
        itemSoundBubble.radius = GetComponent<AudioSource>().volume * itemSoundBubbleSizeMultiplier;
        isThrownOrDropped = false;
        itemThrownPower = 0.1f; // reset to default value, mostly for if item is dropped not thrown
        // itemSoundBubble.enabled = false;
        
    }


}
