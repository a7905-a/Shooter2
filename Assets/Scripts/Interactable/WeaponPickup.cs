using UnityEngine;
using ProjectTwo.Weapon;

namespace ProjectTwo.Interactable
{
    public class WeaponPickup : Interaction
    {
        [SerializeField] private WeaponSO weaponSO;

        protected override void OnInteract()
        {
            if (playerObject != null)
            {
                ActiveWeapon activeWeapon = playerObject.GetComponent<ActiveWeapon>();
                
                activeWeapon.SwitchWeapon(weaponSO);
                Destroy(this.gameObject);
            }
        }

        
    }
}
