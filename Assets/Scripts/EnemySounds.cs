using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySounds : MonoBehaviour
{
    public AudioSource audioSource; // Referencia al AudioSource
    public AudioClip roarClip; // Referencia al clip de audio del rugido

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Este método se llamará cuando se active el evento de animación RoarSound
    public void RoarSound()
    {
        // Reproduce el clip de audio del rugido
        audioSource.clip = roarClip;
        audioSource.Play();
    }
}
