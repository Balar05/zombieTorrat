using UnityEngine;
using Unity.Behavior;

public class SmellSensor : MonoBehaviour
{
    [SerializeReference] public BlackboardVariable<Vector3> SmellTarget;
    [SerializeReference] public BlackboardVariable<bool> HasSmell;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SmellSource"))
        {
            SmellTarget.Value = other.transform.position;
            HasSmell.Value = true;

            Debug.Log("👃 Zombie ha detectat sang → SmellTarget actualitzat!");
        }
    }
}
