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
            
            GameObject destroyVFX = PoolManager.instance.ActivateObject(1);
            PoolManager.instance.SetPosition(destroyVFX, transform.position);
            // DeactivateObject가 없는 이유는 파티클 이펙트는 StopAcion의 Disable로 설정되어 있어서 자동으로 비활성화됨
            // 처음엔 비활성이 안되서 왜 그런가 싶었는데 이펙트 설정에 라이프타임을 1초로 하니까 비활성화 되더라, 라이프 타임 설정 문제였음
            Destroy(this.gameObject);
        }
    }
}
