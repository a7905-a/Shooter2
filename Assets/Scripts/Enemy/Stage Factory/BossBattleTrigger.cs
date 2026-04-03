using System;
using UnityEngine;

namespace ProjectTwo.Enemy
{
    public class BossBattleTrigger : MonoBehaviour
    {
        public static event Action OnBossBattleStart;

        const string PLAYER_STRING = "Player";

        void OnTriggerEnter(Collider other) 
        {
            if (other.CompareTag(PLAYER_STRING))
            {
                OnBossBattleStart?.Invoke();
                Destroy(gameObject);
            }
        }

    }
}
