using UnityEngine;

public class ItemSoundBubbleScript : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<AiBehaviourScript>().HeardSound(this.gameObject.transform, 1);
    }
}
