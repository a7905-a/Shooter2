using UnityEngine;

public class DropTable : MonoBehaviour
{
    [SerializeField] GameObject dropItem;

    public void DropItem() 
    {
        if (dropItem != null)
        {
            Instantiate(dropItem, transform.position, transform.rotation);
        }
    }
}
