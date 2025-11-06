using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class SkeletonSpawner : MonoBehaviour
{
    public GameObject skeletonPrefab;
    public int numberOfSkeletons = 10;
    public float spawnRadius = 20f;
    public float minDistanceBetweenSkeletons = 2f;

    private List<Vector3> usedPositions = new List<Vector3>();

    void Start()
    {
        SpawnSkeletons();
    }

    void SpawnSkeletons()
    {
        int spawned = 0;
        int attempts = 0;

        while (spawned < numberOfSkeletons && attempts < numberOfSkeletons * 10)
        {
            Vector3 randomPoint = transform.position + Random.insideUnitSphere * spawnRadius;
            randomPoint.y = transform.position.y;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 10f, NavMesh.AllAreas))
            {
                if (IsFarEnough(hit.position))
                {
                    Instantiate(skeletonPrefab, hit.position, Quaternion.identity);
                    usedPositions.Add(hit.position);
                    spawned++;
                }
            }

            attempts++;
        }
    }

    bool IsFarEnough(Vector3 position)
    {
        foreach (Vector3 used in usedPositions)
        {
            if (Vector3.Distance(used, position) < minDistanceBetweenSkeletons)
                return false;
        }
        return true;
    }
}
