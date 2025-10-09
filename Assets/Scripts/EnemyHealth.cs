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
            Instantiate(destroyVFX, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
