using System;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectTwo.Enemy
{
    public class SecurityLeader : MonoBehaviour
    {
        public event Action <float, float> OnHealthChanged;

        [SerializeField] private float startingHealth = 10f;
        //[SerializeField] Slider HPbar;
        private float currentHealth;

        private void Awake()
        {
            currentHealth = startingHealth;
        }

        private void Start()
        {
            OnHealthChanged?.Invoke(currentHealth, startingHealth);
        }   

        private void Update()
        {
            //HPbar.value = currentHealth / startingHealth;
        }

        public void TakeDamage(float amount)
        {
            currentHealth -= amount;
            OnHealthChanged?.Invoke(currentHealth, startingHealth);

            if (currentHealth <= 0)
            {
                Destory();
            }
        }

        private void Destory()
        {
            Destroy(this.gameObject);
        }
    }
}
