using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ProjectTwo.Enemy
{
    public class Spawn : MonoBehaviour
    {
        public static event Action<GameObject> OnEnemySpawn;

        [SerializeField] private GameObject robotPrefab;

        //노드 하나당 스폰할 적의 수
        [SerializeField] private int spawnCount = 5;    

        private SpawnBase[] myAreaNodes;

        private void Awake()
        {
            myAreaNodes = GetComponentsInChildren<SpawnBase>();
        }

        private void OnEnable()
        {
            SpawnSwitch.OnRobotBattleStart += StartSpawnRobot;
        }

        private void OnDisable()
        {
            SpawnSwitch.OnRobotBattleStart -= StartSpawnRobot;
        }



        private void StartSpawnRobot()
        {
            if (myAreaNodes.Length == 0)
            {
                Debug.LogWarning("스폰 영역 노드가 없습니다");
                return;
            }
            foreach (SpawnBase node in myAreaNodes)
            {
                for(int i = 0; i < spawnCount; i++)
                {
                    Vector3 randomOffset = Random.insideUnitSphere * node.spawnRadius;
                    randomOffset.y = 0; 
                    Vector3 spawnPoints = node.transform.position + randomOffset;

                    GameObject enemyRobot = Instantiate(robotPrefab, spawnPoints, node.transform.rotation);
                    OnEnemySpawn?.Invoke(enemyRobot);
                }
            }
        }
    }
}
