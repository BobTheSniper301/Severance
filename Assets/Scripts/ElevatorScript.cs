using UnityEngine;

public class ElevatorScript : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("ELEVATOR!");
    }
}
