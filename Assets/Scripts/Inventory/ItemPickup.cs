using UnityEngine;


namespace ProjectTwo.InventoryManagement
{
    public class ItemPickup : MonoBehaviour
    {
        [SerializeField] private LayerMask itemLayerMask;
        [SerializeField] private float pickupRange = 30f;
        [SerializeField] private Material highlightMaterial;
        private Renderer closestRenderer = null;
        private Material originalMaterial;

        private void Update()
        {
            HandleItemInteraction();
        }
        private void HandleItemInteraction()
        {
            // UI 위치가 아닌 플레이어 위치를 중심으로 아이템 구체 레이더를 돌림
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, pickupRange, itemLayerMask);

            Item closestItem = null;
            float minDistance = float.MaxValue;

            // 반경 안에 들어온 아이템 중 가장 가까운 녀석을 찾기
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
            if (closestItem != null)
            {
                // 하이라이트 효과 적용 (자식 오브젝트의 렌더러까지 찾음)
                Renderer rend = closestItem.GetComponentInChildren<Renderer>();
                if (rend != null && rend != closestRenderer)
                {
                    if (closestRenderer != null) closestRenderer.material = originalMaterial;
                    originalMaterial = rend.material;
                    rend.material = highlightMaterial;
                    closestRenderer = rend;
                }

                // E키를 누르면 가장 가까운 아이템 줍기!
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Inventory.Instance.AddItem(closestItem.item, closestItem.amount);
                    Destroy(closestItem.gameObject);
                    
                    // 파괴 후 초기화
                    closestRenderer = null;
                    originalMaterial = null;
                }
            }
            else // 주변에 아이템이 아무것도 없다면?
            {
                // 멀어졌으니 원래 색으로 복구
                if (closestRenderer != null)
                {
                    closestRenderer.material = originalMaterial;
                    closestRenderer = null;
                    originalMaterial = null;
                }
            }
        }
    }
}
