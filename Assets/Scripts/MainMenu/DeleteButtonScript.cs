using UnityEngine;

public class DeleteButtonScript : MonoBehaviour
{
    public void DeleteAll()
    {
        SaveDataManager.instance.DeleteAll();
    }
}
