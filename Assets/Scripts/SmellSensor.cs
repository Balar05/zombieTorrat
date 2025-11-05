using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SmellSensor : MonoBehaviour
{
    private NavMeshAgent agent;
    private Queue<Vector3> trail = new Queue<Vector3>();

    private void Awake()
    {
        agent = GetComponentInParent<NavMeshAgent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SmellSource"))
        {
            trail.Enqueue(other.transform.position);

            if (!agent.hasPath || agent.remainingDistance < 0.4f)
                MoveToNextPoint();
        }
    }

    private void Update()
    {
        if (!agent.hasPath || agent.remainingDistance < 0.4f)
            MoveToNextPoint();
    }

    private void MoveToNextPoint()
    {
        if (trail.Count == 0) return;
        agent.SetDestination(trail.Dequeue());
    }
}
