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
            RaycastHit rayhit;
            
            gunFlashEffect.Play();

            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out rayhit, Mathf.Infinity, interactionLayer, QueryTriggerInteraction.Ignore))
            {         
                //TrailRenderer trail = Instantiate(bulletTrail, bulletSpawnPoint.position, Quaternion.identity);

                GameObject trailObj = PoolManager.instance.ActivateObject(2);
                PoolManager.instance.SetPosition(trailObj, bulletSpawnPoint.position);
                trailObj.transform.rotation = Quaternion.identity;

                TrailRenderer trailRenderer = trailObj.GetComponent<TrailRenderer>();
                trailRenderer.Clear();

                StartCoroutine(SpawnTrail(trailRenderer, rayhit));
                
                EnemyHealth enemyHealth = rayhit.collider.GetComponent<EnemyHealth>();
                SecurityLeader boss = rayhit.collider.GetComponent<SecurityLeader>();

                if (enemyHealth)
                {
                    enemyHealth.TakeDamage(weaponSO.Damage);
                }
                
                if (boss)
                {
                    boss.TakeDamage(weaponSO.Damage);   
                }
            }
        
        }

        IEnumerator SpawnTrail(TrailRenderer Trail, RaycastHit hit)
        {
            float time = 0;
            Vector3 startPosition = Trail.transform.position;

            while(time < 1)
            {
                Trail.transform.position = Vector3.Lerp(startPosition, hit.point, time);
                time += (Time.deltaTime / Trail.time) * trailSpeed;

                yield return null;
            }
            Trail.transform.position = hit.point;

            GameObject hitToSpawn = PoolManager.instance.ActivateObject(0);
            PoolManager.instance.SetPosition(hitToSpawn, hit.point);

            //Destroy(Trail.gameObject, Trail.time);
            yield return new WaitForSeconds(Trail.time);
            PoolManager.instance.DeactivateObject(Trail.gameObject);
        }
    }
}
