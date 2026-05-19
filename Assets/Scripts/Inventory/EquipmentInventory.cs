using UnityEngine;
using ProjectTwo.InventoryManagement; // (네임스페이스 확인 후 맞게 수정하세요)
using ProjectTwo.Weapon; // (네임스페이스 확인 후 맞게 수정하세요)

// 1. MonoBehaviour 대신 Slot을 상속받습니다! (나는 슬롯의 일종이다)
public class EquipmentSlot : Slot 
{
    [SerializeField] private ActiveWeapon playerWeapon;
    // 2. [오버라이딩] 이 슬롯은 '무기' 타입만 받을 수 있도록 입구 컷을 설정합니다.
    public override bool CanAcceptItem(ItemData itemData)
    {
        return itemData.itemType == ItemType.Weapon;
    }

    // 3. [오버라이딩] 아이템이 슬롯에 들어올 때 작동하는 로직
    public override void SetItem(ItemSO item, int amount = 1)
    {
        // base.SetItem은 부모(Slot)의 원래 기능(아이콘 띄우기, 수량 저장 등)을 
        // 똑같이 실행해 달라는 명령어입니다.
        base.SetItem(item, amount);
        if (item == null)
        {
            Debug.Log("실패 원인: item이 null(비어있음)로 들어왔습니다.");
        }
        else
        {
            Debug.Log($"✅ item은 들어왔습니다. 이름: {item.itemName}");
            Debug.Log($"🔍 item의 실제 클래스 타입: {item.GetType()}");
        
            if (item is WeaponSO)
            {
                Debug.Log("⭕ 이 아이템은 WeaponSO가 맞습니다!");
            }
            else
            {
                Debug.Log("❌ 실패 원인: 이 아이템은 WeaponSO가 아닙니다!");
                Debug.Log($"🔍 item의 실제 클래스 타입: {item.GetType()}");
            }
        }
        // --- 부모 기능이 끝난 후, 나만의 추가 기능(무기 장착) 실행 ---
        if (item != null && item is WeaponSO weaponData)
        {
            Debug.Log("2");
            
            // 플레이어의 ActiveWeapon을 찾아 장착!
            // (FindObjectOfType은 테스트용이며, 나중에 GameManager.Instance.Player로 바꾸면 완벽합니다)
            
            if (playerWeapon != null)
            {
                playerWeapon.SwitchWeapon(weaponData);
                Debug.Log($"{weaponData.itemName}을(를) 플레이어 손에 장착했습니다!");
            }
        }
    }

    // 4. [오버라이딩] 아이템이 슬롯에서 빠져나갈 때 작동하는 로직 (선택 사항)
    public override void ClearSlot()
    {
        base.ClearSlot();

        // 무기를 다른 곳으로 뺐으니, 플레이어 손에서도 무기를 해제해야 합니다.
        ActiveWeapon playerWeapon = FindObjectOfType<ActiveWeapon>();
        if (playerWeapon != null)
        {
            // playerWeapon.UnEquipWeapon(); // (ActiveWeapon 쪽에 무기 해제 함수가 있다면 호출)
            Debug.Log("장비창이 비어서 무기를 해제합니다.");
        }
    }
}