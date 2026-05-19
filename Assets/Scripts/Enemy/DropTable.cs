using UnityEngine;

namespace ProjectTwo.Enemy
{

    public class DropTable : MonoBehaviour
    {
        [SerializeField] private GameObject dropItem;

        public void DropItem() 
        {
            if (dropItem != null)
            {
                Instantiate(dropItem, transform.position, transform.rotation);
            }
        }
    }
}
