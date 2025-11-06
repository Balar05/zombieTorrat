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

        if (manager.smellTarget != Vector3.zero && agent.isOnNavMesh)
        {
            // Siempre reasigna el destino al entrar en estado
            agent.SetDestination(manager.smellTarget);

            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                manager.ChangeState(SkeletonState.Wandering);
            }
        }
    }

}
