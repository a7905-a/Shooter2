using UnityEngine;

public class WorkbenchRepair : Interaction
{   
    public GameObject repairWokrbenchPrefab;
    protected override void OnInteract()
    {
        Debug.Log("작업대 수리를 시작합니다!");
        if (interactionIcon != null)
        {
            interactionIcon.gameObject.SetActive(false);
        }

        // foreach (Slot slot in Inventory.Instance.allslots)
        // {
        //     if (slot.GetItem.itemName == "Wood" && slot.GetItem.itemAmount >= 5)
        //     {
        //         if (repairWokrbenchPrefab != null)
        //         {
        //             Instantiate(repairWokrbenchPrefab, transform.position, transform.rotation);
        //         }
        //         slot.RemoveItem(5);
        //         Debug.Log("작업대 수리가 완료되었습니다!");
        //     }
        // }
        
            
        

        Destroy(gameObject);
    }

    
}
