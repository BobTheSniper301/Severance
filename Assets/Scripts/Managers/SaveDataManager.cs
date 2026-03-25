using UnityEngine;

public class SaveDataManager : MonoBehaviour
{
    public static SaveDataManager instance { get; private set; }

    // Will save "floor1Best" and "floor1BestTotal" (can change numbers)
    // Things we save
    public float[] floorBests = new float[5];
    public float[] bestRunTotals = new float[5]; // So players can see how they compare to both the best they have done on that floor, and the best full run they have done
    public float currentTotal;
    public int currentFloor = 1;

    public float finalTotal;
    public float currentFloorTime;
    public float[] currentRunTotals = new float[5];
    public string[] floors = new string[]{"floor1", "floor2", "floor3", "floor4", "floor5"};


    void Awake()
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
        LoadAll();
    }


    public void UpdateTimes()
    {
        if (floorBests[currentFloor-1] == 0 || currentFloorTime < floorBests[currentFloor-1])
        {
            floorBests[currentFloor-1] = currentFloorTime;
            Debug.Log("override current floor time: " + currentFloorTime);
        }
        if (bestRunTotals[4] == 0 || finalTotal < bestRunTotals[4])
        {
            for (int i = 0; i < bestRunTotals.Length; i++)
            {
                bestRunTotals[i] = currentRunTotals[i];
            }
            Debug.Log("override total runs time");
        }
    }

    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
        LoadAll();
        SaveAll();
    }

    #region Save

    public void SaveAll()
    {
        Debug.Log("save all");
        SaveFloorBests();
        SaveBestRunTotals();
        SaveCurrentFloor();
        SaveCurrentTotal();
        PlayerPrefs.Save();
    }

    public void SaveFloorBests()
    {
        for (int i = 0; i < floorBests.Length; i++)
        {
            PlayerPrefs.SetFloat(floors[i] + "Best", floorBests[i]);
        }
    }

    public void SaveBestRunTotals()
    {
        for (int i = 0; i < floorBests.Length; i++)
        {
            PlayerPrefs.SetFloat(floors[i] + "BestTotal", bestRunTotals[i]);
        }
    }

    public void SaveCurrentTotal()
    {
        PlayerPrefs.SetFloat("currentTotal", currentTotal);
    }

    public void SaveCurrentFloor()
    {
        currentFloor = Mathf.Clamp(currentFloor, 1, floors.Length);
        PlayerPrefs.SetInt("currentFloor", currentFloor);
    }

    #endregion

    #region Load

    public void LoadAll()
    {
        Debug.Log("load all");
        LoadFloorBests();
        LoadCurrentFloor();
        LoadBestRunTotals();
        LoadCurrentTotal();
    }

    public void LoadFloorBests()
    {
        for (int i = 0; i < floorBests.Length; i++)
        {
            floorBests[i] = PlayerPrefs.GetFloat(floors[i] + "Best");
        }
    }

    public void LoadBestRunTotals()
    {
        for (int i = 0; i < floorBests.Length; i++)
        {
            bestRunTotals[i] = PlayerPrefs.GetFloat(floors[i] + "BestTotal");
        }
    }

    public void LoadCurrentTotal()
    {
        currentTotal = PlayerPrefs.GetFloat("currentTotal");
    }

    public void LoadCurrentFloor()
    {
        currentFloor = PlayerPrefs.GetInt("currentFloor", 1);
    }

    #endregion
}
