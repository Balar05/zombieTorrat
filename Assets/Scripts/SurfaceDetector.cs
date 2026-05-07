using UnityEngine;

public class SurfaceDetector : MonoBehaviour
{
    public PlayerSoundEvents soundEvents;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            soundEvents.superficieActual = "Aigua";    
        }
        else if (other.CompareTag("Wood"))
        {
            soundEvents.superficieActual = "Fusta";
        }
        else if (other.CompareTag("Path"))
        {
            soundEvents.superficieActual = "Herba";
        }
    }
}