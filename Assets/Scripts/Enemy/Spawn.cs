using System.Collections;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] GameObject robotPrefab;
    [SerializeField] Transform spawnPoints;
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
            yield return new WaitForSeconds(5f);
            GameObject enemyRobot = Instantiate(robotPrefab, spawnPoints.position, Quaternion.identity);
            if (spawnSwitch != null)
            {
                spawnSwitch.robotRegister(enemyRobot);        
                Debug.Log("로봇 스폰");
                
            }
        }

    }

}
