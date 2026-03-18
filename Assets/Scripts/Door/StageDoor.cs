using UnityEngine;

public class StageDoor : MonoBehaviour
{
    void OnEnable()
    {
        BattleManager.OnStageCleared += DoorOpen;
    }

    void OnDisable()
    {
        BattleManager.OnStageCleared -= DoorOpen;
    }
    void DoorOpen()
    {
        Destroy(this.gameObject);
    }
}
