using TMPro;
using UnityEngine;

public class RecordsMenuScript : MonoBehaviour
{
    [SerializeField] TMP_Text totalTimeText;
    [SerializeField] TMP_Text [] floorTimesText;

    void Start()
    {
        UpdateTimes();
    }
    public void UpdateTimes()
    {
        totalTimeText.text = "Total: " + SaveDataManager.instance.bestRunTotals[4].ToString() + "s";
        for (int i = 0; i < SaveDataManager.instance.floorBests.Length; i++)
        {
            floorTimesText[i].text = "Floor " + (i + 1).ToString() + ": " + SaveDataManager.instance.floorBests[i].ToString() + "s";
        }
    }
}
