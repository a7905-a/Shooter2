using TMPro;
using UnityEngine;

public class WeaponDisplay : MonoBehaviour
{
    [SerializeField] TMP_Text ammoText;

    void OnEnable()
    {
        ActiveWeapon.OnAmmoChanged += UpdateAmmoUI;
    }

    void OnDisable()
    {
        ActiveWeapon.OnAmmoChanged -= UpdateAmmoUI;
    }

    private void UpdateAmmoUI(int currentAmmo, int maxAmmo)
    {
        ammoText.text = currentAmmo.ToString("D2") + "/" + maxAmmo.ToString("D2");
    }
}
