using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public double floorTime;
    public double totalTime;
    public bool isTimerActive;


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

    void Update()
    {
        if (isTimerActive)
        {
            floorTime += Time.deltaTime;
            totalTime += Time.deltaTime;
            // Debug.Log(floorTime);
        }
    }


    public void RoundTimes()
    {
        floorTime = Math.Round(floorTime, 2);
        totalTime = Math.Round(totalTime, 2);
    }


    public void NextFloor()
    {
        Debug.Log("game manager, go to next floor");
        floorTime = 0;
        AudioManager.instance.AudioStop();
    }
}
