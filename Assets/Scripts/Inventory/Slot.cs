using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Unity.VisualScripting;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IItemSlot
{
    public bool hovering;

    ItemSO heldItem;
    int itemAmount;
    Image iconImage;
    TextMeshProUGUI amountText;

    void Awake()
    {
        iconImage = transform.GetChild(0).GetComponent<Image>();
        amountText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    public bool CanAcceptItem(ItemData itemData)
    {
        return true;
    }
    public void AddItem(ItemSO item, int amount)
    {
        SetItem(item, amount);
    }
    public void RemoveItem()
    {
        ClearSlot();
    }
    
    public ItemSO GetItem()
    {
        return heldItem;
    }

    public int GetAmount()
    {
        return itemAmount;
    }
    public void SetItem(ItemSO item, int amount = 1)
    {
        heldItem = item;
        itemAmount = amount;

        UpdateSlot();
    }

    public void UpdateSlot()
    {
        if (iconImage == null)
        {
            iconImage = transform.GetChild(0).GetComponent<Image>();
            amountText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        }


        if (heldItem != null)
        {
            iconImage.enabled = true;
            iconImage.sprite = heldItem.itemIcon;
            amountText.text = itemAmount.ToString();
        }
        else
        {
            iconImage.enabled = false;
            amountText.text = "";
        }
    }

    public void AddAmount(int amountToAdd)
    {
        itemAmount += amountToAdd;
        UpdateSlot();
        return;
    }

    public int RemoveAmount(int amountToRemove)
    {
        itemAmount -= amountToRemove;
        if (itemAmount <= 0)
        {
            ClearSlot();
        }
        else
        {
            UpdateSlot();
        }

        return itemAmount;
    }

    public void ClearSlot()
    {
        heldItem = null;
        itemAmount = 0;
        UpdateSlot();
    }

    public bool HasItem()
    {
        return heldItem != null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovering = false;
    }
}

