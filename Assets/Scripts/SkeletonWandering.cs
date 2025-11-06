using UnityEngine;
using UnityEngine.AI;

public class SkeletonWandering : MonoBehaviour
{
    public float wanderRadius = 10f;
    public float wanderSpeed = 2f;
    public float wanderInterval = 3f;

    private NavMeshAgent agent;
    private float wanderTimer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = wanderSpeed;
        wanderTimer = wanderInterval;

        SetNewDestination();
    }

    public void Run()
    {
        wanderTimer += Time.deltaTime;

        if (wanderTimer >= wanderInterval)
        {
            SetNewDestination();
            wanderTimer = 0f;
        }
    }

    void SetNewDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += transform.position;
        randomDirection.y = transform.position.y;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, 10f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }
}
