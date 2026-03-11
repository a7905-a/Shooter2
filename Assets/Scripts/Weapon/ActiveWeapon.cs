using UnityEngine;
using System;

public class ActiveWeapon : MonoBehaviour
{
    public static event Action<int, int> OnAmmoChanged;
    [SerializeField] WeaponSO weaponSO;
    [SerializeField] WeaponSO rifleSO;

    //다른 총기 종류 추가 가능
    // 예시) [SerializeField] WeaponSO pistolSO;

    [SerializeField] Transform weaponHoldPoint;

    //문자열을 상수로 중앙 관리
    const string RELOAD_STRING = "Reload";
    const string SHOOT_STRING = "Shooting";

    AimZoom aimZoom;
    Inputs input;
    Animator animator;
    Weapon currentWeapon;
    Rigging rigging;
    int currentAmmo;
    float timeSinceLastShot = 0f;

    //매직 넘버 사용을 피하기 위해서 변수 이름을 선언
    int rifleBaseLayer = 0;
    int rifleActionLayer = 1;
    int baseLayer = 2;

    public bool weaponReloading = false;

    void Awake()
    {
        input = GetComponent<Inputs>();
        animator = GetComponent<Animator>();
        rigging = GetComponent<Rigging>();
        currentWeapon = GetComponentInChildren<Weapon>();
        aimZoom = GetComponentInChildren<AimZoom>();
        currentAmmo = weaponSO.MaxAmmo;       
    }

    void Start()
    {
        OnAmmoChanged?.Invoke(currentAmmo, weaponSO.MaxAmmo);
    }


    void Update()
    {
        LastShootTimer();
        ReloadInput();
        HandleShoot();
    }


    void LastShootTimer()
    {
        timeSinceLastShot += Time.deltaTime;
    }


    void ReloadInput()
    {
        if (input.reload)
        {
            input.ResetReload();

            //재장전중일 때 재장전이 한번 더 되지 않도록 하기
            if (!weaponReloading)
            {
                ReloadingAction();
            }
        }
    }

    
    void HandleShoot()
    {
        if (input.zoom && input.shoot)
        {
            if (timeSinceLastShot >= weaponSO.FireRate && currentAmmo > 0)
            {
                ProcessShoot();
            }
            //반자동 총기의 경우 발사 버튼을 누를 때마다 발사하도록 하기
            HandleSemiAuto();
        }
    }

    void HandleSemiAuto()
    {
        if (!weaponSO.IsAutomatic)
        {
            input.ResetShoot();
        }
    }

    void ProcessShoot()
    {
        currentWeapon.Shoot(weaponSO);
        animator.SetTrigger(SHOOT_STRING);
        timeSinceLastShot = 0f;
        currentAmmo--;
        OnAmmoChanged?.Invoke(currentAmmo, weaponSO.MaxAmmo);
    }

    public void Reload()
    {
        aimZoom.RigWeight(1f);
        weaponReloading = false;
        animator.SetLayerWeight(rifleActionLayer, 0f);
        currentAmmo = weaponSO.MaxAmmo;

        OnAmmoChanged?.Invoke(currentAmmo, weaponSO.MaxAmmo);
    }
    
    void ReloadingAction()
    {
        aimZoom.RigWeight(0f);
        aimZoom.AimCondition(false);
        animator.SetLayerWeight(rifleActionLayer, 1f);
        animator.SetTrigger(RELOAD_STRING);
        weaponReloading = true;
    }


    public void SwitchWeapon(WeaponSO newWeaponSO)
    {
        // So를 바꿔주는 코드
        // 1 현재 무기 파괴
        DestroyCurrentWeapon();
        // 2 새로운 무기 생성
        EquipNewWeapon(newWeaponSO);
        SetupWeaponIKAndAnimation();

        currentAmmo = weaponSO.MaxAmmo;
        OnAmmoChanged?.Invoke(currentAmmo, weaponSO.MaxAmmo);
    }

    

    void DestroyCurrentWeapon()
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }
    }
    void EquipNewWeapon(WeaponSO newWeaponSO)
    {
        weaponSO = newWeaponSO;
        //So 이름에 맞는 무기를 프리펩에서 찾아서 생성
        currentWeapon = Instantiate(weaponSO.WeaponPrefab, weaponHoldPoint).GetComponent<Weapon>();
    }
    void SetupWeaponIKAndAnimation()
    {
        if (weaponSO.WeaponType == WeaponType.Rifle)
        {
            rigging.SetWeaponIKTargets(currentWeapon.gameObject);
            animator.SetLayerWeight(baseLayer, 0f);
            animator.SetLayerWeight(rifleBaseLayer, 1f);
        }
    }
}
