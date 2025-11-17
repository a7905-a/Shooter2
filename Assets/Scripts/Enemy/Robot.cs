using UnityEngine;
using UnityEngine.AI;

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
        agent.SetDestination(target.transform.position);
    }
}
