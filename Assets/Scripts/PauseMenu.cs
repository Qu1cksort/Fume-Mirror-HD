using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;

    public void PauseGame()
    {
        Cursor.lockState = CursorLockMode.None;

        // Activar el panel de pausa
        pausePanel.SetActive(true);

        // Detener el tiempo en el juego
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        // Desactivar el panel de pausa
        pausePanel.SetActive(false);

        // Reanudar el tiempo en el juego
        Time.timeScale = 1f;
    }

    void Update()
    {
        // Comprobar si se presionó la tecla Esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Si el juego ya está pausado, reanudarlo
            if (Time.timeScale == 0)
            {
                ResumeGame();
            }
            // Si no, pausar el juego
            else
            {
                PauseGame();
            }
        }
    }
}
