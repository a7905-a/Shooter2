using UnityEngine;
using System;
using ProjectTwo.Manager;

namespace ProjectTwo.Weapon
{
    public class ActiveWeapon : MonoBehaviour
    {
        public static event Action<int, int> OnAmmoChanged;
        public static event Action<WeaponSO, WeaponIKTarget> OnWeaponSwitched;
        public static event Action OnWeaponShoot;
        public static event Action OnWeaponReload;
        public static event Action OnWeaponReloadFinished;
        public bool weaponReloading = false;

        WeaponSO weaponSO;

        [SerializeField] Transform weaponHoldPoint;
        
        Inputs input;
        Weapon currentWeapon;
        
        int currentAmmo;
        float timeSinceLastShot = 0f;
        

        void Awake()
        {
            input = GetComponent<Inputs>();

            currentWeapon = GetComponentInChildren<Weapon>();

            if (weaponSO != null)
            {
                currentAmmo = weaponSO.MaxAmmo;       
            }
        }

        void Start()
        {
            if (weaponSO == null) return;
            OnAmmoChanged?.Invoke(currentAmmo, weaponSO.MaxAmmo);
        }


        void Update()
        {   
            Debug.Log(weaponSO);
            
            if(weaponSO == null) return;

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
            OnWeaponReloadFinished?.Invoke();
        }
        
        
        void ReloadingAction()
        {
            weaponReloading = true;
            OnWeaponReload?.Invoke();
        }

        public bool HasWeapon()
        {
            return weaponSO != null;
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
                currentWeapon.gameObject.SetActive(false);

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
        }
    }
}
