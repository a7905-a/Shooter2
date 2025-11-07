using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnSwitch : MonoBehaviour
{
    [SerializeField] GameObject stageDoor;
    [SerializeField] GameObject spawngatePrefab;
    [SerializeField] Transform[] spawngateSpawnPoints;
    List<GameObject> enemiesBasket = new List<GameObject>();
    bool isTriggered = false;
    

    //int remainingRobot = 0;

    void Start()
    {

    }
    void Update()
    {
        if (isTriggered && enemiesBasket.Count == 0)
        {
            
            stageDoor.SetActive(false);
            
        }
    }



    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTriggered = true;
            Debug.Log("스폰 게이트 생성");
            foreach (Transform spawnPoint in spawngateSpawnPoints)
            {
                GameObject gateSpawn = Instantiate(spawngatePrefab, spawnPoint.position, Quaternion.identity);
                enemiesBasket.Add(gateSpawn);
                Spawn spawnScript = gateSpawn.GetComponent<Spawn>();

                if (spawnScript != null)
                {
                    spawnScript.SetUpSwitch(this);
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
    
    // public void AdjustWin(int amount)
    // {
    //     remainingRobot += amount;
        
    //     if (remainingRobot <= 0)
    //     {
    //         Debug.Log("문 열림");
    //         stage_1_Door.SetActive(false);
    //     }
    // }
}
// 적 스폰 시 숫자를 알아야 하는데 그럴때는 List라는 동적할당을 사용해야 한다.