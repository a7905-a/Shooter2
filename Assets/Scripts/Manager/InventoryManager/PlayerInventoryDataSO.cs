using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public struct SavedSlot
    {
        public ItemSO item;
        public int amount;
    }

    [CreateAssetMenu(fileName = "NewInventoryData", menuName = "Inventory/InventoryData")]

    public class PlayerInventoryDataSO : ScriptableObject
    {
        public List<SavedSlot> savedSlots = new List<SavedSlot>();
        public void ClearData()
        {
            savedSlots.Clear();
        }
    }

