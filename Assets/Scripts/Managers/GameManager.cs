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
        floorTime += Time.deltaTime;
        Debug.Log(floorTime);
    }


    public void UpdateTimes()
    {
        floorTime = Math.Round(floorTime, 3);
        totalTime += floorTime;
    }


    public void NextFloor()
    {
        Debug.Log("game manager, go to next floor");
        floorTime = 0;
    }
}
