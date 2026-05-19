using UnityEngine;
using UnityEngine.SceneManagement;
using ProjectTwo.InventoryManagement;

namespace ProjectTwo.Interactable
{
    public class Portal : MonoBehaviour
    {
        public string nextSceneName;
        private bool isPlayerInRange = false;

        private void Update()
        {
            if (isPlayerInRange && Input.GetKeyDown(KeyCode.T))
            {        
                TeleportToNextScene();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                isPlayerInRange = true;
                Debug.Log("포탈 근처 범위 진입");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                isPlayerInRange = false;
            }
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
