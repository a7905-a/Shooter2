using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 5;
    [SerializeField] Image[] healthBar;
    int currentHealth;

    void Awake()
    {
        if (healthBar == null)
        {
            Debug.LogError("HP바 이미지가 할당되지 않음");
            enabled = false;
            return;
        }

        currentHealth = maxHealth;
        HealthBarUI();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        HealthBarUI();
        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void HealthBarUI()
    {
        if (healthBar == null) return;

        for (int i = 0; i < healthBar.Length; i++)
        {
            if (i < currentHealth)
            {
                healthBar[i].gameObject.SetActive(true);
            }
            else
            {
                healthBar[i].gameObject.SetActive(false);
            }
        }
    }
}
