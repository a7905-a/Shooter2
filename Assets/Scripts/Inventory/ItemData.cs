public enum ItemType
{
    Weapon,
    Material,
    Consumable,
}

[System.Serializable]
public class ItemData
{
    public string itemName;
    public ItemType itemType;
}
