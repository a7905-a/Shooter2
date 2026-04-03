using UnityEngine.UI;
using UnityEngine;

namespace ProjectTwo.Interactable
{
    public abstract class Interaction : MonoBehaviour
    {
        [SerializeField] protected Image interactionIcon;
        const string PLAYER_STRING = "Player";
        protected bool isPlayerInRange = false;
        protected GameObject playerObject;
        protected abstract void OnInteract();

        protected virtual void Update()
        {
            if (isPlayerInRange && Input.GetKeyDown(KeyCode.T))
            {
                OnInteract();
            }
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(PLAYER_STRING))
            {
                isPlayerInRange = true;
                //진입 시 플레이어 정보 저장
                playerObject = other.gameObject;

                if (interactionIcon != null)
                {
                    interactionIcon.gameObject.SetActive(true);
                }
                Debug.Log("상호작용 범위 진입");
            }
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(PLAYER_STRING))
            {
                isPlayerInRange = false;

                playerObject = null;

                if (interactionIcon != null)
                {
                    interactionIcon.gameObject.SetActive(false);
                }
                Debug.Log("상호작용 범위 이탈");
            }
        }
    }
}
