using UnityEngine;
using UnityEngine.AI;

public enum skeletonState
{
    Wandering,
    InvestigatingSmell,
    Chasing
}

[RequireComponent(typeof(NavMeshAgent))]
public class SkeletonBehavior : MonoBehaviour
{
    public skeletonState currentState = skeletonState.Wandering;

    [Header("Wandering Settings")]
    public float wanderRadius = 10f;
    public float wanderInterval = 5f;
    public float wanderSpeed = 2f;

    [Header("Chase Settings")]
    public float chaseSpeed = 4f;

    private NavMeshAgent agent;
    private float wanderTimer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = wanderSpeed;
        wanderTimer = wanderInterval;

        Debug.Log($"{name} iniciado en estado: {currentState}");
    }

    void Update()
    {
        switch (currentState)
        {
            case skeletonState.Wandering:
                Wander();
                break;

            case skeletonState.InvestigatingSmell:
                // El destino ya se establece desde el SmellSensor
                break;

            case skeletonState.Chasing:
                // Se puede implementar persecución aquí
                break;
        }
    }

    void Wander()
    {
        wanderTimer += Time.deltaTime;

        if (wanderTimer >= wanderInterval)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            wanderTimer = 0f;
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    public void OnPlayerSeen()
    {
        currentState = skeletonState.Chasing;
        agent.speed = chaseSpeed;
        Debug.Log($"{name} ha cambiado a estado: {currentState}");
    }

    public void ReactToPlayer()
    {
        OnPlayerSeen(); // Para BroadcastMessage
    }

    public void SetState(skeletonState newState)
    {
        currentState = newState;
        Debug.Log($"{name} ha cambiado a estado: {currentState}");
    }
}
