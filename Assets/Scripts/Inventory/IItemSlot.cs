public interface IItemSlot
{
    bool CanAcceptItem(ItemData itemData);
    
    void AddItem(ItemSO item, int amount);

    void RemoveItem();
}
