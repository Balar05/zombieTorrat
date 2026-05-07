using UnityEngine;
using UnityEngine.Audio; 

public class MusicZoneController : MonoBehaviour
{
    //Inizialitzar els 3 snapshots i el temps de transició.
    public AudioMixerSnapshot generalSnapshot;
    public AudioMixerSnapshot pobleSnapshot;
    public AudioMixerSnapshot covaSnapshot;
    public float transitionTime = 0.5f; 

   
    private void OnTriggerEnter(Collider other)
    {
        //Si entrem al poble, configurem el snapshot del poble.
        if (other.CompareTag("ZonaPoble"))
        {
            pobleSnapshot.TransitionTo(transitionTime);
        }
        //Si entrem a la cova, configurem el snapshot de la cova.
        else if (other.CompareTag("cave"))
        {
            covaSnapshot.TransitionTo(transitionTime);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //Si sortim del poble o de la cova, configurem el snapshot general del bosc.
        if (other.CompareTag("ZonaPoble") || other.CompareTag("cave"))
        {
            generalSnapshot.TransitionTo(transitionTime);
        }
    }
}