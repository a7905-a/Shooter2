using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectTwo.InventoryManagement
{
    public class DragSlot : MonoBehaviour
    {
        public static DragSlot Instance;

        [SerializeField] private Image iconImage;
        [SerializeField] private TextMeshProUGUI amountText;
        
        // 어디 슬롯에서 드레그가 시작됬는지 기억
        public Slot draggedSlot;

        private void Awake()
        {
            Instance = this;
            HideSlot();

        }

        public void ShowSlot(ItemSO item, int amount)
        {
            iconImage.sprite = item.itemIcon;
            amountText.text = amount > 1 ? amount.ToString() : "";
            iconImage.enabled = true;
            amountText.enabled = true;
            gameObject.SetActive(true);
        }

        public void HideSlot()
        {
            draggedSlot = null;
            gameObject.SetActive(false);
        }

        public void UpdatePosition(Vector2 position)
        {
            transform.position = position;
        }
    }
}


