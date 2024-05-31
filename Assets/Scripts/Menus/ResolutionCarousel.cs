using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResolutionCarousel : MonoBehaviour
{
    public TextMeshProUGUI statusText;  // Referencia al TextMeshProUGUI que muestra el estatus
    public Button previousButton;         // Bot�n para ir al estatus anterior
    public Button nextButton;             // Bot�n para ir al siguiente estatus
    public Button applyButton;            // Bot�n para aplicar la resoluci�n seleccionada

    private Resolution[] status;
    private int currentIndex = 0;

    void Start()
    {
        // Obtiene las resoluciones disponibles
        status = Screen.resolutions;

        // Encuentra la resoluci�n actual en la lista de resoluciones disponibles
        Resolution currentResolution = Screen.currentResolution;
        for (int i = 0; i < status.Length; i++)
        {
            if (status[i].width == currentResolution.width && status[i].height == currentResolution.height)
            {
                currentIndex = i;
                break;
            }
        }

        // Configura los listeners de los botones
        previousButton.onClick.AddListener(ShowPreviousStatus);
        nextButton.onClick.AddListener(ShowNextStatus);
        applyButton.onClick.AddListener(ApplySelectedResolution);

        // Muestra el estatus inicial
        UpdateStatusText();
    }

    void ShowPreviousStatus()
    {
        // Muestra la resoluci�n anterior en la lista
        currentIndex = (currentIndex - 1 + status.Length) % status.Length;
        UpdateStatusText();
    }

    void ShowNextStatus()
    {
        // Muestra la siguiente resoluci�n en la lista
        currentIndex = (currentIndex + 1) % status.Length;
        UpdateStatusText();
    }

    void ApplySelectedResolution()
    {
        // Cambia la resoluci�n del juego a la seleccionada
        Screen.SetResolution(status[currentIndex].width, status[currentIndex].height, Screen.fullScreen);
        Debug.Log(status[currentIndex].ToString());
    }

    void UpdateStatusText()
    {
        // Actualiza el texto del TextMeshProUGUI
        statusText.text = status[currentIndex].ToString();        
    }
}
