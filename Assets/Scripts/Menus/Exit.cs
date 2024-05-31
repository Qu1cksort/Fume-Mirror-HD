using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void ContinueGame()
    {
        // Aqu� puedes agregar la l�gica para continuar el juego (cargar el �ltimo guardado, etc.)
        //Debug.Log("Continue Game");
    }

    public void NewGame()
    {
        // Aqu� puedes cargar la escena del juego nuevo
        //SceneManager.LoadScene("NombreDeTuEscenaDeJuego");
    }

    public void Credits()
    {
        // Aqu� puedes cargar la escena de cr�ditos
        //SceneManager.LoadScene("NombreDeTuEscenaDeCreditos");
    }

    public void ExitGame()
    {
        // Si estamos en el editor de Unity
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        // Si es una aplicaci�n construida
        Application.Quit();
        #endif
        // Salir del juego
        //Application.Quit();
        Debug.Log("Exit Game");
    }
}
