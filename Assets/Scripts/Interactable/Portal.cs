using UnityEngine;
using UnityEngine.SceneManagement;
using ProjectTwo.InventoryManagement;

namespace ProjectTwo.Interactable
{
    public class Portal : Interaction
    {
        [Header("포탈 설정")]
        public string nextSceneName;

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

            SceneManager.LoadScene(nextSceneName);
        }
    }
}
