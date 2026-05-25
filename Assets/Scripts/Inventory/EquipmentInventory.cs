using UnityEngine;
using ProjectTwo.Weapon; 
using ProjectTwo.Manager; 

public class EquipmentSlot : Slot 
{
    public override bool CanAcceptItem(ItemData itemData)
    {
        return itemData.itemType == ItemType.Weapon;
    }

    // 아이템이 슬롯에 들어올 때 작동하는 로직
    public override void SetItem(ItemSO item, int amount = 1)
    {
        base.SetItem(item, amount);
        
        if (item != null && item is WeaponSO weaponData)
        {
            ActiveWeapon playerWeapon = GameManager.Instance.player.GetComponent<ActiveWeapon>();
            if (playerWeapon != null)
            {
                playerWeapon.SwitchWeapon(weaponData);
            }
        }
    }

    public override void ClearSlot()
    {
        base.ClearSlot();

        ActiveWeapon playerWeapon = GameManager.Instance.player.GetComponent<ActiveWeapon>();
        if (playerWeapon != null)
        {
            Debug.Log("장비창이 비어서 무기를 해제합니다.");
        }
    }
}