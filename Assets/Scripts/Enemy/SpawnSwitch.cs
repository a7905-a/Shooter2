using System;
using UnityEngine;

public class SpawnSwitch : MonoBehaviour
{
    public static event Action OnRobotBattleStart;

    // [SerializeField] GameObject stageDoor;
    // [SerializeField] GameObject spawngatePrefab;
    // [SerializeField] Transform[] spawngateSpawnPoints;
    
    bool isTriggered = false;
    const string PLAYER_TAG = "Player";

    void OnTriggerEnter(Collider other)
    {
        if ( !isTriggered && other.CompareTag(PLAYER_TAG))
        {
            isTriggered = true;
            OnRobotBattleStart?.Invoke();

            //스폰하는 건 스폰 스크립트로 이동
            // foreach (Transform spawnPoint in spawngateSpawnPoints)
            // {
            //     GameObject gateSpawn = Instantiate(spawngatePrefab, spawnPoint.position, spawnPoint.rotation);
            //     enemiesBasket.Add(gateSpawn);
            //     Spawn spawnScript = gateSpawn.GetComponent<Spawn>();
            //     if (spawnScript != null)
            //     {
            //         spawnScript.SetUpSwitch(this);
            //     }
            //     EnemyHealth enemyHealthScript = gateSpawn.GetComponent<EnemyHealth>();
            //     if (enemyHealthScript != null)
            //     {
            //         enemyHealthScript.SetUpSwitch(this);
            //     }

            // }
        
        }

    }

    // public void robotRegister(GameObject enemyRobot)
    // {
    //     enemiesBasket.Add(enemyRobot);
    // }

    // public void robotRemove(GameObject enemyRobot)
    // {
    //     if (enemiesBasket.Contains(enemyRobot))
    //     {
    //         enemiesBasket.Remove(enemyRobot);

    //     }
    // }
}
