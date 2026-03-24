using UnityEngine;
using System;

public class ActiveWeapon : MonoBehaviour
{
    public static event Action<int, int> OnAmmoChanged;
    public static event Action<WeaponSO, WeaponIKTarget> OnWeaponSwitched;
    public static event Action OnWeaponShoot;
    public static event Action OnWeaponReload;
    public static event Action OnWeaponRoloadFinished;
    public bool weaponReloading = false;

    [SerializeField] WeaponSO weaponSO;
    //[SerializeField] WeaponSO rifleSO;

    //다른 총기 종류 추가 가능
    // 예시) [SerializeField] WeaponSO pistolSO;

    [SerializeField] Transform weaponHoldPoint;

    //문자열을 해싱
    

    AimZoom aimZoom;
    Inputs input;
    Animator animator;
    Weapon currentWeapon;
    
    int currentAmmo;
    float timeSinceLastShot = 0f;

    //매직 넘버 사용을 피하기 위해서 변수 이름을 선언
    

    

    void Awake()
    {
        input = GetComponent<Inputs>();
        //animator = GetComponent<Animator>();
        //
        currentWeapon = GetComponentInChildren<Weapon>();
        
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
        if (input.reload && !weaponReloading)
        {
            input.ResetReload();
            ReloadingAction();            
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
        timeSinceLastShot = 0f;
        currentAmmo--;
        OnAmmoChanged?.Invoke(currentAmmo, weaponSO.MaxAmmo);
        OnWeaponShoot?.Invoke(); 
    }
    public void Reload()
    {
        weaponReloading = false;
        currentAmmo = weaponSO.MaxAmmo;
        OnAmmoChanged?.Invoke(currentAmmo, weaponSO.MaxAmmo);
        OnWeaponRoloadFinished?.Invoke();
    }
    
    
    void ReloadingAction()
    {
        OnWeaponReload?.Invoke();
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
        WeaponIKTarget targetData = currentWeapon.GetComponent<WeaponIKTarget>();
        OnWeaponSwitched?.Invoke(weaponSO, targetData);

        // if (weaponSO.WeaponType == WeaponType.Rifle)
        // {
            

        //     if (targetData != null)
        //     {
        //         rigging.SetWeaponIKTargets(targetData);
        //     }
            
        //     animator.SetLayerWeight(baseLayer, 0f);
        //     animator.SetLayerWeight(rifleBaseLayer, 1f);
        // }
        // else if (weaponSO.WeaponType == WeaponType.Pistol)
        // {
        //     WeaponIKTarget targetData = currentWeapon.GetComponent<WeaponIKTarget>();

        //     if (targetData != null)
        //     {
        //         rigging.SetWeaponIKTargets(targetData);
        //     }
            
        //     animator.SetLayerWeight(baseLayer, 0f);
        //     animator.SetLayerWeight(pistolBaseLayer, 1f);
        // }
    }
}
