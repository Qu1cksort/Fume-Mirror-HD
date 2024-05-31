using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public GameObject deathPanel;

    public void ShowDeathMenu()
    {
        Cursor.lockState = CursorLockMode.None;

        // Activar el panel de muerte
        deathPanel.SetActive(true);

    }

    public void HideDeathMenu()
    {
        // Desactivar el panel de muerte
        deathPanel.SetActive(false);

    }

}
