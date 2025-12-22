using UnityEngine;
using TMPro;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] WeaponSO weaponSO;
    [SerializeField] WeaponSO rifleSO;

    //다른 총기 종류 추가 가능
    // 예시) [SerializeField] WeaponSO pistolSO;

    [SerializeField] TMP_Text ammoText;
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

    //매직 넘버 사용을 피하기 위한 변수 이름 선언
    int rifleBaseLayer = 0;
    int rifleActionLayer = 1;
    int baseLayer = 2;

    public bool weaponReloading = false;

    void Awake()
    {
        input = GetComponent<Inputs>();
        animator = GetComponent<Animator>();
        rigging = GetComponent<Rigging>();
        if (input == null || animator == null || rigging == null)
        {
            Debug.LogError("필수 컴포넌트가 누락!");
            enabled = false;
            return;
        }
        
        currentWeapon = GetComponentInChildren<Weapon>();
        aimZoom = GetComponentInChildren<AimZoom>();
        currentAmmo = weaponSO.MaxAmmo;       
    }


    void Update()
    {
        if (input.reload)
        {
            input.ResetReload();

            //재장전중일 때 재장전이 한번 더 되지 않도록 return으로 방지
            if (weaponReloading)
            {
                return;
            }
            ReloadingAction();
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
                    animator.SetTrigger(SHOOT_STRING); 
                    timeSinceLastShot = 0f;
                    currentAmmo--;

                }

                if (!weaponSO.IsAutomatic)
                {
                    input.ResetShoot();
                }

            }
        }
    }


    public void Reload()
    {
        aimZoom.RigWeight(1f);
        weaponReloading = false;
        animator.SetLayerWeight(rifleActionLayer, 0f);
        currentAmmo = weaponSO.MaxAmmo;
    }
    void ReloadingAction()
    {
        aimZoom.RigWeight(0f);
        aimZoom.AimCondition(false);
        animator.SetLayerWeight(rifleActionLayer, 1f);
        animator.SetTrigger(RELOAD_STRING);
        weaponReloading = true;
    }


    public void SwitchWeapon(WeaponSO weaponSO)
    {
        // So를 바꿔주는 코드
        // 1 현재 무기 파괴
        if (currentWeapon)
        {
            Destroy(currentWeapon.gameObject);
        }
        // 2 새로운 무기 생성
        Weapon newWeapon = Instantiate(weaponSO.WeaponPrefab, weaponHoldPoint).GetComponent<Weapon>();
        
        currentWeapon = newWeapon;
        this.weaponSO = weaponSO;
        
        //So 이름에 맞는 무기를 프리펩에서 찾아서 생성
        
        if (weaponSO.WeaponType == WeaponType.Rifle)
        {
            rigging.SetWeaponIKTargets(currentWeapon.gameObject);
            animator.SetLayerWeight(baseLayer, 0f);
            animator.SetLayerWeight(rifleBaseLayer, 1f);
        }

    }
}
