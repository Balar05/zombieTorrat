using UnityEngine;
using UnityEngine.Audio;

public class GlobalOcclusion : MonoBehaviour
{
    public AudioMixer audioMixer;
    public string parameterName = "AmbientCutoff";

    public float clearFreq = 22000f;
    public float occludedFreq = 800f;
    public float transitionSpeed = 4f;

    public float rayDistance = 10f;
    public LayerMask detectionLayer; 

    private float targetFreq;
    private float currentFreq;

    void Start()
    {
        currentFreq = clearFreq;
        targetFreq = clearFreq;
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.up, out hit, rayDistance, detectionLayer))
        {
            targetFreq = occludedFreq;
            Debug.DrawRay(transform.position + Vector3.up, Vector3.up * rayDistance, Color.red);
        }
        else
        {
            targetFreq = clearFreq;
            Debug.DrawRay(transform.position + Vector3.up, Vector3.up * rayDistance, Color.green);
        }

        currentFreq = Mathf.Lerp(currentFreq, targetFreq, Time.deltaTime * transitionSpeed);
        audioMixer.SetFloat(parameterName, currentFreq);
    }
}