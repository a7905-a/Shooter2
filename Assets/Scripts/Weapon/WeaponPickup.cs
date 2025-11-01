using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] WeaponSO weaponSO;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ActiveWeapon activeWeapon = other.GetComponent<ActiveWeapon>();
            activeWeapon.SwitchWeapon(weaponSO);
            Destroy(this.gameObject);
        }
    }
}
