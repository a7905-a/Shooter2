using UnityEngine;
using UnityEngine.UI;

namespace ProjectTwo.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 5;
        [SerializeField] private Image[] healthBar;
        private int currentHealth;

        private void Awake()
        {
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

        private void HealthBarUI()
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
}