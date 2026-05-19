using UnityEngine;
using UnityEngine.UI;

namespace ProjectTwo.Enemy
{
    public class BossHealthUI : MonoBehaviour
    {
        [SerializeField] private Slider hpBar;
        [SerializeField] private SecurityLeader stageFactoryBoss;

        private void OnEnable()
        {
            if (stageFactoryBoss != null)
            {
                stageFactoryBoss.OnHealthChanged += UpdateHPBar;
                
            }
        }
        private void OnDisable()
        {
            if (stageFactoryBoss != null)
            {
                stageFactoryBoss.OnHealthChanged -= UpdateHPBar;
                
            }
        }

        private void UpdateHPBar(float currentHealth, float maxHealth)
        {
            hpBar.value = currentHealth / maxHealth;
        }
    }
}
