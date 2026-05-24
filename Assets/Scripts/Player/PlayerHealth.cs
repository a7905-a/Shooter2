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
            UpdateHealthUI();
        }

        public void TakeDamage(int amount)
        {
            currentHealth -= amount;

            UpdateHealthUI();

            if (currentHealth <= 0)
            {
                Retire();
            }
        }

        private void Retire()
        {
            Destroy(this.gameObject);
        }

        private void UpdateHealthUI()
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