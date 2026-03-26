using UnityEngine;

public class SaveDataManager : MonoBehaviour
{
    public static SaveDataManager instance { get; private set; }

    // Will save "floor1Best" and "floor1BestTotal" (can change numbers)
    // Things we save
    public float[] floorBests = new float[5];
    public float[] bestRunTotals = new float[5]; // So players can see how they compare to both the best they have done on that floor, and the best full run they have done
    public float[] currentRunTotals = new float[5];
    public float currentTotal;
    public int currentFloor = 1;


    public bool isActiveRun;
    public float finalTotal;
    public float currentFloorTime;
    public string[] floors = new string[]{"floor1", "floor2", "floor3", "floor4", "floor5"};


    void Awake()
    {

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

    }

    void Start()
    {
        LoadAll();
    }

    void Update()
    {
        if (currentFloor == 1 || currentFloor == 6)
        {
            isActiveRun = false;
        }
        else isActiveRun = true;
    }

    public void UpdateTimes()
    {
        Debug.Log("current floor: " + currentFloor);
        currentRunTotals[currentFloor - 1] = currentFloorTime;
        Debug.Log("current floor time: " + currentFloorTime);
        if (floorBests[currentFloor - 1] == 0 || currentFloorTime < floorBests[currentFloor - 1])
        {
            floorBests[currentFloor - 1] = currentFloorTime;
            Debug.Log("override current floor time: " + currentFloorTime);
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
        SaveCurrentFloor();
        SaveCurrentTotal();
        SaveCurrentRunTotals();
        PlayerPrefs.Save();
    }

    public void SaveFloorBests()
    {
        for (int i = 0; i < floorBests.Length; i++)
        {
            PlayerPrefs.SetFloat(floors[i] + "Best", floorBests[i]);
        }
    }

    public void SaveCurrentRunTotals()
    {
        for (int i = 0; i < floorBests.Length; i++)
        {
            PlayerPrefs.SetFloat(floors[i] + "CurrentTotal", currentRunTotals[i]);
        }
    }

    public void SaveCurrentTotal()
    {
        PlayerPrefs.SetFloat("currentTotal", currentTotal);
    }

    public void SaveCurrentFloor()
    {
        PlayerPrefs.SetInt("currentFloor", currentFloor);
    }

    #endregion

    #region Load

    public void LoadAll()
    {
        Debug.Log("load all");
        LoadFloorBests();
        LoadCurrentFloor();
        LoadCurrentTotal();
        LoadCurrentRunTotals();
    }

    public void LoadFloorBests()
    {
        for (int i = 0; i < floorBests.Length; i++)
        {
            floorBests[i] = PlayerPrefs.GetFloat(floors[i] + "Best");
        }
    }

    public void LoadCurrentRunTotals()
    {
        if (isActiveRun) return;

        for (int i = 0; i < floorBests.Length; i++)
        {
            bestRunTotals[i] = PlayerPrefs.GetFloat(floors[i] + "CurrentTotal");
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
