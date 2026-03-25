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
            // Debug.Log(floorTime);
        }
    }


    public void UpdateTimes()
    {
        floorTime = (float)Math.Round(unroundedfloorTime, 2);
        totalTime = (float)Math.Round(unroundedtotalTime, 2);
        Debug.Log("unroundedfloor time: " + unroundedfloorTime);
        Debug.Log("floor time: " + floorTime);
        SaveDataManager.instance.currentFloorTime = floorTime;
        SaveDataManager.instance.currentTotal = totalTime;
    }


    public void NextFloor()
    {
        Debug.Log("game manager, go to next floor");
        unroundedfloorTime = 0;
        floorTime = 0;
        AudioManager.instance.Sound(null, false, false, 0);
        isTimerActive = true;
        SceneManager.LoadScene("Floor" + SaveDataManager.instance.currentFloor.ToString());
    }
}
