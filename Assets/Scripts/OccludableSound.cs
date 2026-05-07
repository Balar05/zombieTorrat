using UnityEngine;
using UnityEngine.Audio;

public class OccludableSound : MonoBehaviour
{
    [Header("Referències")]
    public Transform player;          // El teu Cavaller
    public AudioMixer audioMixer;     // El teu Mixer principal
    public string exposedParam;       // El nom del paràmetre que has exposat (ex: "LlacCutoff")

    [Header("Configuració d'Oclusió")]
    public float clearFreq = 22000f;  // Freqüència quan no hi ha obstacles (so nítid)
    public float occludedFreq = 800f; // Freqüència quan hi ha obstacles (so sord)
    public float transitionSpeed = 5f; // Velocitat del canvi (més alt = més ràpid)

    [Header("Configuració del Raycast")]
    public LayerMask obstacleLayers;  // Capes que bloquegen el so (ex: Default, Ground)

    private float currentFreq;
    private float targetFreq;

    void Start()
    {
        // Comencem amb el so clar
        currentFreq = clearFreq;
        if (player == null) player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        // 1. Calculem la direcció i distància cap al jugador
        Vector3 directionToPlayer = player.position - transform.position;
        float distance = directionToPlayer.magnitude;

        // 2. Lancem el Raycast (Criteri 10)
        RaycastHit hit;
        // El raig va des de la font de so cap al jugador
        if (Physics.Raycast(transform.position, directionToPlayer, out hit, distance, obstacleLayers))
        {
            // Si el que ha xocat el raig NO és el Player, hi ha un obstacle
            if (!hit.collider.CompareTag("Player"))
            {
                targetFreq = occludedFreq;
                Debug.DrawRay(transform.position, directionToPlayer.normalized * hit.distance, Color.red);
            }
            else
            {
                targetFreq = clearFreq;
                Debug.DrawRay(transform.position, directionToPlayer, Color.green);
            }
        }
        else
        {
            // Si el raig ni tan sols xoca amb res, el so arriba net
            targetFreq = clearFreq;
        }

        // 3. Suavitzem el canvi de freqüència (Interpolació)
        currentFreq = Mathf.Lerp(currentFreq, targetFreq, Time.deltaTime * transitionSpeed);

        // 4. Enviem el valor al Mixer
        audioMixer.SetFloat(exposedParam, currentFreq);
    }
}