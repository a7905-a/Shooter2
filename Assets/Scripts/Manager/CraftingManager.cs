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

        private void Awake()
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

        public void Craft(Recipe recipe)
        {
            if(!CanCraft(recipe))
            {
                Debug.Log("재료 부족");
                return;
            }
                
            //재료 소모
            ConsumeIngredients(recipe);
            //완성품 인벤토리에 추가
            Inventory.Instance.AddItem(recipe.result, recipe.resultAmount);

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

