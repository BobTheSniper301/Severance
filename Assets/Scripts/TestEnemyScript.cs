using UnityEngine;

public class TestEnemyScript : MonoBehaviour
{
    public GameObject keycard;
    public bool isVulnerable = true;

    public void Die()
    {
        ItemInventoryManager.instance.SpawnObject(keycard, this.transform.position);
        Destroy(this.gameObject);
    }
}
