using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ModeCarousel : MonoBehaviour
{
    public TextMeshProUGUI statusText;  // Referencia al TextMeshProUGUI que muestra el estatus
    public Button previousButton;         // Botón para ir al estatus anterior
    public Button nextButton;             // Botón para ir al siguiente estatus
    public Button applyButton;            // Botón para aplicar el modo de pantalla seleccionado

    private string[] status = { "Ventana", "Pantalla Completa" };
    private int currentIndex = 0;

    void Start()
    {
        // Configura los listeners de los botones
        previousButton.onClick.AddListener(ShowPreviousStatus);
        nextButton.onClick.AddListener(ShowNextStatus);
        applyButton.onClick.AddListener(ApplySelectedMode);

        // Establece el estatus inicial basado en el modo de pantalla actual
        currentIndex = Screen.fullScreen ? 1 : 0;

        // Muestra el estatus inicial
        UpdateStatusText();
    }

    void ShowPreviousStatus()
    {
        // Muestra el status anterior en la lista
        currentIndex = (currentIndex - 1 + status.Length) % status.Length;
        UpdateStatusText();
    }

    void ShowNextStatus()
    {
        // Muestra el siguiente status en la lista
        currentIndex = (currentIndex + 1) % status.Length;
        UpdateStatusText();
    }

    void ApplySelectedMode()
    {
        // Cambia el modo de pantalla a la seleccionada
        Screen.fullScreen = (currentIndex == 1);
        Debug.Log(status[currentIndex]);
    }

    void UpdateStatusText()
    {
        // Actualiza el texto del TextMeshProUGUI
        statusText.text = status[currentIndex];
    }
}
