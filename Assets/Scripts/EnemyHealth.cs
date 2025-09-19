using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float startingHealth = 3;
    [SerializeField] Slider HPbar;
    float currentHealth;

    void Awake()
    {
        currentHealth = startingHealth;
    }

    void Update()
    {
        HPbar.value = currentHealth / startingHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
