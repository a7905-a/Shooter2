using System.Collections;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] GameObject robotPrefab;
    [SerializeField] Transform spawnPoints;

    void Start()
    {
        StartCoroutine(RobotSpawn());
    }

    void Update()
    {

    }

    IEnumerator RobotSpawn()
    {
        while (true)
        { 

        yield return new WaitForSeconds(10f);
        Instantiate(robotPrefab, spawnPoints.position, Quaternion.identity);
        }

    }

}
