using System.Collections;
using UnityEngine;

namespace ProjectTwo.Enemy
{
    public class BossSpawn : MonoBehaviour
    {
        [SerializeField] private GameObject bossSpawnEffect;
        [SerializeField] private Transform playerTarget;
        [SerializeField] private float riseHeight = 10f;
        [SerializeField] private float riseDuration = 3f;
        private Vector3 initialPosition;
        private bool isSpawned = false;

        private void OnEnable()
        {
            BossBattleTrigger.OnBossBattleStart += StartBossRise;
        }
        private void OnDisable()
        {
            BossBattleTrigger.OnBossBattleStart -= StartBossRise;
        }
        private void Awake()
        {
            initialPosition = transform.position;
        }

        private void Update()
        {
            if (isSpawned)
            {
                this.transform.LookAt(playerTarget);
            }
        }


        private void StartBossRise()
        {
            StartCoroutine(RiseBoss());
        }

        IEnumerator RiseBoss()
        {
            isSpawned = false;
            transform.position = initialPosition;

            Vector3 startPos = transform.position;
            Vector3 targetPos = initialPosition + Vector3.up * riseHeight;

            float elapsed = 0f;

            while (elapsed < riseDuration)
            {
                transform.position = Vector3.Lerp(startPos, targetPos, elapsed / riseDuration);
                bossSpawnEffect.SetActive(true);
                elapsed += Time.deltaTime;
                yield return null;
            }
            isSpawned = true;
        }
    }
}
