using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutoCarousel : MonoBehaviour
{
    public TextMeshProUGUI statusText;  // Referencia al TextMeshProUGUI que muestra el estatus
    public Button previousButton;         // Botón para ir al estatus anterior
    public Button nextButton;             // Botón para ir al siguiente estatus

    private string[] status = { "Activado", "Desactivado" };
    private int currentIndex = 0;

    void Start()
    {
        // Configura los listeners de los botones
        previousButton.onClick.AddListener(ShowPreviousStatus);
        nextButton.onClick.AddListener(ShowNextStatus);

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

    void UpdateStatusText()
    {
        // Actualiza el texto del TextMeshProUGUI
        statusText.text = status[currentIndex];
    }
}
