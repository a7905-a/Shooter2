using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Unity.VisualScripting;
using ProjectTwo.InventoryManagement;

public class Slot : MonoBehaviour, IItemSlot, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    //public bool hovering;

    private int itemAmount;
    private Transform originalParent;
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

        // 아이콘이 원래 있던 곳 기억
        // originalParent = iconImage.transform.parent;

        // // 아이콘이 다른 UI 요소보다 위에 있도록 설정
        // // root 사용이유
        // // 유니티 UI 랜더링은 하이어러키의 아래쪽에 있을수록 먼저 그려지므로, 아이콘이 다른 UI 요소보다 위에 있도록 하기 위해 root로 이동
        // iconImage.transform.SetParent(transform.root);
        // iconImage.transform.SetAsLastSibling();

        // // 아이콘이 도착 슬롯을 가리지 않도록 RaycastTarget 비활성화
        // iconImage.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // if (heldItem == null) return;

        // // 매 프레임 마다 마우스 좌표를 아이콘 위치에 적용
        // iconImage.transform.position = eventData.position;
        DragSlot.Instance.UpdatePosition(eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragSlot.Instance.HideSlot();

        Color color = iconImage.color;
        color.a = 1f;
        iconImage.color = color;
        // if (heldItem == null) return;

        // iconImage.transform.SetParent(originalParent, false);
        // iconImage.transform.SetAsFirstSibling();

        // RectTransform iconRect = iconImage.GetComponent<RectTransform>();

        // iconRect.anchoredPosition = Vector2.zero; 
        // iconRect.localPosition = Vector3.zero;

        // iconRect.sizeDelta = Vector2.zero; // Width, Height 초기화
        // iconRect.offsetMin = Vector2.zero; // Left, Bottom 초기화
        // iconRect.offsetMax = Vector2.zero;

        // // 다음 클릭을 정상적으로 인식하기 위해 RaycastTarget 다시 활성화
        // iconImage.raycastTarget = true;
    }

    public void OnDrop(PointerEventData eventData)
    {
        //eventData.pointerDrag는 드래그 중인 게임 오브젝트를 참조합니다. 이 경우에는 드래그한 슬롯의 오브젝트
        GameObject draggedObj = eventData.pointerDrag;

        if (draggedObj != null)
        {
            // 드래그한 슬롯에서 Slot 컴포넌트를 가져온다
            //Slot fromSlot = draggedObj.GetComponent<Slot>();
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
                    // 남은 공간과 가져온 수량 중 더 '작은' 값만큼만 이동
                    int move = Mathf.Min(space, draggedAmount);
                    
                    // 내 슬롯(도착지)은 수량을 더해주고
                    SetItem(currentItem, currentAmount + move);
                    
                    // 출발지 슬롯은 수량을 빼줌
                    fromSlot.SetItem(draggedItem, draggedAmount - move);

                    // 출발지 슬롯의 수량이 0 이하가 되면 깔끔하게 비움
                    if (fromSlot.GetAmount() <= 0)
                    {
                        fromSlot.ClearSlot();
                    }

                    // 합치기가 끝났으므로 아래의 '교환' 로직은 실행하지 않고 함수를 종료 (핵심!)
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

    // public void OnPointerEnter(PointerEventData eventData)
    // {
    //     hovering = true;
    // }

    // public void OnPointerExit(PointerEventData eventData)
    // {
    //     hovering = false;
    // }
}

