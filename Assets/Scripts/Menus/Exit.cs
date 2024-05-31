using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void ContinueGame()
    {
        // Aquí puedes agregar la lógica para continuar el juego (cargar el último guardado, etc.)
        //Debug.Log("Continue Game");
    }

    public void NewGame()
    {
        // Aquí puedes cargar la escena del juego nuevo
        //SceneManager.LoadScene("NombreDeTuEscenaDeJuego");
    }

    public void Credits()
    {
        // Aquí puedes cargar la escena de créditos
        //SceneManager.LoadScene("NombreDeTuEscenaDeCreditos");
    }

    public void ExitGame()
    {
        // Si estamos en el editor de Unity
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        // Si es una aplicación construida
        Application.Quit();
        #endif
        // Salir del juego
        //Application.Quit();
        Debug.Log("Exit Game");
    }
}
