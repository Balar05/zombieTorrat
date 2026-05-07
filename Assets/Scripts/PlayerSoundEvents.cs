using UnityEngine;

public class PlayerSoundEvents : MonoBehaviour
{
    [Header("So d'Atac")]
    public AudioSource audioSource;
    public AudioClip attackSound;

    [Header("Sons de Passos (Criteri 5)")]
    public AudioClip[] passosHerba; // Arrossega aquí 2 o 3 sons d'herba
    public AudioClip[] passosAigua; // Arrossega aquí 2 o 3 sons de pedra
    public AudioClip[] passosFusta; // Arrossega aquí 2 o 3 sons de fusta

    // Aquesta variable la canviarem mitjançant Triggers
    public string superficieActual = "Herba";

    // --- FUNCIÓ D'ATAC (La que ja teníem) ---
    public void PlayAttackSound()
    {
        if (audioSource != null && attackSound != null)
        {
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.PlayOneShot(attackSound);
        }
    }

    // --- NOVA FUNCIÓ PELS PASSOS ---
    public void PlayFootstepSound()
    {
        AudioClip clipToPlay = null;

        // Triem l'array de sons segons on estem trepitjant
        if (superficieActual == "Herba" && passosHerba.Length > 0)
            clipToPlay = passosHerba[Random.Range(0, passosHerba.Length)];
        else if (superficieActual == "Aigua" && passosAigua.Length > 0)
            clipToPlay = passosAigua[Random.Range(0, passosAigua.Length)];
        else if (superficieActual == "Fusta" && passosFusta.Length > 0)
            clipToPlay = passosFusta[Random.Range(0, passosFusta.Length)];

        // Si hem trobat un so, el fem sonar
        if (clipToPlay != null)
        {
            audioSource.pitch = Random.Range(0.85f, 1.15f); // Variem el to perquè soni natural
            audioSource.PlayOneShot(clipToPlay, 0.2f); // Volum al 0.4 perquè els passos no tapin la música
        }
    }
}