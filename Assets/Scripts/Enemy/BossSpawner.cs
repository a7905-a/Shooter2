using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] BossSpawn bossSpawn;
    [SerializeField] BossAttack bossAttack;
    const string PLAYER_STRING = "Player";

    void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag(PLAYER_STRING))
        {
            bossSpawn.StartBossRise();
            bossAttack.StartFiring();
            Destroy(gameObject);
        }
    }

}
