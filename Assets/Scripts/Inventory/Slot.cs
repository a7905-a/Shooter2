using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Unity.VisualScripting;
using ProjectTwo.InventoryManagement;

public class Slot : MonoBehaviour, IItemSlot, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private int itemAmount;
    private ItemSO heldItem;
    private Image iconImage;
    private TextMeshProUGUI amountText;

    private void Awake()
    {
        iconImage = transform.GetChild(0).GetComponent<Image>();
        amountText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    public virtual bool CanAcceptItem(ItemData itemData)
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
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (heldItem == null) return;

        DragSlot.Instance.draggedSlot = this;
        DragSlot.Instance.ShowSlot(heldItem, itemAmount);

        Color color = iconImage.color;
        color.a = 0.5f;
        iconImage.color = color;
    }

    public void OnDrag(PointerEventData eventData)
    {
        DragSlot.Instance.UpdatePosition(eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragSlot.Instance.HideSlot();

        Color color = iconImage.color;
        color.a = 1f;
        iconImage.color = color;
    }

    public void OnDrop(PointerEventData eventData)
    {
        //eventData.pointerDrag는 드래그 중인 게임 오브젝트를 참조. 이 경우에는 드래그한 슬롯의 오브젝트
        GameObject draggedObj = eventData.pointerDrag;

        if (draggedObj != null)
        {
            // 드래그한 슬롯에서 Slot 컴포넌트를 가져옴
            Slot fromSlot = DragSlot.Instance.draggedSlot;

            // 드래그한 슬롯이 존재하고, 현재 슬롯과 다를 때만 아이템 교환을 수행
            if (fromSlot != null && fromSlot != this && fromSlot.HasItem())
            {
                // 드래그한 슬롯에서 아이템과 수량 가져오기
                ItemSO draggedItem = fromSlot.GetItem();
                int draggedAmount = fromSlot.GetAmount();

                // 현재 슬롯의 아이템과 수량 가져오기
                ItemSO currentItem = GetItem();
                int currentAmount = GetAmount();
            if (HasItem() && currentItem == draggedItem)
            {
                int max = currentItem.maxStackSize;
                int space = max - currentAmount; // 내 슬롯의 남은 공간 계산

                if (space > 0)
                {
                    // 남은 공간과 가져온 수량 중 더 작은 값만큼만 이동
                    int move = Mathf.Min(space, draggedAmount);
                    
                    // 내 슬롯(도착지)은 수량을 더해주고
                    SetItem(currentItem, currentAmount + move);
                    
                    // 출발지 슬롯은 수량을 빼줌
                    fromSlot.SetItem(draggedItem, draggedAmount - move);

                    // 출발지 슬롯의 수량이 0 이하가 되면 비우기
                    if (fromSlot.GetAmount() <= 0)
                    {
                        fromSlot.ClearSlot();
                    }

                    return; 
                }
            }
                // 아이템 교환
                SetItem(draggedItem, draggedAmount);
                fromSlot.SetItem(currentItem, currentAmount);
            }
        }
    }

    public ItemSO GetItem()
    {
        return heldItem;
    }

    public int GetAmount()
    {
        return itemAmount;
    }
    public virtual void SetItem(ItemSO item, int amount = 1)
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

    public virtual void ClearSlot()
    {
        heldItem = null;
        itemAmount = 0;
        UpdateSlot();
    }

    public bool HasItem()
    {
        return heldItem != null;
    }

}

