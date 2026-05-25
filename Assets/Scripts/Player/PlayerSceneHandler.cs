using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectTwo.Player
{
    public class PlayerSceneHandler : MonoBehaviour
    {
        private void Enable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void Disable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            GameObject spawnPoint = GameObject.FindGameObjectWithTag("Respawn");

            if (spawnPoint != null)
            {
                CharacterController cc = GetComponent<CharacterController>();
                if (cc != null) cc.enabled = false;

                transform.position = spawnPoint.transform.position;
                transform.rotation = spawnPoint.transform.rotation;

                if (cc != null) cc.enabled = true;
            }
        }
    }
}



