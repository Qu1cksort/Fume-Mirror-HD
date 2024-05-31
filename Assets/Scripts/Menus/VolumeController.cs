using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;


public class VolumeController : MonoBehaviour
{
    public TextMeshProUGUI statusText;  // Referencia al TextMeshProUGUI que muestra el estatus
    public Slider volumeSlider;         // Referencia al Slider para controlar el volumen
    public AudioMixer audioMixer;       // Referencia al AudioMixer que controla el volumen del audio

    void Start()
    {
        // Inicializa el volumen del Slider
        float currentVolumeLevel;
        audioMixer.GetFloat("Volume", out currentVolumeLevel);
        volumeSlider.value = currentVolumeLevel;
        UpdateVolumeText(currentVolumeLevel);

        // Suscribirse al evento OnValueChanged del Slider
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
        UpdateVolumeText(volume);
    }

    void UpdateVolumeText(float volume)
    {
        // Actualiza el texto del TextMeshProUGUI para mostrar el nivel de volumen actual
        int volumePercentage = Mathf.RoundToInt((volume + 80) * 100 / 80);
        statusText.text = volumePercentage.ToString() + "%";
    }
}
