using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ProjectTwo.InventoryManagement;

namespace ProjectTwo.Manager
{
    public class CraftingManager : MonoBehaviour
    {
        public static CraftingManager Instance;

        //[SerializeField] List<Recipe> allRecipes = new List<Recipe>();

        // [SerializeField] Transform craftingGrid;
        // [SerializeField] GameObject craftingBottonPrefab;
        // [SerializeField] GameObject itemNeededUIPrefab;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            } 
            else
            {
                Destroy(gameObject);
            } 
        }
        void Start()
        {
            //PopulateCraftingGrid();
        }

        // public void PopulateCraftingGrid()
        // {
        //     //기존 UI 지우기
        //     for(int i = craftingGrid.childCount - 1; i >= 0; i--)
        //     {
        //         Destroy(craftingGrid.GetChild(i).gameObject);
        //     }

        //     //레시피 버튼 생성
        //     foreach(Recipe recipe in allRecipes)
        //     {
        //         GameObject buttonObject = Instantiate(craftingBottonPrefab, craftingGrid);
                
        //         Image img = buttonObject.transform.GetChild(0).GetComponent<Image>();
        //         img.sprite = recipe.result.itemIcon;

        //         Button button = buttonObject.GetComponent<Button>();
        //         button.interactable = CanCraft(recipe);
        //         button.onClick.RemoveAllListeners();
        //         button.onClick.AddListener(() => Craft(recipe));

        //         //필요 재료 UI 생성
        //         foreach(Ingredient ingredient in recipe.ingredients)
        //         {
        //             GameObject neededItem = Instantiate(itemNeededUIPrefab, buttonObject.transform.GetChild(1));
        //             neededItem.GetComponent<Image>().sprite = ingredient.item.itemIcon;
        //             neededItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "x" + ingredient.amount.ToString();
        //         }
        //     }
        // }

        public void Craft(Recipe recipe)
        {
            if(!CanCraft(recipe)) return;

            //재료 소모
            ConsumeIngredients(recipe);
            //완성품 인벤토리에 추가
            Inventory.Instance.AddItem(recipe.result, recipe.resultAmount);

            //UI 갱신
            //PopulateCraftingGrid();
        }
        public void ConsumeIngredients(Recipe recipe)
        {
            foreach(Ingredient ingredient in recipe.ingredients)
            {
                Inventory.Instance.RemoveItem(ingredient.item, ingredient.amount);
            }
        }

        public bool CanCraft(Recipe recipe)
        {
            foreach(Ingredient ingredient in recipe.ingredients)
            { 
                int totalFound = Inventory.Instance.GetTotalItemCount(ingredient.item);

                if (totalFound < ingredient.amount)
                {
                    return false;
                }
            }
            return true;
        }
    }
}

