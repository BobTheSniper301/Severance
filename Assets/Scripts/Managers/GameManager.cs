using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public float floorTime;
    public float totalTime;
    double unroundedfloorTime;
    double unroundedtotalTime;
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
            unroundedfloorTime += Time.deltaTime;
            unroundedtotalTime += Time.deltaTime;
        }
    }


    public void UpdateTimes()
    {
        floorTime = (float)Math.Round(unroundedfloorTime, 2);
        totalTime = (float)Math.Round(unroundedtotalTime, 2);
        SaveDataManager.instance.currentFloorTime = floorTime;
        SaveDataManager.instance.currentTotal = totalTime;
    }


    public void NextFloor()
    {
        unroundedfloorTime = 0;
        floorTime = 0;
        AudioManager.instance.Sound(null, false, false, 0);
        isTimerActive = true;
        SceneManager.LoadScene("Floor" + SaveDataManager.instance.currentFloor.ToString());
    }
}
