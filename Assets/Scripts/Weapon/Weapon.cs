using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] ParticleSystem gunFlashEffect;
    
    
    public void Shoot(WeaponSO weaponSO)
    {

        RaycastHit rayhit;
        gunFlashEffect.Play();
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out rayhit, Mathf.Infinity))
        {
            GameObject hitToSpawn = PoolManager.instance.ActivateObject(0);
            PoolManager.instance.SetPosition(hitToSpawn, rayhit.point);
            

            EnemyHealth enemyHealth = rayhit.collider.GetComponent<EnemyHealth>();
            if (enemyHealth)
            {
                enemyHealth.TakeDamage(weaponSO.Damege);
            }

        }
    }



    
}
