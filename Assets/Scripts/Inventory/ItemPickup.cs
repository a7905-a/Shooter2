using ProjectTwo.Manager;
using UnityEngine;
using UnityEngine.UI;


namespace ProjectTwo.InventoryManagement
{
    public class ItemPickup : MonoBehaviour
    {
        [Header("아이템 획득 설정")]
        [SerializeField] private LayerMask itemLayerMask;
        [SerializeField] private float pickupRange = 1f;

        private Inputs input;
        private Item activeItem = null;

        private void Start()
        {
            input = FindFirstObjectByType<Inputs>();
        }

        private void Update()
        {
            HandleItemInteraction();

            // 허공에서 줍기 키를 입력했을 때 입력이 남아있는 것 방지
            if (input != null && input.interactItem)
            {
                input.ResetInteractItem();
            }
        }

        private void HandleItemInteraction()
        {
            // UI 위치가 아닌 플레이어 위치를 중심으로 아이템 구체 레이더를 돌림
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, pickupRange, itemLayerMask);

            Item closestItem = null;
            float minDistance = float.MaxValue;

            // 반경 안에 들어온 아이템 중 가장 가까운 오브젝트를 찾기
            foreach (Collider col in hitColliders)
            {
                Item item = col.GetComponentInParent<Item>(); 
                if (item != null)
                {
                    float distance = Vector3.Distance(transform.position, col.transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closestItem = item;
                    }
                }
            }

            // 주울 수 있는 아이템이 근처에 있다면?
            if (closestItem != activeItem)
            {
                if (activeItem != null)
                {
                    activeItem.ToggleInteractUI(false);
                }

                activeItem = closestItem;

                if (activeItem != null)
                {
                    activeItem.ToggleInteractUI(true);
                }
            }

            if (closestItem != null)
            {
                if (input.interactItem)
                {
                    Inventory.Instance.AddItem(closestItem.item, closestItem.amount);
                    
                    activeItem  = null;
                    Destroy(closestItem.gameObject);

                    input.ResetInteractItem();
                }
            }

        }
    }
}
