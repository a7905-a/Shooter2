using System.Collections;
using UnityEngine;

namespace ProjectTwo.Enemy
{
    public class BossAttack : MonoBehaviour
    {
        [SerializeField] Transform projectileSpawnPoint;
        [SerializeField] GameObject projectilePrefab;
        [SerializeField] Transform bossTransform;
        [SerializeField] Transform playerTarget;
        [SerializeField] float fireRate = 2f;
        [SerializeField] int damage = 3;

        void OnEnable()
        {
            BossBattleTrigger.OnBossBattleStart += StartFiring;
        }

        void OnDisable()
        {
            BossBattleTrigger.OnBossBattleStart -= StartFiring;
        }

        void StartFiring()
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
