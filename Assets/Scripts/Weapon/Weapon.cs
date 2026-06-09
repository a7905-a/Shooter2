using UnityEngine;
using System.Collections;
using ProjectTwo.Manager;
using ProjectTwo.Enemy;

namespace ProjectTwo.Weapon
{
    public class Weapon : MonoBehaviour
    {
        [Header("무기 효과")]
        [SerializeField] private ParticleSystem gunFlashEffect;
        [SerializeField] private TrailRenderer bulletTrail;
        [SerializeField] private float trailSpeed = 50f;

        [Header("상호작용할 레이어")]
        [SerializeField] private LayerMask interactionLayer;

        [Header("총알 발사 위치")]
        [SerializeField] private Transform bulletSpawnPoint;
        
        
        public void Shoot(WeaponSO weaponSO)
        {
            gunFlashEffect.Play();
            
            // 카메라 기준 레이캐스트 : 유저가 바라보는 실제 타겟 지점 포착
            Vector3 targetPoint = Vector3.zero;
            RaycastHit cameraRayHit;
            float defaultAimDistance = 40f; // 레이캐스트가 아무것도 맞지 않았을 때의 기본 사거리

            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out cameraRayHit, Mathf.Infinity, interactionLayer, QueryTriggerInteraction.Ignore))
            {        
                targetPoint = cameraRayHit.point; 

                
            }
            else
            {
                // 허공을 조준한 경우, 카메라 앞의 일정 거리 지점을 타겟으로 설정
                targetPoint = Camera.main.transform.position + Camera.main.transform.forward * defaultAimDistance;
            }

            // 실제 총구 기준 레이캐스트 : 총구에서 타겟 지점을 향해 발사
            RaycastHit muzzleRayHit;

            Vector3 fireDirection = (targetPoint - bulletSpawnPoint.position).normalized;

            // 총구와 타겟 사이의 실제 거리 계산
            // 실제 거리를 계산하지 않는다면, 총구에서 나온 레이캐스트가 타겟을 지나쳐 허공에 맞는 경우가 발생함
            float distanceToTarget = Vector3.Distance(bulletSpawnPoint.position, targetPoint);
            

            if (Physics.Raycast(bulletSpawnPoint.position, fireDirection, out muzzleRayHit, distanceToTarget + 0.5f, interactionLayer, QueryTriggerInteraction.Ignore))
            {
                ProcessHit(muzzleRayHit, weaponSO);
            }
            else
            {
                if (cameraRayHit.collider != null)
                {
                    ProcessHitAtTarget(targetPoint, cameraRayHit.collider, weaponSO);
                }
                else
                {
                    // 완전한 허공/공중 사격인 경우 연출만 처리
                    ProcessNoHit(targetPoint);
                }
            }
        
        }

        // 장애물이나 지형에 가로막혔을 때의 처리 로직
        private void ProcessHit(RaycastHit hit, WeaponSO weaponSO)
        {
            GameObject trailObj = PoolManager.instance.ActivateObject(2);
            PoolManager.instance.SetPosition(trailObj, bulletSpawnPoint.position);
            trailObj.transform.rotation = Quaternion.identity;

            TrailRenderer trailRenderer = trailObj.GetComponent<TrailRenderer>();
            trailRenderer.Clear();

            StartCoroutine(SpawnTrail(trailRenderer, hit.point, true));

            ApplyDamage(hit.collider, weaponSO);
        }

        private void ProcessHitAtTarget(Vector3 targetPoint, Collider targetCollider, WeaponSO weaponSO)
        {
            GameObject trailObj = PoolManager.instance.ActivateObject(2);
            PoolManager.instance.SetPosition(trailObj, bulletSpawnPoint.position);
            trailObj.transform.rotation = Quaternion.identity;

            TrailRenderer trailRenderer = trailObj.GetComponent<TrailRenderer>();
            trailRenderer.Clear();

            // 카메라 조준 지점(targetPoint)까지 궤적 이동 코루틴 실행
            StartCoroutine(SpawnTrail(trailRenderer, targetPoint, true));

            // 데미지 처리
            ApplyDamage(targetCollider, weaponSO);
        }

        private void ProcessNoHit(Vector3 targetPoint)
        {
            GameObject trailObj = PoolManager.instance.ActivateObject(2);
            PoolManager.instance.SetPosition(trailObj, bulletSpawnPoint.position);
            trailObj.transform.rotation = Quaternion.identity;

            TrailRenderer trailRenderer = trailObj.GetComponent<TrailRenderer>();
            trailRenderer.Clear();

            // 허공 지정 좌표까지 이동하되, 명중 이펙트는 생성하지 않도록 false 전달
            StartCoroutine(SpawnTrail(trailRenderer, targetPoint, false));
        }

        private static void ApplyDamage(Collider targetCollider, WeaponSO weaponSO)
        {
            EnemyHealth enemyHealth = targetCollider.GetComponent<EnemyHealth>();
            SecurityLeader boss = targetCollider.GetComponent<SecurityLeader>();

            if (enemyHealth)
            {
                enemyHealth.TakeDamage(weaponSO.Damage);
            }

            if (boss)
            {
                boss.TakeDamage(weaponSO.Damage);
            }
        }


        IEnumerator SpawnTrail(TrailRenderer Trail, Vector3 destination, bool showHitEffect)
        {
            float time = 0;
            Vector3 startPosition = Trail.transform.position;

            while(time < 1)
            {
                Trail.transform.position = Vector3.Lerp(startPosition, destination, time);
                time += (Time.deltaTime / Trail.time) * trailSpeed;

                yield return null;
            }
            Trail.transform.position = destination;

            if (showHitEffect)
            {
                GameObject hitToSpawn = PoolManager.instance.ActivateObject(0);
                PoolManager.instance.SetPosition(hitToSpawn, destination);
                
            }

            yield return new WaitForSeconds(Trail.time);
            PoolManager.instance.DeactivateObject(Trail.gameObject);
        }
    }
}
