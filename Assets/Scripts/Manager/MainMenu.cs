using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectTwo.Manager
{
    public class MainMenu : MonoBehaviour
    {
        public void GoToScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}


