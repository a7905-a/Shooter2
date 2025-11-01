using UnityEngine;
using UnityEngine.AI;

public class Robot : MonoBehaviour
{
    GameObject target;
    NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

    }
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    
    void Update()
    {
        agent.SetDestination(target.transform.position);
    }
}
