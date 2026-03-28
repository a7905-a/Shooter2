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

    WeaponType currentWeaponType;

    void Awake()
    {
        animator = GetComponent<Animator>();
        aimZoom = GetComponentInChildren<AimZoom>();
        rigging = GetComponent<Rigging>();
    }

    void OnEnable()
    {
        ActiveWeapon.OnWeaponSwitched += HandleWeaponSwitched;
        ActiveWeapon.OnWeaponReload += ReloadStartAction;
        ActiveWeapon.OnWeaponReloadFinished += ReloadFinishedAction;
        ActiveWeapon.OnWeaponShoot += ShootAction;
        AimZoom.OnWeaponZoom += ZoomAction;
    }

    void OnDisable()
    {
        ActiveWeapon.OnWeaponSwitched -= HandleWeaponSwitched;
        ActiveWeapon.OnWeaponReload -= ReloadStartAction;
        ActiveWeapon.OnWeaponReloadFinished -= ReloadFinishedAction;
        ActiveWeapon.OnWeaponShoot -= ShootAction;
        AimZoom.OnWeaponZoom -= ZoomAction;
    }
    void HandleWeaponSwitched(WeaponSO weaponSO, WeaponIKTarget targetData)
    {
        currentWeaponType = weaponSO.WeaponType;
        //리깅 세팅
        if (targetData != null)
        {
            rigging.SetWeaponIKTargets(targetData);
        }

        //애니메이터 레이어 세팅 (기존 레이어 끄고 맞는 무기 켜기)
        ResetAllWeaponLayers();

        switch (currentWeaponType)
        {
            case WeaponType.Rifle :
            animator.SetLayerWeight(rifleBaseLayer, 1f);
            break;

            case WeaponType.Pistol :
            animator.SetLayerWeight(pistolBaseLayer, 1f);
            break;

            default :
            Debug.Log("처리되지 않은 무기 타입");
            break;
        }

    }

    void ReloadStartAction()
    {
        aimZoom.RigWeight(0f);
        aimZoom.AimCondition(false);
        animator.SetTrigger(hashReload);

        switch (currentWeaponType)
        {
            case WeaponType.Rifle :
            animator.SetLayerWeight(rifleActionLayer, 1f);
            break;

            case WeaponType.Pistol :
            animator.SetLayerWeight(pistolActionLayer, 1f);
            break;

            default :
            Debug.Log("처리되지 않은 무기 타입");
            break;
        }
    }

    void ReloadFinishedAction()
    {
        aimZoom.RigWeight(1f);
        switch (currentWeaponType)
        {
            case WeaponType.Rifle :
            animator.SetLayerWeight(rifleActionLayer, 0f);
            break;

            case WeaponType.Pistol :
            animator.SetLayerWeight(pistolActionLayer, 0f);
            break;

            default :
            Debug.Log("처리되지 않은 무기 타입");
            break;
        }
    }

    void ShootAction()
    {
        animator.SetTrigger(hashShooting);
    }

    void ZoomAction(bool isZooming)
    {
        float weight = isZooming ? 1f : 0;

        switch (currentWeaponType)
        {
            case WeaponType.Rifle :
            animator.SetLayerWeight(rifleActionLayer, weight);
            break;

            case WeaponType.Pistol :
            animator.SetLayerWeight(pistolActionLayer, weight);
            break;

            default :
            Debug.Log("처리되지 않은 무기 타입");
            break;
        }
    }

    void ResetAllWeaponLayers()
    {
        animator.SetLayerWeight(baseLayer, 0f);
        animator.SetLayerWeight(rifleBaseLayer, 0f);
        animator.SetLayerWeight(pistolBaseLayer, 0f);
    }
}
