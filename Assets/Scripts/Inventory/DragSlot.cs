using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectTwo.InventoryManagement
{
    public class DragSlot : MonoBehaviour
    {
        private static DragSlot _instance;
        public static DragSlot Instance
        {
            get
            {
                // 만약 인스턴스가 비어있거나, 이전 씬에서 파괴되었다면?
                if (_instance == null)
                {
                    //  씬 전체를 뒤져서 꺼져있는(Inactive) DragSlot까지 찾아내기
                    _instance = FindFirstObjectByType<DragSlot>(FindObjectsInactive.Include);
                    
                    if (_instance == null)
                    {
                        Debug.LogError("씬에 DragSlot이 아예 존재하지 않는다");
                    }
                }
                return _instance;
            }
        }

        [SerializeField] private Image iconImage;
        [SerializeField] private TextMeshProUGUI amountText;
        
        // 어디 슬롯에서 드레그가 시작됬는지 기억
        public Slot draggedSlot;

        private void Awake()
        {
            _instance = this;
            
        }

        private void Start()
        {
            HideSlot();
        }

        public void ShowSlot(ItemSO item, int amount)
        {
            if (this == null || gameObject == null)
        {
            Debug.LogError("지금 파괴된 DragSlot을 참조하려고 한다");
            return;
        }
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


