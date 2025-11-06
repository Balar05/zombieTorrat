using UnityEngine;
using UnityEngine.AI;

public class SkeletonInvestigatingSmell : MonoBehaviour
{
    public float investigatingSpeed = 2.5f;

    private NavMeshAgent agent;
    private SkeletonStateManager manager;
    private bool hasSmellTarget = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        manager = GetComponent<SkeletonStateManager>();
    }

    public void Run()
    {
        agent.speed = investigatingSpeed;

        if (hasSmellTarget && agent.isOnNavMesh)
        {
            // Assign destination if the skeleton does not have a path to follow or it has arrived
            if (!agent.hasPath || agent.remainingDistance <= agent.stoppingDistance)
            {
                agent.SetDestination(manager.smellTarget);
            }


            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                hasSmellTarget = false;
                manager.ChangeState(SkeletonState.Wandering);
            }
        }
    }

    // On Trigger detects if the Sphere Collider of the Skeleton touches an object
    // with a Trigger and the Tag "SmellSource"
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SmellSource"))
        {
            manager.smellTarget = other.transform.position;
            hasSmellTarget = true;
            manager.ChangeState(SkeletonState.InvestigatingSmell);
            Debug.Log($"Blood smelled");
        }
    }
}
