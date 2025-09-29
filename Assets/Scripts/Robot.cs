using UnityEngine;
using UnityEngine.AI;

public class Robot : MonoBehaviour
{
    [SerializeField] GameObject target;
    NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

    }
    void Start()
    {
        
    }

    
    void Update()
    {
        agent.SetDestination(target.transform.position);
    }
}
