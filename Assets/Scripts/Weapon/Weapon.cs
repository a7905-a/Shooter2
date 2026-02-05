using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] ParticleSystem gunFlashEffect;
    [SerializeField] LayerMask interactionLayer;
    
    
    public void Shoot(WeaponSO weaponSO)
    {

        RaycastHit rayhit;
        gunFlashEffect.Play();
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out rayhit, Mathf.Infinity, interactionLayer, QueryTriggerInteraction.Ignore))
        {
            GameObject hitToSpawn = PoolManager.instance.ActivateObject(0);
            PoolManager.instance.SetPosition(hitToSpawn, rayhit.point);
            

            EnemyHealth enemyHealth = rayhit.collider.GetComponent<EnemyHealth>();
            Boss boss = rayhit.collider.GetComponent<Boss>();
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
}
