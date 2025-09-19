using System;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    
    [SerializeField] AimZoom aimZoom;
    [SerializeField] int damageAmount = 1;
    [SerializeField] ParticleSystem gunFlash;
    Inputs input;
    Animator animator;
    

    public bool weaponReloading = false;

    void Awake()
    {
        input = GetComponent<Inputs>();
        animator = GetComponentInChildren<Animator>(); // 성능상 얼마나 좋은 나중에 알아보고 변경
    }

    void Update()
    {
        if (input.reload)
        {
            input.reload = false;

            if (weaponReloading)
            {
                return;
            }

            aimZoom.RigWeight(0);
            aimZoom.AimCondition(false);
            animator.SetLayerWeight(1, 1);
            animator.SetTrigger("Reload");
            weaponReloading = true;

        }

        if (input.zoom)
        {
            if (input.shoot)
            {
                HandleShoot();
                animator.SetBool("Shoot", true);
            }
            else
            {
                animator.SetBool("Shoot", false);
            }

        }

        
    }

    void HandleShoot()
    {
        if (!input.shoot) return;
        gunFlash.Play();
        input.shoot = false;
        RaycastHit rayhit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out rayhit, Mathf.Infinity))
        {
            EnemyHealth enemyHealth = rayhit.collider.GetComponent<EnemyHealth>();
            if (enemyHealth)
            {
                enemyHealth.TakeDamage(damageAmount);
            }

        }
    }

    public void Reload()
    {
        Debug.Log("Reload");
        aimZoom.RigWeight(1);
        weaponReloading = false;
        animator.SetLayerWeight(1, 0);
    }


    
}
