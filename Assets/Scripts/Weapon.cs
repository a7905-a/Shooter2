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
            Instantiate(weaponSO.HitEffect, rayhit.point, Quaternion.identity); // 오브젝트 풀링으로 변경하고 싶긴함

            EnemyHealth enemyHealth = rayhit.collider.GetComponent<EnemyHealth>();
            if (enemyHealth)
            {
                enemyHealth.TakeDamage(weaponSO.Damege);
            }

        }
    }



    
}
