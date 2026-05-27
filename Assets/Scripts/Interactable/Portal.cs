using UnityEngine;
using UnityEngine.SceneManagement;
using ProjectTwo.Player;
using ProjectTwo.InventoryManagement;

namespace ProjectTwo.Interactable
{
    public class Portal : Interaction
    {
        [Header("포탈 설정")]
        public string nextSceneName;
        // 도착할 스폰 포인트의 고유 번호
        public PortalID destinationPortalID;

        [Header("씬 이동 데이터 연결")]
        public PlayerMovementDataSO playerMovementData;

        protected override void OnInteract()
        {
            TeleportToNextScene();
        }

        private void TeleportToNextScene()
        {
            Debug.Log("포탈 이동");
            if (Inventory.Instance != null)
            {
                Inventory.Instance.SaveInventory();
            }

            

            if (playerMovementData != null)
            {
                playerMovementData.targetPortalID = destinationPortalID;
            }

            SceneManager.LoadScene(nextSceneName);
        }
    }
}
