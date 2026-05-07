using UnityEngine;
using UnityEngine.Audio; 

public class MusicZoneController : MonoBehaviour
{
    [Header("Configuració de Snapshots")]
    public AudioMixerSnapshot generalSnapshot;
    public AudioMixerSnapshot pobleSnapshot;
    public AudioMixerSnapshot covaSnapshot;
    public float transitionTime = 0.5f; 

   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ZonaPoble"))
        {
            pobleSnapshot.TransitionTo(transitionTime);
        }
        else if (other.CompareTag("cave"))
        {
            covaSnapshot.TransitionTo(transitionTime);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        
        if (other.CompareTag("ZonaPoble") || other.CompareTag("cave"))
        {
            generalSnapshot.TransitionTo(transitionTime);
        }
    }
}