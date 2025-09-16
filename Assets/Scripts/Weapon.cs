using System;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] AimZoom aimZoom;
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
                animator.SetBool("Shoot", true);
            }
            else
            {
                animator.SetBool("Shoot", false);
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
