using UnityEngine;

public class SurfaceDetector : MonoBehaviour
{
    public PlayerSoundEvents soundEvents;

    private void OnTriggerEnter(Collider other)
    {
        // Aquest Debug ens avisarà a la consola cada cop que toquem un Trigger
        Debug.Log("He entrat al trigger: " + other.gameObject.name + " amb el Tag: " + other.tag);

        if (other.CompareTag("Water"))
        {
            soundEvents.superficieActual = "Aigua";
            Debug.Log("Canviant so a: Aigua");
        }
        else if (other.CompareTag("Wood"))
        {
            soundEvents.superficieActual = "Fusta";
            Debug.Log("Canviant so a: Fusta");
        }
        else if (other.CompareTag("Path"))
        {
            soundEvents.superficieActual = "Herba";
            Debug.Log("Canviant so a: Herba");
        }
    }
}