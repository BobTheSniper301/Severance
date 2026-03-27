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
    float soundDelayTimer  = 5;
    bool isSoundDelayTimerActive;


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

    void Start()
    {
        soundDelayTimer = 5;
        StartSoundDelayTimer();
    }

    void Update()
    {
        if (isTimerActive)
        {
            unroundedfloorTime += Time.deltaTime;
            unroundedtotalTime += Time.deltaTime;
        }
        if (isSoundDelayTimerActive)
        {
            soundDelayTimer -= Time.deltaTime;
        }
        if (soundDelayTimer > 0)
        {
            AudioListener.volume = 0;
        }
        else
        {
            AudioManager.instance.ChangeVolume();
            isSoundDelayTimerActive = false;
        }
    }

    public void StartSoundDelayTimer()
    {
        isSoundDelayTimerActive = true;
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
