using UnityEngine.UI;
using UnityEngine;

public abstract class Interaction : MonoBehaviour
{
    [SerializeField] protected Image interactionIcon;

    protected bool isPlayerInRange = false;

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
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if (interactionIcon != null)
            {
                interactionIcon.gameObject.SetActive(true);
            }
            Debug.Log("상호작용 범위 진입");
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if (interactionIcon != null)
            {
                interactionIcon.gameObject.SetActive(false);
            }
            Debug.Log("상호작용 범위 이탈");
        }
    }
}
