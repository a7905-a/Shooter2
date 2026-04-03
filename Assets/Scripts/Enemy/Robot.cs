using UnityEngine;
using UnityEngine.AI;

namespace ProjectTwo.Enemy
{
    public class Robot : MonoBehaviour
    {
        const string PLAYER_STRING = "Player";
        GameObject target;
        NavMeshAgent agent;

        void Awake()
        {
            agent = GetComponent<NavMeshAgent>();

        }
        void Start()
        {
            target = GameObject.FindGameObjectWithTag(PLAYER_STRING);
        }

        
        void Update()
        {
            if (!target) return;
            
            agent.SetDestination(target.transform.position);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(PLAYER_STRING))
            {
                EnemyHealth enemyHealth = GetComponent<EnemyHealth>();
                enemyHealth.Destruct();
            }
        }
    }
}

