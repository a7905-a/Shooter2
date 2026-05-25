using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using ProjectTwo.Manager;
using ProjectTwo.Player;

namespace ProjectTwo.Weapon
{

    public class AimZoom : MonoBehaviour
    {
        // ==========================================
        // 이벤트 선언
        // ==========================================
        public static event Action<bool> OnWeaponZoom;


        [Header("플레이어")]
        [SerializeField] private Transform playerBody;


        [Header("조준 오브젝트")]
        [SerializeField] private CinemachineCamera aimCam;
        //aimCrosshair는 HUD 오브젝트의 AimImage를 가져옴
        [SerializeField] private GameObject aimCrosshair;
        [SerializeField] private GameObject aimTarget;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float defaultAimDis = 25f;


        [Header("애니메이션 리깅")]
        [SerializeField] private Rig handRig;
        [SerializeField] private Rig aimRig;


        // ==========================================
        // 내부 참조
        // ==========================================
        private Inputs input;
        private PlayerMove playerMove;
        private ActiveWeapon activeWeapon;
        private Transform camPos;
        private RaycastHit rayhit;


        private void Awake()
        {
            input = GetComponentInParent<Inputs>();
            playerMove = GetComponentInParent<PlayerMove>();
            activeWeapon = GetComponentInParent<ActiveWeapon>();
        }

        private void Start()
        {
            camPos = Camera.main.transform;
        }

        private void Update()
        {        
            AimCheck(); 
        }

        private void AimCheck()
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

                targetPoint = CalculateAimPoint();
                aimTarget.transform.position = targetPoint;

                //플레이어가 조준점 방향을 바라보도록 회전
                UpdateAimRotation(targetPoint);

                RigWeight(1f);

            }
            else
            {
                AimCondition(false);
                OnWeaponZoom?.Invoke(false);
                RigWeight(0f);
            }
        }

        private void UpdateAimRotation(Vector3 targetPoint)
        {
            Vector3 targetAim = targetPoint;
            targetAim.y = playerBody.position.y;
            Vector3 aimDirection = (targetAim - playerBody.position).normalized;
            playerBody.forward = Vector3.Lerp(playerBody.forward, aimDirection, Time.deltaTime * 30f);
        }


        public void AimCondition(bool check)
        {
            aimCam.gameObject.SetActive(check);
            aimCrosshair.gameObject.SetActive(check);
            playerMove.isAimingMove = check;
        }

        public void RigWeight(float weight)
        {
            handRig.weight = weight;
            aimRig.weight = weight;

        }

        private Vector3 CalculateAimPoint()
        {
            if (Physics.Raycast(camPos.position, camPos.forward, out rayhit, Mathf.Infinity, layerMask))
            {
                return rayhit.point;
            }

            return camPos.position + camPos.forward * defaultAimDis;
        }
        
    }
}
