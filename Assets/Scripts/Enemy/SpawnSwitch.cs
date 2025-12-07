using System.Collections.Generic;
using UnityEngine;

public class SpawnSwitch : MonoBehaviour
{
    [SerializeField] GameObject stageDoor;
    [SerializeField] GameObject spawngatePrefab;
    [SerializeField] Transform[] spawngateSpawnPoints;
    List<GameObject> enemiesBasket = new List<GameObject>();
    bool isTriggered = false;
    const string PLAYER_TAG = "Player";

    void Update()
    {
        if (isTriggered && enemiesBasket.Count == 0)
        {
            
            stageDoor.SetActive(false);
            
        }
    }



    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_TAG))
        {
            isTriggered = true;

            foreach (Transform spawnPoint in spawngateSpawnPoints)
            {
                GameObject gateSpawn = Instantiate(spawngatePrefab, spawnPoint.position, Quaternion.identity);
                enemiesBasket.Add(gateSpawn);
                Spawn spawnScript = gateSpawn.GetComponent<Spawn>();
                if (spawnScript != null)
                {
                    spawnScript.SetUpSwitch(this);
                }
                EnemyHealth enemyHealthScript = gateSpawn.GetComponent<EnemyHealth>();
                if (enemyHealthScript != null)
                {
                    enemyHealthScript.SetUpSwitch(this);
                }

            }
        
        }
    }

    public void robotRegister(GameObject enemyRobot)
    {
        enemiesBasket.Add(enemyRobot);
    }

    public void robotRemove(GameObject enemyRobot)
    {
        if (enemiesBasket.Contains(enemyRobot))
        {
            enemiesBasket.Remove(enemyRobot);

        }
    }
}
