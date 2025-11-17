using System.Collections;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] GameObject robotPrefab;
    [SerializeField] Transform spawnPoints;
    float spawnInterval = 2f;
    SpawnSwitch spawnSwitch;

    public void SetUpSwitch(SpawnSwitch spawnSwitch)
    {
        this.spawnSwitch = spawnSwitch;
        StartCoroutine(RobotSpawn());
    }



    IEnumerator RobotSpawn()
    {
        while (true)
        { 
            yield return new WaitForSeconds(spawnInterval);
            GameObject enemyRobot = Instantiate(robotPrefab, spawnPoints.position, Quaternion.identity);
            if (spawnSwitch != null)
            {
                spawnSwitch.robotRegister(enemyRobot);
                EnemyHealth enemyHealthScript = enemyRobot.GetComponent<EnemyHealth>();
                if (enemyHealthScript != null)
                {
                    enemyHealthScript.SetUpSwitch(spawnSwitch);
                }
                
            }
        }

    }

}
