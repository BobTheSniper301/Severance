using UnityEngine;

public class TestEnemyScript : MonoBehaviour
{
    public GameObject keycard;
    public bool isVulnerable = true;
    public GameObject killPosition;

    public void StopEnemy()
    {
        // Stop enemy
    }
    public void Die()
    {
        Debug.Log("die");
        ItemInventoryManager.instance.SpawnObject(keycard, this.transform.position);
        Destroy(this.gameObject);
    }
}
