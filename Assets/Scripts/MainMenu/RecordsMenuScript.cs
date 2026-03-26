using TMPro;
using UnityEngine;

public class RecordsMenuScript : MonoBehaviour
{
    [SerializeField] TMP_Text totalTimeText;
    [SerializeField] TMP_Text [] floorTimesText;
    float totalTime;

    void OnEnable()
    {
        UpdateTimes();
    }
    public void UpdateTimes()
    {
        totalTime = 0;
        foreach (float floorTime in SaveDataManager.instance.floorBests)
        {
            Debug.Log ("totalTime: " + totalTime + " + " + floorTime + " :floorTime");
            totalTime += floorTime;
        }
        totalTimeText.text = "Total: " + totalTime.ToString() + "s";
        for (int i = 0; i < SaveDataManager.instance.floorBests.Length; i++)
        {
            floorTimesText[i].text = "Floor " + (i + 1).ToString() + ": " + SaveDataManager.instance.floorBests[i].ToString() + "s";
        }
    }
}
