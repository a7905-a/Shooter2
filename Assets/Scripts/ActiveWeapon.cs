using UnityEngine;
using TMPro;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] WeaponSO weaponSO;
    [SerializeField] TMP_Text ammoText;

    AimZoom aimZoom;
    Inputs input;
    Animator animator;
    Weapon currentWeapon;
    int currentAmmo;
    float timeSinceLastShot = 0f;


    public bool weaponReloading = false;

    void Awake()
    {
        input = GetComponent<Inputs>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        currentWeapon = GetComponentInChildren<Weapon>();
        aimZoom = GetComponentInChildren<AimZoom>();
        currentAmmo = weaponSO.MaxAmmo;
    }

    void Update()
    {

        if (input.reload)
        {
            input.reload = false;

            if (weaponReloading)
            {
                return;
            }

            aimZoom.RigWeight(0);
            aimZoom.AimCondition(false);
            animator.SetLayerWeight(1, 1);
            animator.SetTrigger("Reload");
            weaponReloading = true;

        }

        HandleShoot();
        ammoText.text = currentAmmo.ToString("D2") + "/" + weaponSO.MaxAmmo.ToString("D2");
        timeSinceLastShot += Time.deltaTime;
    }

    void HandleShoot()
    {
        if (input.zoom)
        {
            if (input.shoot)
            {
                if (timeSinceLastShot >= weaponSO.FireRate && currentAmmo > 0)
                {
                    currentWeapon.Shoot(weaponSO);
                    animator.SetBool("Shoot", true);
                    timeSinceLastShot = 0f;
                    currentAmmo--;

                }

                if (!weaponSO.IsAutomatic)
                {
                    input.shoot = false;
                }

            }
            else
            {
                animator.SetBool("Shoot", false);
            }

        }
    }


    public void Reload()
    {
        Debug.Log("Reload");
        aimZoom.RigWeight(1);
        weaponReloading = false;
        animator.SetLayerWeight(1, 0);
        currentAmmo = weaponSO.MaxAmmo;
    }

    public void SwitchWeapon(WeaponSO weaponSO)
    {
        Debug.Log(weaponSO.name);
    }
}
