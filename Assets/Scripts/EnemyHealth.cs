using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float startingHealth = 3;
    [SerializeField] Slider HPbar;
    [SerializeField] GameObject destroyVFX;
    float currentHealth;

    void Awake()
    {
        currentHealth = startingHealth;
    }

    void Update()
    {
        HPbar.value = currentHealth / startingHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            //Instantiate(destroyVFX, transform.position, Quaternion.identity);
            GameObject destroyVFX = PoolManager.instance.ActivateObject(1);
            PoolManager.instance.SetPosition(destroyVFX, transform.position);
            PoolManager.instance.DeactivateObject(destroyVFX);// 바로 없어지는게 맞나??
            Destroy(this.gameObject);
        }
    }
}
