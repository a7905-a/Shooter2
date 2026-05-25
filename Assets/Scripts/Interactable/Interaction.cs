using UnityEngine.UI;
using UnityEngine;
using ProjectTwo.Manager;

namespace ProjectTwo.Interactable
{
    public abstract class Interaction : MonoBehaviour
    {
        [Header("상호작용 UI")]
        [SerializeField] protected Image interactionIcon;
        const string PLAYER_STRING = "Player";
        protected bool isPlayerInRange = false;
        protected GameObject playerObject;

        protected Inputs input;
        protected abstract void OnInteract();

        protected virtual void Update()
        {
            if (isPlayerInRange && input.interactAction)
            {
                OnInteract();
                input.ResetInteractAction();
            }
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(PLAYER_STRING))
            {
                isPlayerInRange = true;
                //진입 시 플레이어 정보 저장
                playerObject = other.gameObject;
                input = FindFirstObjectByType<Inputs>();

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
