using System;
using System.Collections.Generic;
using UnityEngine;
using ProjectTwo.Enemy;

namespace ProjectTwo.Manager
{
    public class BattleManager : MonoBehaviour
    {
        public static event Action OnStageCleared;

        private List<GameObject> enemiesBasket = new List<GameObject>();
        
        private bool isBattleActive = false;

        private void OnEnable()
        {
            SpawnSwitch.OnRobotBattleStart += StartBattle;
            Spawn.OnEnemySpawn += ResisterEnemy;
            EnemyHealth.OnEnemyDied += RemoveEnemy;
        }

        private void OnDisable()
        {
            SpawnSwitch.OnRobotBattleStart -= StartBattle;
            Spawn.OnEnemySpawn -= ResisterEnemy;
            EnemyHealth.OnEnemyDied -= RemoveEnemy;
        }

        private void StartBattle()
        {
            isBattleActive = true;
        }

        private void ResisterEnemy(GameObject enemyRobot)
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
