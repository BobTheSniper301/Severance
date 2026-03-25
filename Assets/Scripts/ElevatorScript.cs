using UnityEngine;

public class ElevatorScript : MonoBehaviour
{
    [SerializeField] GameObject elevatorDoorsClosed;
    public void Open()
    {
        // TODO: Make an animation for this
        Destroy(elevatorDoorsClosed);
    }
    void OnTriggerEnter(Collider other)
    {
        GameManager.instance.isTimerActive = false;
        GameManager.instance.UpdateTimes();
        MenuManager.instance.FloorCompletionMenu();
        AudioManager.instance.Sound(GetComponent<AudioSource>().clip, true, true, AudioManager.SoundType.MUSIC);
    }
}
