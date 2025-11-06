using UnityEngine;

public class SkeletonVision : MonoBehaviour
{
    public Transform playerTransform;
    public float viewAngle = 60f;
    public float viewDistance = 15f;
    public LayerMask obstacleMask;

    private SkeletonStateManager manager;

    void Start()
    {
        manager = GetComponent<SkeletonStateManager>();
    }

    void Update()
    {
        if (playerTransform == null || manager == null)
            return;

        Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        bool canSeePlayer = angleToPlayer < viewAngle / 2f &&
                            distanceToPlayer < viewDistance &&
                            !Physics.Raycast(transform.position + Vector3.up, directionToPlayer, distanceToPlayer, obstacleMask);

        if (canSeePlayer && manager.currentState != SkeletonState.Chasing)
        {
            manager.ChangeState(SkeletonState.Chasing);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (playerTransform == null)
            return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewDistance);

        Vector3 forward = transform.forward;
        Vector3 leftBoundary = Quaternion.Euler(0, -viewAngle / 2f, 0) * forward;
        Vector3 rightBoundary = Quaternion.Euler(0, viewAngle / 2f, 0) * forward;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary * viewDistance);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary * viewDistance);
    }

}
