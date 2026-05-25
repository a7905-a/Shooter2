using UnityEngine;

namespace ProjectTwo.InventoryManagement
{
    public class Item : MonoBehaviour
    {
        public ItemSO item;
        public int amount = 1;

        [Header("상호작용 UI")]
        [SerializeField] private GameObject interactItemUI;

        private void Start()
        {
            if (interactItemUI != null)
            {
                interactItemUI.SetActive(false);
            }
        }

        public void ToggleInteractUI(bool isActive)
        {
            if (interactItemUI != null)
            {
                interactItemUI.SetActive(isActive);
            }
        }
    }
}
