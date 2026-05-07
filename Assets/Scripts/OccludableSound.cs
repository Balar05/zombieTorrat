using UnityEngine;
using UnityEngine.Audio;

public class OccludableSound : MonoBehaviour
{
    [Header("Referències")]
    public Transform player;          
    public AudioMixer audioMixer;     
    public string exposedParam;       

    [Header("Configuració d'Oclusió")]
    public float clearFreq = 22000f;  
    public float occludedFreq = 800f; 
    public float transitionSpeed = 5f; 

    [Header("Configuració del Raycast")]
    public LayerMask obstacleLayers;  

    private float currentFreq;
    private float targetFreq;

    void Start()
    { 
        currentFreq = clearFreq;
        if (player == null) player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        if (player == null) return; 
        Vector3 directionToPlayer = player.position - transform.position;
        float distance = directionToPlayer.magnitude;

        RaycastHit hit;
        
        if (Physics.Raycast(transform.position, directionToPlayer, out hit, distance, obstacleLayers))
        {
            
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
            targetFreq = clearFreq;
        }
        currentFreq = Mathf.Lerp(currentFreq, targetFreq, Time.deltaTime * transitionSpeed);

        audioMixer.SetFloat(exposedParam, currentFreq);
    }
}