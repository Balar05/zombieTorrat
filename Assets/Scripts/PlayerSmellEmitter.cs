using UnityEngine;

public class PlayerSmellEmitter : MonoBehaviour
{
    public GameObject bloodPrefab;
    public float dropInterval = 1f;
    private bool canDrop = true;

    private void Update()
    {
        if (canDrop)
        {
            canDrop = false;
            Instantiate(bloodPrefab, transform.position, Quaternion.identity);
            StartCoroutine(ResetDropTimer());
        }
    }

    private System.Collections.IEnumerator ResetDropTimer()
    {
        yield return new WaitForSeconds(dropInterval);
        canDrop = true;
    }
}
