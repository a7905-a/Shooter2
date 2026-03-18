using Unity.VisualScripting;
using UnityEngine;

public class WorkbenchRepair : Interaction
{   
    public GameObject repairWokrbenchPrefab;

    public Recipe workBenchRecipe;

    protected override void OnInteract()
    {
        if (Inventory.Instance.CanCraft(workBenchRecipe))
        {
            Inventory.Instance.ConsumeIngredients(workBenchRecipe);

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

    
}
