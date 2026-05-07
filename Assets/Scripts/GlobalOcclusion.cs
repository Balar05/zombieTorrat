using UnityEngine;
using UnityEngine.Audio;

public class GlobalOcclusion : MonoBehaviour
{
    public AudioMixer audioMixer;
    public string parameterName = "AmbientCutoff";

    [Header("Configuració de Filtre")]
    public float clearFreq = 22000f;
    public float occludedFreq = 800f;
    public float transitionSpeed = 4f;

    [Header("Detecció")]
    public float rayDistance = 10f;
    public LayerMask detectionLayer; // Capa de sostres/roques

    private float targetFreq;
    private float currentFreq;

    void Start()
    {
        currentFreq = clearFreq;
        targetFreq = clearFreq;
    }

    void Update()
    {
        // Llancem un raig cap amunt per veure si estem sota un sostre o roca (oclusió global)
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.up, out hit, rayDistance, detectionLayer))
        {
            // Estem sota cobert: el so ambient s'apaga
            targetFreq = occludedFreq;
            Debug.DrawRay(transform.position + Vector3.up, Vector3.up * rayDistance, Color.red);
        }
        else
        {
            // Estem a l'aire lliure: so clar
            targetFreq = clearFreq;
            Debug.DrawRay(transform.position + Vector3.up, Vector3.up * rayDistance, Color.green);
        }

        // Apliquem el canvi suau al Mixer
        currentFreq = Mathf.Lerp(currentFreq, targetFreq, Time.deltaTime * transitionSpeed);
        audioMixer.SetFloat(parameterName, currentFreq);
    }
}