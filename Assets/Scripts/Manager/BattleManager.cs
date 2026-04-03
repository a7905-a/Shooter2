using System;
using System.Collections.Generic;
using UnityEngine;
using ProjectTwo.Enemy;

namespace ProjectTwo.Manager
{
    public class BattleManager : MonoBehaviour
    {
        public static event Action OnStageCleared;

        List<GameObject> enemiesBasket = new List<GameObject>();
        
        bool isBattleActive = false;

        void OnEnable()
        {
            SpawnSwitch.OnRobotBattleStart += StartBattle;
            Spawn.OnEnemySpawn += ResisterEnemy;
            EnemyHealth.OnEnemyDied += RemoveEnemy;
        }

        void OnDisable()
        {
            SpawnSwitch.OnRobotBattleStart -= StartBattle;
            Spawn.OnEnemySpawn -= ResisterEnemy;
            EnemyHealth.OnEnemyDied -= RemoveEnemy;
        }

        void StartBattle()
        {
            isBattleActive = true;
        }

        void ResisterEnemy(GameObject enemyRobot)
        {
            enemiesBasket.Add(enemyRobot);
        }
        public void RemoveEnemy(GameObject enemyRobot)
        {
            if (enemiesBasket.Contains(enemyRobot))
            {
                enemiesBasket.Remove(enemyRobot);
                
                if (isBattleActive && enemiesBasket.Count == 0)
                {
                    OnStageCleared?.Invoke();
                    isBattleActive = false;
                }

            }

        }
    }
}
