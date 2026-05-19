using UnityEngine;
using ProjectTwo.Manager;

namespace ProjectTwo.Interactable
{
    public class StageDoor : MonoBehaviour
    {
        private void OnEnable()
        {
            BattleManager.OnStageCleared += DoorOpen;
        }

        private void OnDisable()
        {
            BattleManager.OnStageCleared -= DoorOpen;
        }
        private void DoorOpen()
        {
            Destroy(this.gameObject);
        }
    }
}
