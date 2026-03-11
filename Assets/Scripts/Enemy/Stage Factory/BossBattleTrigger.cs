using System;
using UnityEngine;

public class BossBattleTrigger : MonoBehaviour
{
    public static event Action OnBossBattleStart;
    // [SerializeField] BossSpawn bossSpawn;
    // [SerializeField] BossAttack bossAttack;
    const string PLAYER_STRING = "Player";

    void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag(PLAYER_STRING))
        {
            // bossSpawn.StartBossRise();
            // bossAttack.StartFiring();
            OnBossBattleStart?.Invoke();
            Destroy(gameObject);
        }
    }

}
