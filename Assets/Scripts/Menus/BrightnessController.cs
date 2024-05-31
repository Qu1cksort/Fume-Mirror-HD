using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class BrightnessController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public TextMeshProUGUI statusText;  // Referencia al TextMeshProUGUI que muestra el estatus
    public Button decreaseButton;       // Botón para disminuir el brillo
    public Button increaseButton;       // Botón para aumentar el brillo

    private int maxBrightnessLevel = 30; // 30 repeticiones del símbolo "|" equivalen al 100% de brillo
    private int currentBrightnessLevel = 15; // Inicialmente establecemos el brillo al 50%
    public bool IsIncreasing { get; set; }
    public bool IsDecreasing { get; set; }

    private float delay = 0.5f; // Retraso entre cada cambio de brillo

    void Start()
    {
        // Muestra el brillo inicial
        UpdateBrightnessText();
    }

    void Update()
    {
        if (IsIncreasing)
        {
            StopAllCoroutines();
            StartCoroutine(IncreaseBrightness());
        }
        else if (IsDecreasing)
        {
            StopAllCoroutines();
            StartCoroutine(DecreaseBrightness());
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerPress == decreaseButton.gameObject)
        {
            IsDecreasing = true;
        }
        else if (eventData.pointerPress == increaseButton.gameObject)
        {
            IsIncreasing = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsIncreasing = false;
        IsDecreasing = false;
        StopAllCoroutines();
    }

    IEnumerator DecreaseBrightness()
    {
        // Disminuye el brillo si no está en el mínimo
        if (currentBrightnessLevel > 0)
        {
            currentBrightnessLevel--;
            // Aquí puedes agregar el código para cambiar el brillo de la pantalla
            UpdateBrightnessText();
        }
        yield return new WaitForSeconds(delay);
    }

    IEnumerator IncreaseBrightness()
    {
        // Aumenta el brillo si no está en el máximo
        if (currentBrightnessLevel < maxBrightnessLevel)
        {
            currentBrightnessLevel++;
            // Aquí puedes agregar el código para cambiar el brillo de la pantalla
            UpdateBrightnessText();
        }
        yield return new WaitForSeconds(delay);
    }

    void UpdateBrightnessText()
    {
        // Actualiza el texto del TextMeshProUGUI para mostrar el nivel de brillo actual
        statusText.text = new string('|', currentBrightnessLevel);
    }
}
