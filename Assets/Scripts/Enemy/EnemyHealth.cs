using System;
using UnityEngine;
using UnityEngine.UI;
using ProjectTwo.Manager;

namespace ProjectTwo.Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        public static event Action<GameObject> OnEnemyDied;

        [SerializeField] private float startingHealth = 3;
        [SerializeField] private Slider HPbar;
        [SerializeField] private GameObject destroyVFX;
        private SpawnSwitch spawnSwitch;
        private float currentHealth;

        private void Awake()
        {
            currentHealth = startingHealth;
        }
        private void Update()
        {
            HPbar.value = currentHealth / startingHealth;
        }

        public void SetUpSwitch(SpawnSwitch spawnSwitch)
        {
            this.spawnSwitch = spawnSwitch;
        }

        public void TakeDamage(float amount)
        {
            currentHealth -= amount;

            if (currentHealth <= 0)
            {
                Destruct();
            }
        }

        public void Destruct()
        {
            GameObject destroyVFX = PoolManager.instance.ActivateObject(1);
            PoolManager.instance.SetPosition(destroyVFX, transform.position);
            // DeactivateObject가 없는 이유는 파티클 이펙트는 StopAcion의 Disable로 설정되어 있어서 자동으로 비활성화됨
            // 만약 비활성이 되지 않는다면 이펙트 설정에 라이프타임을 1초로 하면 비활성 된다.
            
            DropTable dropTable = GetComponent<DropTable>();
            if(dropTable != null)
            {
                dropTable.DropItem();
            }

            OnEnemyDied?.Invoke(gameObject);
            Destroy(this.gameObject);
        }
    }
}
