using UnityEngine;

public class AiBehaviourScript : MonoBehaviour
{
    public bool sleepState = false;
    public bool playerSeen = false;

    Transform[] Patrols;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Patrols = GameObject.Find("Patrols").GetComponentsInChildren<Transform>();
        Patrols.
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
