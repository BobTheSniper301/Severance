using UnityEngine;

public class ElevatorScript : MonoBehaviour
{
    [SerializeField] GameObject elevatorDoorsClosed;
    [SerializeField] BoxCollider boxCollider;
    public void Open()
    {
        // TODO: Make an animation for this
        Destroy(elevatorDoorsClosed);
    }
    void OnTriggerEnter(Collider other)
    {
        AudioManager.instance.backgroundAudio.Stop();
        boxCollider.enabled = false;
        Debug.Log(this.gameObject.name);
        Debug.Log("finish floor");
        GameManager.instance.isTimerActive = false;
        GameManager.instance.UpdateTimes();
        MenuManager.instance.FloorCompletionMenu();
        AudioManager.instance.Sound(GetComponent<AudioSource>().clip, true, true, AudioManager.SoundType.MUSIC);
    }
}
