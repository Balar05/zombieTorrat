using UnityEngine;
using UnityEngine.AI;

public class SkeletonInvestigatingSmell : MonoBehaviour
{
    public float investigatingSpeed = 2.5f;

    private NavMeshAgent agent;
    private SkeletonStateManager manager;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        manager = GetComponent<SkeletonStateManager>();
    }

    public void Run()
    {
        agent.speed = investigatingSpeed;

        // Si aún no tiene destino, lo asignamos
        if (!agent.hasPath)
        {
            agent.SetDestination(manager.smellTarget);
        }

        // Cuando llega al destino, vuelve a deambular
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            manager.ChangeState(SkeletonState.Wandering);
        }
    }
}
