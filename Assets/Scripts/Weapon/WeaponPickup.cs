using UnityEngine;

public class WeaponPickup : Interaction
{
    [SerializeField] WeaponSO weaponSO;

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
