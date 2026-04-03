using UnityEngine;
using UnityEngine.UI;

namespace ProjectTwo.Enemy
{
    public class BossHealthUI : MonoBehaviour
    {
        [SerializeField] Slider hpBar;
        [SerializeField] SecurityLeader stageFactoryBoss;

        void OnEnable()
        {
            if (stageFactoryBoss != null)
            {
                stageFactoryBoss.OnHealthChanged += UpdateHPBar;
                
            }
        }
        void OnDisable()
        {
            if (stageFactoryBoss != null)
            {
                stageFactoryBoss.OnHealthChanged -= UpdateHPBar;
                
            }
        }

        void UpdateHPBar(float currentHealth, float maxHealth)
        {
            hpBar.value = currentHealth / maxHealth;
        }
    }
}
