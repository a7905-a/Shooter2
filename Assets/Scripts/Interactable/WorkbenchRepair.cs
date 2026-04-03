using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using ProjectTwo.Manager;
using ProjectTwo.InventoryManagement;

namespace ProjectTwo.Interactable
{
    public class WorkbenchRepair : Interaction
    {   
        public GameObject repairWokrbenchPrefab;
        public Recipe workBenchRecipe;

        public TextMeshProUGUI requirementText;
        protected override void Update()
        {
            base.Update();

            if (isPlayerInRange)
            {
                UpdateRequirementUI();
            }
        
        }

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);

            if (other.CompareTag("Player"))
            {
                UpdateRequirementUI();
            }
        }
        protected override void OnInteract()
        {
            if (CraftingManager.Instance.CanCraft(workBenchRecipe))
            {
                CraftingManager.Instance.ConsumeIngredients(workBenchRecipe);

                if (interactionIcon != null)
                {
                    interactionIcon.gameObject.SetActive(false);
                }

                if (repairWokrbenchPrefab != null)
                {
                    Instantiate(repairWokrbenchPrefab, transform.position, transform.rotation);             
                }
                
                Destroy(this.gameObject);
                
            }
        }

        public void UpdateRequirementUI()
        {
            if (workBenchRecipe == null || workBenchRecipe.ingredients.Count == 0) return;

            Ingredient reqItem = workBenchRecipe.ingredients[0]; // 레시피의 첫 번째 재료
            int currentAmount = Inventory.Instance.GetTotalItemCount(reqItem.item);
            
            if (requirementText != null)
            {
                // 글씨 갱신: "Wood 0 / 5"
                requirementText.text = $"{reqItem.item.itemName} {currentAmount} / {reqItem.amount}";
                
                // 재료가 충분하면 초록색, 부족하면 빨간색
                requirementText.color = currentAmount >= reqItem.amount ? Color.green : Color.red;
            }
        }


        
    }
}
