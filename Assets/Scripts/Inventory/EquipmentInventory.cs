using UnityEngine;

public class EquipmentInventory : MonoBehaviour, IItemSlot
{
    public bool CanAcceptItem(ItemData itemData)
    {
        return itemData.itemType == ItemType.Weapon;
    }

    public void AddItem(ItemSO item, int amount)
    {
    
    }
    public void RemoveItem()
    {
    
    }
}
