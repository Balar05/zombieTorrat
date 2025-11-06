using UnityEngine;
using UnityEngine.AI;

public class SkeletonChasing : MonoBehaviour
{
    public Transform playerTransform;
    public float chaseSpeed = 4f;
    public float viewAngle = 60f;
    public float viewDistance = 15f;
    public LayerMask obstacleMask;

    private NavMeshAgent agent;
    private SkeletonStateManager manager;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        manager = GetComponent<SkeletonStateManager>();
    }

    public void Run()
    {
        if (playerTransform == null)
            return;

        agent.speed = chaseSpeed;
        agent.SetDestination(playerTransform.position);

        // Verifica si el jugador sigue visible
        Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        if (angleToPlayer > viewAngle / 2f || distanceToPlayer > viewDistance ||
            Physics.Raycast(transform.position + Vector3.up, directionToPlayer, distanceToPlayer, obstacleMask))
        {
            // Si el jugador se escapa o se esconde, vuelve a deambular
            manager.ChangeState(SkeletonState.Wandering);
        }
    }
}
