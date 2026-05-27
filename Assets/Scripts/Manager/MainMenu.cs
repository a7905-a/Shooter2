using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectTwo.Manager
{
    public class MainMenu : MonoBehaviour
    {
        public PlayerInventoryDataSO playerInventoryData;
        public void GoToScene(string sceneName)
        {
            playerInventoryData.ClearData();
            SceneManager.LoadScene(sceneName);
        }
    }
}


