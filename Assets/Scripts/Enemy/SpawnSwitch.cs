using System;
using UnityEngine;

public class SpawnSwitch : MonoBehaviour
{
    public static event Action OnRobotBattleStart;
    
    bool isTriggered = false;
    const string PLAYER_TAG = "Player";

    void OnTriggerEnter(Collider other)
    {
        if ( !isTriggered && other.CompareTag(PLAYER_TAG))
        {
            isTriggered = true;
            OnRobotBattleStart?.Invoke();
        }
    }
}
