using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    [SerializeField] ParticleSystem gunFlashEffect;
    [SerializeField] LayerMask interactionLayer;
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] TrailRenderer bulletTrail;
    
    
    public void Shoot(WeaponSO weaponSO)
    {
        RaycastHit rayhit;
        
        gunFlashEffect.Play();

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out rayhit, Mathf.Infinity, interactionLayer, QueryTriggerInteraction.Ignore))
        {         
            TrailRenderer trail = Instantiate(bulletTrail, bulletSpawnPoint.position, Quaternion.identity);

            StartCoroutine(SpawnTrail(trail, rayhit));
            
            EnemyHealth enemyHealth = rayhit.collider.GetComponent<EnemyHealth>();
            SecurityLeader boss = rayhit.collider.GetComponent<SecurityLeader>();

            if (enemyHealth)
            {
                enemyHealth.TakeDamage(weaponSO.Damege);
            }
            
            if (boss)
            {
                boss.TakeDamage(weaponSO.Damege);   
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
            time += Time.deltaTime / Trail.time;

            yield return null;
        }
        Trail.transform.position = hit.point;
        //Instantiate(Camera.main.transform.position, hit.point, Quaternion.LookRotation(hit.normal));

        GameObject hitToSpawn = PoolManager.instance.ActivateObject(0);
        PoolManager.instance.SetPosition(hitToSpawn, hit.point);

        Destroy(Trail.gameObject, Trail.time);
    }
}
