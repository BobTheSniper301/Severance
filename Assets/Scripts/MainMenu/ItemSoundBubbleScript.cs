using UnityEngine;

public class ItemSoundBubbleScript : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        other.gameObject.transform.parent.GetComponent<AiBehaviourScript>().HeardSound(this.gameObject.transform, 1);   
    }
}
