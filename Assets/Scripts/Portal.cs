using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string nextSceneName;
    bool isPlayerInRange = false;

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.T))
        {        
            TeleportToNextScene();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            Debug.Log("포탈 근처 범위 진입");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    void TeleportToNextScene()
    {
        Debug.Log("포탈 이동");
        if (Inventory.Instance != null)
        {
            Inventory.Instance.SaveInventory();
        }

        SceneManager.LoadScene(nextSceneName);
    }
}
