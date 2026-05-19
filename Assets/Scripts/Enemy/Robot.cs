using UnityEngine;
using UnityEngine.AI;

namespace ProjectTwo.Enemy
{
    public class Robot : MonoBehaviour
    {
        const string PLAYER_STRING = "Player";
        private GameObject target;
        private NavMeshAgent agent;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();

        }
        private void Start()
        {
            target = GameObject.FindGameObjectWithTag(PLAYER_STRING);
        }

        
        private void Update()
        {
            if (!target) return;
            
            agent.SetDestination(target.transform.position);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(PLAYER_STRING))
            {
                EnemyHealth enemyHealth = GetComponent<EnemyHealth>();
                enemyHealth.Destruct();
            }
        }
    }
}

