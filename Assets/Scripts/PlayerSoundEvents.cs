using UnityEngine;

public class PlayerSoundEvents : MonoBehaviour
{
    [Header("Configuració de So")]
    public AudioSource audioSource;
    public AudioClip attackSound;

    // Aquesta funció és "pública" perquè l'animació la pugui veure
    public void PlayAttackSound()
    {
        if (audioSource != null && attackSound != null)
        {
            // Variem una mica el to (Pitch) perquè cada cop soni diferent (Criteri 11)
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.PlayOneShot(attackSound);
        }
    }
}