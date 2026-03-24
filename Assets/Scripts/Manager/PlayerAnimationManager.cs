using UnityEngine;
using UnityEngine.TextCore;

public class PlayerAnimationManager : MonoBehaviour
{
    
    Animator animator;
    AimZoom aimZoom;
    Rigging rigging;
    
    int rifleBaseLayer = 0;
    int rifleActionLayer = 1;
    int baseLayer = 2;
    int pistolBaseLayer = 3;
    int pistolActionLayer = 4;
    readonly int hashReload = Animator.StringToHash("Reload");
    readonly int hashShooting = Animator.StringToHash("Shooting");
   void Awake()
    {
        animator = GetComponent<Animator>();
        aimZoom = GetComponentInChildren<AimZoom>();
        rigging = GetComponent<Rigging>();
    }

    void OnEnable()
    {
        ActiveWeapon.OnWeaponSwitched += HandleWeaponSwitched;
        ActiveWeapon.OnWeaponReload += ReloadAction;
        ActiveWeapon.OnWeaponRoloadFinished += ReloadFinished;
        ActiveWeapon.OnWeaponShoot += ShootAction;
    }

    void OnDisable()
    {
        ActiveWeapon.OnWeaponSwitched -= HandleWeaponSwitched;
        ActiveWeapon.OnWeaponReload -= ReloadAction;
        ActiveWeapon.OnWeaponRoloadFinished -= ReloadFinished;
        ActiveWeapon.OnWeaponShoot -= ShootAction;
    }
    void HandleWeaponSwitched(WeaponSO weaponSO, WeaponIKTarget targetData)
    {
        // 1. 리깅 세팅
        if (targetData != null)
        {
            rigging.SetWeaponIKTargets(targetData);
        }

        // 2. 애니메이터 레이어 세팅 (기존 레이어 끄고 맞는 무기 켜기)
        ResetAllWeaponLayers();

        if (weaponSO.WeaponType == WeaponType.Rifle)
        {
            animator.SetLayerWeight(rifleBaseLayer, 1f);
        }
        else if (weaponSO.WeaponType == WeaponType.Pistol)
        {
            animator.SetLayerWeight(pistolBaseLayer, 1f);
        }
    }

    void ReloadAction()
    {
        aimZoom.RigWeight(0f);
        aimZoom.AimCondition(false);
        animator.SetLayerWeight(rifleActionLayer, 1f);
        animator.SetTrigger(hashReload);
    }

    void ReloadFinished()
    {
        aimZoom.RigWeight(1f);
        animator.SetLayerWeight(rifleActionLayer, 0f);
    }

    void ShootAction()
    {
        animator.SetTrigger(hashShooting);
    }
    void ResetAllWeaponLayers()
    {
        animator.SetLayerWeight(baseLayer, 0f);
        animator.SetLayerWeight(rifleBaseLayer, 0f);
        animator.SetLayerWeight(pistolBaseLayer, 0f);
    }
}
