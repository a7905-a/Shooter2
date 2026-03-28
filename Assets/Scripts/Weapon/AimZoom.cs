using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AimZoom : MonoBehaviour
{
    public static event Action<bool> OnWeaponZoom;

    [Header("Inspector-Driven DI")]
    [SerializeField] Transform playerBody;
    //[SerializeField] WeaponSO weaponSO;
    Inputs input;
    PlayerMove playerMove;
    ActiveWeapon activeWeapon;

    [Header("Object")]
    [SerializeField] CinemachineCamera aimCam;

    [SerializeField] GameObject aimCorsshair;
    [SerializeField] GameObject aimObj;
    [SerializeField]LayerMask layerMask;

    [Header("Rig")]
    [SerializeField] Rig handRig;
    [SerializeField] Rig aimRig;


    [Header("Value")]
    [SerializeField] float aimObjDis = 25f;
    

    Transform camTrans;
    RaycastHit rayhit;


    void Awake()
    {
        input = GetComponentInParent<Inputs>();
        playerMove = GetComponentInParent<PlayerMove>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
    }

    void Start()
    {
        camTrans = Camera.main.transform;
    }

    void Update()
    {        
        AimCheck(); 
    }

    void AimCheck()
    {
        Vector3 targetPoint = Vector3.zero;
        

        if (!activeWeapon.HasWeapon() || activeWeapon.weaponReloading)
        {
            return;
        }

        if (input.zoom)
        {
            
            
            AimCondition(true);
            OnWeaponZoom?.Invoke(true);

            if (Physics.Raycast(camTrans.position, camTrans.forward, out rayhit, Mathf.Infinity, layerMask))
            {
                targetPoint = rayhit.point;
                aimObj.transform.position = rayhit.point;

            }
            else
            {
                targetPoint = camTrans.position + camTrans.forward * aimObjDis;
                aimObj.transform.position = camTrans.position + camTrans.forward * aimObjDis;
            }

            Vector3 targetAim = targetPoint;
            targetAim.y = playerBody.position.y;
            Vector3 aimDirection = (targetAim - playerBody.position).normalized;

            playerBody.forward = Vector3.Lerp(playerBody.forward, aimDirection, Time.deltaTime * 30f);
            RigWeight(1f);

        }
        else
        {
            AimCondition(false);
            OnWeaponZoom?.Invoke(false);
            RigWeight(0f);
        }
    }
    

    
    public void AimCondition(bool check)
    {
        aimCam.gameObject.SetActive(check);
        aimCorsshair.gameObject.SetActive(check);
        playerMove.isAimingMove = check;
    }

    public void RigWeight(float weight)
    {
        handRig.weight = weight;
        aimRig.weight = weight;

    }
    

    
}
