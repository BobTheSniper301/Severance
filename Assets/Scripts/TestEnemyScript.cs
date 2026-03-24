using UnityEngine;

public class TestEnemyScript : MonoBehaviour
{
    public GameObject keycard;

    public void Die()
    {
        Destroy(this.gameObject);
        ItemInventoryManager.instance.SpawnObject(keycard, this.transform.position);
    }
}
