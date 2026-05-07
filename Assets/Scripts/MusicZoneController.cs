using UnityEngine;
using UnityEngine.Audio; // Obligatori per fer servir Snapshots

public class MusicZoneController : MonoBehaviour
{
    [Header("Configuració de Snapshots")]
    public AudioMixerSnapshot generalSnapshot;
    public AudioMixerSnapshot pobleSnapshot;
    public AudioMixerSnapshot covaSnapshot;
    public float transitionTime = 0.5f; // Temps que triga el "fade"

    // Criteri 8: Música interactiva per zones
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ZonaPoble"))
        {
            pobleSnapshot.TransitionTo(transitionTime);
            Debug.Log("Entrant al Poble: Canviant música...");
        }
        else if (other.CompareTag("cave"))
        {
            covaSnapshot.TransitionTo(transitionTime);
            Debug.Log("Entrant a la cova: Canviant música...");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Quan sortim de qualsevol zona, tornem al Snapshot general
        if (other.CompareTag("ZonaPoble") || other.CompareTag("cave"))
        {
            generalSnapshot.TransitionTo(transitionTime);
            Debug.Log("Sortint de la zona: Tornant a música general");
        }
    }
}