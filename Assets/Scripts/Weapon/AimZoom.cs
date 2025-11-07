using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AimZoom : MonoBehaviour
{
    [Header("Inspector-Driven DI")]
    [SerializeField] Inputs input;
    [SerializeField] Animator animator;
    [SerializeField] PlayerMove playerMove;
    [SerializeField] Transform playerBody;
    [SerializeField] WeaponSO weaponSO;
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
    
    int rifleActionLayer = 1;




    void Awake()
    {
        camTrans = Camera.main.transform;
        animator = GetComponentInParent<Animator>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
    }

    void Update()
    {
        
        AimCheck();
        
    }

    void AimCheck()
    {
        Vector3 targetPoint = Vector3.zero;

        if (activeWeapon.weaponReloading)
        {
            return;
        }

        if (input.zoom)
        {
            AimCondition(true);
            animator.SetLayerWeight(rifleActionLayer, 1f);

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
            RigWeight(1);

        }
        else
        {
            AimCondition(false);
            animator.SetLayerWeight(rifleActionLayer, 0f);
            RigWeight(0);
            animator.SetBool("Shoot", false);
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
