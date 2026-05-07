using UnityEngine;

public class PlayerSoundEvents : MonoBehaviour
{
    [Header("So d'Atac")]
    public AudioSource audioSource;
    public AudioClip attackSound;

    [Header("Sons de Passos")]
    public AudioClip[] passosHerba; 
    public AudioClip[] passosAigua; 
    public AudioClip[] passosFusta; 

   
    public string superficieActual = "Herba";

    
    public void PlayAttackSound()
    {
        if (audioSource != null && attackSound != null)
        {
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.PlayOneShot(attackSound);
        }
    }
    public void PlayFootstepSound()
    {
        AudioClip clipToPlay = null;

        
        if (superficieActual == "Herba" && passosHerba.Length > 0)
            clipToPlay = passosHerba[Random.Range(0, passosHerba.Length)];
        else if (superficieActual == "Aigua" && passosAigua.Length > 0)
            clipToPlay = passosAigua[Random.Range(0, passosAigua.Length)];
        else if (superficieActual == "Fusta" && passosFusta.Length > 0)
            clipToPlay = passosFusta[Random.Range(0, passosFusta.Length)];
        
        if (clipToPlay != null)
        {
            audioSource.pitch = Random.Range(0.85f, 1.15f); 
            audioSource.PlayOneShot(clipToPlay, 0.2f); 
        }
    }
}