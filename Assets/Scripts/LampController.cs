using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampController : MonoBehaviour
{
    public Light lampLight;
    public float maxIntensity = 10000f;
    public float minIntensity = 0.2f;
    public float flickerSpeed = 0.05f;

    private bool isFlickering = false;

    void Start()
    {
        if (lampLight == null)
        {
            lampLight = GetComponentInChildren<Light>();
        }
        InvokeRepeating("TryStartFlickering", 0f, 5f); // Intenta iniciar el parpadeo cada 5 segundos
    }

    void TryStartFlickering()
    {
        if (!isFlickering && Random.value < 0.05f) // 10% de probabilidad
        {
            StartCoroutine(FlickerLight());
        }
    }

    IEnumerator FlickerLight()
    {
        isFlickering = true;
        float flickerDuration = 5f; // Duración del parpadeo
        float endTime = Time.time + flickerDuration;

        while (Time.time < endTime)
        {
            lampLight.intensity = Random.Range(minIntensity, maxIntensity);
            yield return new WaitForSeconds(flickerSpeed);
        }

        isFlickering = false;
    }
}
