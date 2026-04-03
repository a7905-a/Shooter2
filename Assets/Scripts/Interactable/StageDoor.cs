using UnityEngine;
using ProjectTwo.Manager;

namespace ProjectTwo.Interactable
{
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
}
