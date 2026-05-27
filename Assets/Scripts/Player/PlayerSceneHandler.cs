using UnityEngine;
using UnityEngine.SceneManagement;
using ProjectTwo.Interactable;

namespace ProjectTwo.Player
{
    public class PlayerSceneHandler : MonoBehaviour
    {
        [Header("씬 이동 데이터 연결")]
        public PlayerMovementDataSO playerMovementData;

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
            if (playerMovementData == null || playerMovementData.targetPortalID == PortalID.None)
            {
                return;
            }

            PlayerScenePoint[] scenePoints = FindObjectsByType<PlayerScenePoint>(FindObjectsSortMode.None);

            foreach (PlayerScenePoint point in scenePoints)
            {
                if (point.myPortalID == playerMovementData.targetPortalID)
                {
                    CharacterController cc = GetComponent<CharacterController>();
                    if (cc != null) cc.enabled = false;

                    transform.position = point.transform.position;
                    transform.rotation = point.transform.rotation;

                    if (cc != null) cc.enabled = true;
                    playerMovementData.targetPortalID = PortalID.None;
                    break;
                }
            }
        }
    }
}



