using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhistleController : MonoBehaviour
{
    public Animator animator; // Referencia al Animator
    public AudioSource audioSource; // Referencia al AudioSource
    public AudioClip whistleClip; // Referencia al clip de audio del silbato

   
    // Este m�todo se llamar� cuando se active el evento de animaci�n WhistleSound
    public void WhistleSound()
    {
        // Reproduce el clip de audio del silbato
        audioSource.clip = whistleClip;
        audioSource.Play();
    }
}
