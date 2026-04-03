using System;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectTwo.Enemy
{
    public class SecurityLeader : MonoBehaviour
    {
        public event Action <float, float> OnHealthChanged;

        [SerializeField] float startingHealth = 10f;
        //[SerializeField] Slider HPbar;
        float currentHealth;

        void Awake()
        {
            currentHealth = startingHealth;
        }

        void Start()
        {
            OnHealthChanged?.Invoke(currentHealth, startingHealth);
        }   

        void Update()
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

        void Destory()
        {
            Destroy(this.gameObject);
        }
    }
}
