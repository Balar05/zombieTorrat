using UnityEngine;

public class PlayerSoundEvents : MonoBehaviour
{
    //Inicialitzem un AudioSource i els AudioClips.
    public AudioSource audioSource;
    public AudioClip attackSound;

    public AudioClip[] passosHerba; 
    public AudioClip[] passosAigua; 
    public AudioClip[] passosFusta; 

   //Creem una variable per saber a quina superfície estem i la inicialitzem.

    public string superficieActual = "Herba";

    //Funció per reproduïr el so d'atac.
    public void PlayAttackSound()
    {
        if (audioSource != null && attackSound != null)
        {
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.PlayOneShot(attackSound);
        }
    }

    //Funció per reproduïr els passos.
    public void PlayFootstepSound()
    {
        AudioClip clipToPlay = null;

        //Depenent de la superfície, reproduïm un so o un altre.
        //Seleccionem un so aleatori de la llista de cada tipus de so.
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