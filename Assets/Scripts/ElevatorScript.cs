using UnityEngine;

public class ElevatorScript : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("ELEVATOR!");
        GameManager.instance.isTimerActive = false;
        GameManager.instance.RoundTimes();
        MenuManager.instance.FloorCompletionMenu();
        AudioManager.instance.Sound(GetComponent<AudioSource>().clip, true, true, AudioManager.SoundType.MUSIC);
    }
}
