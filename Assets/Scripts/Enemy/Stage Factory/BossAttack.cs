using System.Collections;
using UnityEngine;

namespace ProjectTwo.Enemy
{
    public class BossAttack : MonoBehaviour
    {
        [SerializeField] private Transform projectileSpawnPoint;
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private Transform bossTransform;
        [SerializeField] private Transform playerTarget;
        [SerializeField] private float fireRate = 2f;
        [SerializeField] private int damage = 3;

        private void OnEnable()
        {
            BossBattleTrigger.OnBossBattleStart += StartFiring;
        }

        private void OnDisable()
        {
            BossBattleTrigger.OnBossBattleStart -= StartFiring;
        }

        private void StartFiring()
        {
            StartCoroutine(FireRoutine());
        }

        IEnumerator FireRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(fireRate);
                Projectille newProjectille = Instantiate(projectilePrefab, projectileSpawnPoint.position, bossTransform.rotation).GetComponent<Projectille>();
                newProjectille.transform.LookAt(playerTarget);
                newProjectille.Init(damage);
            }
        }
    }
}
