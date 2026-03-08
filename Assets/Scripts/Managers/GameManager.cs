using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }



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



    public void NextFloor()
    {
        Debug.Log("game manager, go to next floor");
    }
}
