using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    [SerializeField] float startingHealth = 10f;
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

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
