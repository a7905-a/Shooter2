using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using ProjectTwo.Manager;
using ProjectTwo.Player;

namespace ProjectTwo.InventoryManagement
{
    public class Inventory : MonoBehaviour
    {
        public static Inventory Instance;

        [Header("데이터")]
        public PlayerInventoryDataSO inventoryData;

        [Header("UI 패널")]
        [SerializeField] private GameObject container; // 인벤토리 전체 창
        [SerializeField] private GameObject inventorySlotParent; // 인벤토리 슬롯들이 있는 부모

        [Header("아이템 설명")]
        [SerializeField] private GameObject itemDescriptionParent;
        [SerializeField] private Image itemDescriptionImage;
        [SerializeField] private TextMeshProUGUI descriptionItemNameText;
        [SerializeField] private TextMeshProUGUI itemDescriptionText;

    
        [Header("슬롯 캐싱 리스트")]
        private List<Slot> inventorySlots = new List<Slot>();
        private List<Slot> allSlots = new List<Slot>();

        private Inputs input;

        // hotbar는 추후 사용할 떄 사용

        // [SerializeField] private GameObject hotbarObject; 

        // [Header("핫바 설정")]
        // private int equippedHotbarIndex = 0;
        // [SerializeField] private float equippedOpacity = 0.9f;
        // [SerializeField] private float normalOpacity = 0.58f;

        // [Header("핫바 슬롯 캐싱")]
        // private List<Slot> hotbarSlots = new List<Slot>();
        private void Awake()
        {   
            //싱글톤 패턴
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            // 부모 객체 아래에 있는 모든 Slot 컴포넌트를 찾아 inventorySlots 리스트에 한 번에 넣기
            // 플레이어가 아이템을 획득할 때마다 실행하면 비용이 높으니까 캐싱해서 사용
            inventorySlots.AddRange(inventorySlotParent.GetComponentsInChildren<Slot>(true));

            // 핫 바는 사용 계획있을 때 사용
            // hotbarSlots.AddRange(hotbarObject.GetComponentsInChildren<Slot>(true));
            // allSlots.AddRange(hotbarSlots);

            allSlots.AddRange(inventorySlots);
        }

        private void Start()
        {
            LoadInventory();
            input = FindFirstObjectByType<Inputs>();
        }

        private void Update()
        {
            if (input.toggleInventory)
            {
                ToggleInventory();
                input.ResetToggleInventory();
            }

            // 아이템 설명과 핫 바 구현은 추후 계획있을 때 사용
            // UpdateItemDescription();
            // UpdateHotbarOpacity();
        }

        private void ToggleInventory()
        {
            container.SetActive(!container.activeInHierarchy);
            Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = !Cursor.visible;
            TPSCamera.Instance.updatingRotation = !TPSCamera.Instance.updatingRotation;
        }

        //인벤토리 데이터를 저장하는 로직
        public void SaveInventory()
        {
            //인벤토리 데이터가 연결되어있지 않을 때 에러 방지
            if (inventoryData == null) return;

            //낡은 데이터 싹 지우기
            inventoryData.ClearData();

            //내 인벤토리의 모든 슬롯을 하나씩 확인
            foreach(Slot slot in allSlots)
            {
                if (slot.HasItem()) //아이템이 들어있는 슬롯이라면
                {
                    //저장할 데이터 구조체 하나 만들어서 아이템 정보와 개수를 넣고 인벤토리 데이터 리스트에 추가
                    SavedSlot newSavedSlot = new SavedSlot();
                    newSavedSlot.item = slot.GetItem();
                    newSavedSlot.amount = slot.GetAmount();
                    inventoryData.savedSlots.Add(newSavedSlot);
                }
            }
        }

        public void LoadInventory()
        {
            if (inventoryData == null) return;
            // 혹시 모르니 인벤토리에 쓰레기 데이터 비우기
            foreach(Slot slot in allSlots)
            {
                slot.ClearSlot();
            }

            foreach(SavedSlot savedData in inventoryData.savedSlots)
            {
                AddItem(savedData.item, savedData.amount);
            }
        }

        public void AddItem(ItemSO itemToAdd, int amount)
        {
            int remaining = amount;

            foreach(Slot slot in allSlots)
            {
                //이미 같은 아이템이 들어있는 슬롯이 있는지 확인
                if (slot.HasItem() && slot.GetItem() == itemToAdd)
                {
                    int currentAmount = slot.GetAmount();
                    int maxStack = itemToAdd.maxStackSize;

                    if (currentAmount < maxStack)
                    {
                        int spaceLeft = maxStack - currentAmount;
                        int amountToAdd = Mathf.Min(spaceLeft, remaining);

                        slot.SetItem(itemToAdd, currentAmount + amountToAdd);
                        remaining -= amountToAdd;

                        if (remaining <= 0)
                        {
                            return;
                        }
                        
                    }
                }
            }

            foreach(Slot slot in allSlots)
            {
                if (!slot.HasItem())
                {
                    int amountToPlace = Mathf.Min(itemToAdd.maxStackSize, remaining);
                    slot.SetItem(itemToAdd, amountToPlace);
                    remaining -= amountToPlace;

                    if (remaining <= 0)
                    {
                        return;
                    }
                }
            }

            if (remaining > 0)
            {
                Debug.Log("가방이 부족해서 " + itemToAdd.itemName + " " + remaining + "개 획득하지 못했습니다");
                // 가방이 꽉 차면 넘치는 아이템은 바닥에 드랍되게 하기
            }

        }
        public void RemoveItem(ItemSO itemToRemove, int amount)
        {
            int remaining = amount;

            foreach(Slot slot in allSlots)
            {
                if(!slot.HasItem()) continue;
                if(slot.GetItem() != itemToRemove) continue;

                int take = Mathf.Min(slot.GetAmount(), remaining);
                slot.SetItem(slot.GetItem(), slot.GetAmount() - take);

                if (slot.GetAmount() <= 0)
                {
                    slot.ClearSlot();
                }

                remaining -= take;
                if(remaining <= 0)
                {
                    break;
                }
            }
        }
        
        public int GetTotalItemCount(ItemSO targetItem)
        {
            int totalCount = 0;

            // 내 가방의 모든 슬롯을 하나씩 확인
            foreach (Slot slot in allSlots)
            {
                // 만약 슬롯에 아이템이 들어있고 && 그 아이템 원본이 내가 찾는 목표물(targetItem)과 똑같다면?
                if (slot.HasItem() && slot.GetItem() == targetItem)
                {
                    // 그 슬롯에 들어있는 개수를 총합에 더해줌
                    totalCount += slot.GetAmount();
                }
            }

            //최종 개수를 보고
            return totalCount;
        }
    }
}



// private void HandleDrop(Slot from, Slot to)
        // {
        //     if (from == to) return;

        //     //아이템 이동 처리 중 같은 아이템일 경우
        //     if (to.HasItem() && to.GetItem() == from.GetItem())
        //     {
        //         int max = to.GetItem().maxStackSize;
        //         int space = max - to.GetAmount();

        //         if (space > 0)
        //         {
        //             int move = Mathf.Min(space, from.GetAmount());
                    
        //             to.SetItem(to.GetItem(), to.GetAmount() + move);
        //             from.SetItem(from.GetItem(), from.GetAmount() - move);

        //             if (from.GetAmount() <= 0)
        //             {
        //                 from.ClearSlot();
        //             }
        //             return;
        //         }
        //     }

        //     //아이템 이동 처리 중 다른 아이템일 경우
        //     if (to.HasItem())
        //     {
        //         ItemSO tempItem = to.GetItem();
        //         int tempAmount = to.GetAmount();

        //         to.SetItem(from.GetItem(), from.GetAmount());
        //         from.SetItem(tempItem, tempAmount);
        //         return;
        //     }

        //     //아이템 이동 처리 중 빈 슬롯일 경우
        //     to.SetItem(from.GetItem(), from.GetAmount());
        //     from.ClearSlot();
        // }


        // private void UpdateHotbarOpacity()
        // {
        //     for (int i = 0; i < hotbarSlots.Count; i++)
        //     {
        //         Image icon = hotbarSlots[i].GetComponent<Image>();
        //         if (icon != null)
        //         {
        //             icon.color = (i == equippedHotbarIndex) ? new Color(1, 1, 1, equippedOpacity) : new Color(1, 1, 1, normalOpacity);
        //         }
        //     }
        // }

        // private void UpdateItemDescription()
        // {
        //     Slot hoveredSlot = GetHoveredSlot();

        //     if (hoveredSlot != null)
        //     {
        //         ItemSO hoveredItem = hoveredSlot.GetItem();

        //         if(hoveredItem != null)
        //         {
        //             itemDescriptionParent.SetActive(true);
        //             itemDescriptionImage.sprite = hoveredItem.itemIcon;
        //             itemDescriptionText.text = hoveredItem.description;
        //             descriptionItemNameText.text = hoveredItem.itemName;
        //             return; 
        //         }
        //     }
        //     itemDescriptionParent.SetActive(false);
        // }

