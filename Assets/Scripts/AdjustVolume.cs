using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustVolume : MonoBehaviour
{
    public Transform enemy; // Referencia al enemigo
    public AudioSource audioSource; // Referencia al AudioSource
    public float minVolume = 0.2f; // Volumen m�nimo
    public float triggerDistance = 50f; // Distancia para empezar a aumentar el volumen

    void Update()
    {
        // Calcular la distancia entre el jugador y el enemigo
        float distance = Vector3.Distance(transform.position, enemy.position);

        // Ajustar el volumen
        if (distance < triggerDistance)
        {
            // Ajustar el volumen proporcionalmente a la distancia dentro del rango de activaci�n
            audioSource.volume = minVolume + (1 - minVolume) * (1 - Mathf.Clamp01(distance / triggerDistance));
        }
        else
        {
            // Mantener el volumen en el nivel m�nimo si el enemigo est� fuera del rango de activaci�n
            audioSource.volume = minVolume;
        }
    }
}
