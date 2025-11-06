using UnityEngine;

public enum SkeletonState
{
    Wandering,
    InvestigatingSmell,
    Chasing
}

public class SkeletonStateManager : MonoBehaviour
{
    public SkeletonState currentState;

    private SkeletonWandering wandering;
    private SkeletonInvestigatingSmell investigating;
    private SkeletonChasing chasing;

    public Vector3 smellTarget;
    public Transform player;
    public float detectionRange = 200f;

    void Start()
    {
        // Gets components from the 3 states' scripts.
        wandering = GetComponent<SkeletonWandering>();
        investigating = GetComponent<SkeletonInvestigatingSmell>();
        chasing = GetComponent<SkeletonChasing>();

        ChangeState(SkeletonState.Wandering);
    }

    void Update()
    {
        switch (currentState)
        {
            case SkeletonState.Wandering:
                wandering?.Run();

                if (player != null)
                {
                    float distanceToPlayer = Vector3.Distance(transform.position, player.position);

                    if (distanceToPlayer <= detectionRange)
                    {
                        ChangeState(SkeletonState.Chasing);
                        AlertNearbySkeletons();
                    }
                }
                break;

            case SkeletonState.InvestigatingSmell:
                investigating?.Run();
                break;

            case SkeletonState.Chasing:
                chasing?.Run();
                break;
        }
    }

    public void ChangeState(SkeletonState newState)
    {
        currentState = newState;

        if (newState == SkeletonState.InvestigatingSmell)
        {
            investigating?.Run(); // fuerza el movimiento hacia la sangre
        }
    }


    public void OnPlayerDetected()
    {
        if (currentState != SkeletonState.Chasing)
        {
            ChangeState(SkeletonState.Chasing);
        }
    }

    void AlertNearbySkeletons()
    {
        // Checks nearby Skeletons within s Sphere and Broadcasts a Message to the Skeletons with the tag Skeleton

        Collider[] nearby = Physics.OverlapSphere(transform.position, detectionRange);

        foreach (Collider col in nearby)
        {
            if (col.CompareTag("Skeleton") && col.gameObject != this.gameObject)
            {
                col.gameObject.BroadcastMessage("OnPlayerDetected", SendMessageOptions.DontRequireReceiver);
            }
        }
    }

}
