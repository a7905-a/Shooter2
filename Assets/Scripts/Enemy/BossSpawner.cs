using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] BossSpawn bossSpawn;
    const string PLAYER_STRING = "Player";

    void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag(PLAYER_STRING))
        {
            bossSpawn.StartBossRise();
            Destroy(gameObject);
        }
    }

}
