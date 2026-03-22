using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    public PlayerInventoryDataSO inventoryData;

    public Transform playerTransform;
    public LayerMask itemLayerMask;
    public ItemSO woodItem;
    public ItemSO axeItem;

    public GameObject hotbarObject;
    public GameObject inventorySlotParent;
    public GameObject container;

    public Image dragIcon;

    //아이템 줍기
    public float pickupRange = 30f;
    Item lookedAtitem = null;
    public Material highlightMaterial;
    Material originalMaterial;
    Renderer lookedAtRenderer = null;

    int equippedHotbarIndex = 0;
    public float equippedOpacity = 0.9f;
    public float normalOpacity = 0.58f;
    public Transform hand;
    GameObject currentHandItem;
    
    //아이템 설명 UI
    public GameObject itemDescriptionParent;
    public Image itemDescriptionImage;
    public TextMeshProUGUI descriptionItemNameText;
    public TextMeshProUGUI itemDescriptionText;

    //크래프팅
    public List<Recipe> allRecipes = new List<Recipe>();
    public Transform craftingGrid;
    public GameObject craftingBottonPrefab;
    public GameObject itemNeededUIPrefab;

    //인벤토리 슬롯 리스트
    List<Slot> inventorySlots = new List<Slot>();
    List<Slot> hotbarSlots = new List<Slot>();
    List<Slot> allSlots = new List<Slot>();

    Slot draggedSlot = null;
    bool isDragging = false; 

    void Awake()
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

        inventorySlots.AddRange(inventorySlotParent.GetComponentsInChildren<Slot>(true));
        hotbarSlots.AddRange(hotbarObject.GetComponentsInChildren<Slot>(true));

        allSlots.AddRange(inventorySlots);
        allSlots.AddRange(hotbarSlots);

        PopulateCraftingGrid();
    }

    void Start()
    {
        LoadInventory();
    }

    void Update()
    {
        
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 5f, Color.red);

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            container.SetActive(!container.activeInHierarchy);
            Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = !Cursor.visible;
            TPSCamera.Instance.updatingRotation = !TPSCamera.Instance.updatingRotation;
        }
        

        HandleItemInteraction();

        DetectLookedAtItem();
        Pickup();

        StartDrag();
        UpdateDragItemPosition();
        EndDrag();

        HandleHotbarSelection();
        HandleDropEquippedItem();
        UpdateHotbarOpacity();

        UpdateItemDescription();
    }
    //주변 아이템 회수
    void HandleItemInteraction()
    {
        // 플레이어 연결이 안 되어있으면 에러 방지
        if (playerTransform == null) return; 

        // 1. UI 위치가 아닌 '플레이어 위치'를 중심으로 아이템 레이더(구체)를 돌립니다!
        Collider[] hitColliders = Physics.OverlapSphere(playerTransform.position, pickupRange, itemLayerMask);

        Item closestItem = null;
        float minDistance = float.MaxValue;

        // 2. 반경 안에 들어온 아이템 중 가장 가까운 녀석을 찾습니다.
        foreach (Collider col in hitColliders)
        {
            Item item = col.GetComponentInParent<Item>(); 
            if (item != null)
            {
                float distance = Vector3.Distance(playerTransform.position, col.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestItem = item;
                }
            }
        }

        // 3. 주울 수 있는 아이템이 근처에 있다면?
        if (closestItem != null)
        {
            // 하이라이트 효과 적용 (자식 오브젝트의 렌더러까지 찾음)
            Renderer rend = closestItem.GetComponentInChildren<Renderer>();
            if (rend != null && rend != lookedAtRenderer)
            {
                if (lookedAtRenderer != null) lookedAtRenderer.material = originalMaterial;
                originalMaterial = rend.material;
                rend.material = highlightMaterial;
                lookedAtRenderer = rend;
            }

            // 💡 E키를 누르면 가장 가까운 아이템 줍기!
            if (Input.GetKeyDown(KeyCode.E))
            {
                AddItem(closestItem.item, closestItem.amount);
                Destroy(closestItem.gameObject);
                
                // 파괴 후 초기화
                lookedAtRenderer = null;
                originalMaterial = null;
            }
        }
        else // 4. 주변에 아이템이 아무것도 없다면?
        {
            // 멀어졌으니 원래 색으로 복구
            if (lookedAtRenderer != null)
            {
                lookedAtRenderer.material = originalMaterial;
                lookedAtRenderer = null;
                originalMaterial = null;
            }
        }
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
                        PopulateCraftingGrid();
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
                        PopulateCraftingGrid();
                        return;
                    }
            }
        }

        if (remaining > 0)
        {
            Debug.Log("Not enough space to add all items. " + remaining + " items were not added " + itemToAdd.itemName);
        }
        PopulateCraftingGrid();
    }


    void StartDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Slot hovered = GetHoveredSlot();

            if (hovered != null && hovered.HasItem())
            {
                draggedSlot = hovered;
                isDragging = true;

                //드래그한 아이템 이미지 보이게하기
                dragIcon.sprite = hovered.GetItem().itemIcon;
                dragIcon.color = new Color(1, 1, 1, 0.5f);
                dragIcon.enabled = true;
            }
        }
    }

    void EndDrag()
    {
        if(Input.GetMouseButtonUp(0) && isDragging)
        {
            Slot hovered = GetHoveredSlot();

            if (hovered != null)
            {
                HandleDrop(draggedSlot, hovered);

                dragIcon.enabled = false;
                draggedSlot = null;
                isDragging = false;
            }
        }
    }

    Slot GetHoveredSlot()
    {
        foreach (Slot s in allSlots)
        {
            if (s.hovering)
                return s;
        }

        return null;
    }

    
    void HandleDrop(Slot from, Slot to)
    {
        if (from == to) return;

        //아이템 이동 처리 중 같은 아이템일 경우
        if (to.HasItem() && to.GetItem() == from.GetItem())
        {
            int max = to.GetItem().maxStackSize;
            int space = max - to.GetAmount();

            if (space > 0)
            {
                int move = Mathf.Min(space, from.GetAmount());
                
                to.SetItem(to.GetItem(), to.GetAmount() + move);
                from.SetItem(from.GetItem(), from.GetAmount() - move);

                if (from.GetAmount() <= 0)
                {
                    from.ClearSlot();
                }
                return;
            }
        }

        //아이템 이동 처리 중 다른 아이템일 경우
        if (to.HasItem())
        {
            ItemSO tempItem = to.GetItem();
            int tempAmount = to.GetAmount();

            to.SetItem(from.GetItem(), from.GetAmount());
            from.SetItem(tempItem, tempAmount);
            return;
        }

        //아이템 이동 처리 중 빈 슬롯일 경우
        to.SetItem(from.GetItem(), from.GetAmount());
        from.ClearSlot();
    }

    void UpdateDragItemPosition()
    {
        if (isDragging)
        {
            dragIcon.transform.position = Input.mousePosition;
        }
    }

    void Pickup()
    {
        if(lookedAtRenderer != null && Input.GetKeyDown(KeyCode.E))
        {
            Item item = lookedAtRenderer.GetComponent<Item>();
            if (item != null)
            {
                AddItem(item.item, item.amount);
                Destroy(item.gameObject);
                EquipHandItem();


            }
        }
    }

    void DetectLookedAtItem()
    {
        if (lookedAtRenderer != null)
        {
            lookedAtRenderer.material = originalMaterial;
            lookedAtRenderer = null;
            originalMaterial = null;
        }

        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, pickupRange, itemLayerMask))
        {
            Item item = hit.collider.GetComponent<Item>();
            if (item != null)
            {
                Renderer rend = item.GetComponent<Renderer>();
                if (rend != null)
                {
                    originalMaterial = rend.material;
                    rend.material = highlightMaterial;
                    lookedAtRenderer = rend;
                }
            }
        }
    }

    void UpdateHotbarOpacity()
    {
        for (int i = 0; i < hotbarSlots.Count; i++)
        {
            Image icon = hotbarSlots[i].GetComponent<Image>();
            if (icon != null)
            {
                icon.color = (i == equippedHotbarIndex) ? new Color(1, 1, 1, equippedOpacity) : new Color(1, 1, 1, normalOpacity);
            }
        }
    }
    void HandleHotbarSelection()
    {
        for(int i = 0; i < 6; i++)
        {
            if(Input.GetKeyDown((i + 1).ToString()))
            {
                equippedHotbarIndex = i;
                UpdateHotbarOpacity();
                EquipHandItem();
            }
        }
    }

    void HandleDropEquippedItem()
    {
        if (!Input.GetKeyDown(KeyCode.Q)) return;

        Slot equippedSlot = hotbarSlots[equippedHotbarIndex];

        if (!equippedSlot.HasItem()) return;

        ItemSO itemSO = equippedSlot.GetItem();
        GameObject prefab = itemSO.itemPrefab;

        if (prefab == null) return;

        GameObject dropped = Instantiate(prefab, Camera.main.transform.position + Camera.main.transform.forward, Quaternion.identity);

        Item item = dropped.GetComponent<Item>();
        item.item = itemSO;
        item.amount = equippedSlot.GetAmount();

        equippedSlot.ClearSlot();

        EquipHandItem();
        PopulateCraftingGrid();
    }

    void EquipHandItem()
    {
        if (currentHandItem != null)
        {
            Destroy(currentHandItem);
        }

        Slot equippedSlot = hotbarSlots[equippedHotbarIndex];
        if (!equippedSlot.HasItem()) return;

        ItemSO item = equippedSlot.GetItem();  
        if (item.handItemPrefab == null) return;

        currentHandItem = Instantiate(item.handItemPrefab, hand);
        currentHandItem.transform.localPosition = Vector3.zero;
        currentHandItem.transform.localRotation = Quaternion.identity;

    }

    void UpdateItemDescription()
    {
        Slot hoveredSlot = GetHoveredSlot();

        if (hoveredSlot != null)
        {
            ItemSO hoveredItem = hoveredSlot.GetItem();

            if(hoveredItem != null)
            {
                itemDescriptionParent.SetActive(true);
                itemDescriptionImage.sprite = hoveredItem.itemIcon;
                itemDescriptionText.text = hoveredItem.description;
                descriptionItemNameText.text = hoveredItem.itemName;
                return; 
            }
        }
        itemDescriptionParent.SetActive(false);
    }

    void PopulateCraftingGrid()
    {
        for(int i = craftingGrid.childCount - 1; i >= 0; i--)
        {
            Destroy(craftingGrid.GetChild(i).gameObject);
        }

        foreach(Recipe recipe in allRecipes)
        {
            GameObject buttonObject = Instantiate(craftingBottonPrefab, craftingGrid);
            
            Image img = buttonObject.transform.GetChild(0).GetComponent<Image>();
            img.sprite = recipe.result.itemIcon;

            Button button = buttonObject.GetComponent<Button>();

            button.interactable = CanCraft(recipe);
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => Craft(recipe));

            foreach(Ingredient ingredient in recipe.ingredients)
            {
                GameObject neededItem = Instantiate(itemNeededUIPrefab, buttonObject.transform.GetChild(1));
                neededItem.GetComponent<Image>().sprite = ingredient.item.itemIcon;
                neededItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "x" + ingredient.amount.ToString();
            }
        }
    }

    public void Craft(Recipe recipe)
    {
        if(!CanCraft(recipe))
        {
            return;
        }

        ConsumeIngredients(recipe);
        AddItem(recipe.result, recipe.resultAmount);

        PopulateCraftingGrid();
    }

    public void ConsumeIngredients(Recipe recipe)
    {
        foreach(Ingredient ingredient in recipe.ingredients)
        {
            int remaining = ingredient.amount;

            foreach(Slot slot in allSlots)
            {
                if (!slot.HasItem()) continue;
                if (slot.GetItem() != ingredient.item) continue;

                int take = Mathf.Min(slot.GetAmount(), remaining);
                slot.SetItem(slot.GetItem(), slot.GetAmount() - take);

                if(slot.GetAmount() <= 0)
                {
                    slot.ClearSlot();
                }

                remaining -= take;
                if (remaining <= 0)
                {
                    break;
                }
            }
        }
    }

    public bool CanCraft(Recipe recipe)
    {
        foreach(Ingredient ingredient in recipe.ingredients)
        {
            int totalFound = 0;
            
            foreach(Slot slot in allSlots)
            {
                if(slot.HasItem() && slot.GetItem() == ingredient.item)
                {
                    totalFound += slot.GetAmount();
                }
            }

            if (totalFound < ingredient.amount)
            {
                return false;
            }
        }
        return true;
    }

    public int GetTotalItemCount(ItemSO targetItem)
    {
        int totalCount = 0; // 처음엔 0개부터 세기 시작합니다.

        // 내 가방(인벤토리 + 핫바)의 모든 슬롯을 하나씩 열어봅니다.
        foreach (Slot slot in allSlots)
        {
            // 만약 슬롯에 아이템이 들어있고 && 그 아이템 원본이 내가 찾는 목표물(targetItem)과 똑같다면?
            if (slot.HasItem() && slot.GetItem() == targetItem)
            {
                // 그 슬롯에 들어있는 개수를 총합(totalCount)에 더해줍니다!
                totalCount += slot.GetAmount();
            }
        }

        // 가방을 다 뒤졌으면 최종 개수를 사장님(작업대)에게 보고합니다!
        return totalCount;
    }
}
