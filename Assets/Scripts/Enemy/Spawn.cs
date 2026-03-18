using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawn : MonoBehaviour
{
    public static event Action<GameObject> OnEnemySpawn;

    [SerializeField] GameObject robotPrefab;
    [SerializeField] int spawnCount = 5;
    [SerializeField] float spawnRadius = 3f;
    
 
    
    void OnEnable()
    {
        SpawnSwitch.OnRobotBattleStart += StartSpawnRobot;
    }

    void OnDisable()
    {
        SpawnSwitch.OnRobotBattleStart -= StartSpawnRobot;
    }



    void StartSpawnRobot()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 randomOffset = Random.insideUnitSphere * spawnRadius;
            randomOffset.y = 0; 
            Vector3 spawnPoints = transform.position + randomOffset;

            GameObject enemyRobot = Instantiate(robotPrefab, spawnPoints, transform.rotation);
            OnEnemySpawn?.Invoke(enemyRobot);

        }
    }

    
    void OnDrawGizmosSelected()
    {
        
        Gizmos.color = Color.red;

        
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
