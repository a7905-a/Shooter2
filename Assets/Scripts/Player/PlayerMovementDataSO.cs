using UnityEngine;
using ProjectTwo.Interactable;

namespace ProjectTwo.Player
{
    [CreateAssetMenu(fileName = "NewMovementData", menuName = "Player/MovementData")]
    public class PlayerMovementDataSO : ScriptableObject
    {
        [Header("씬 이동 데이터")]
        public PortalID targetPortalID = PortalID.None;

        public void ClearData()
        {
            targetPortalID = PortalID.None;
        }
    }
}

