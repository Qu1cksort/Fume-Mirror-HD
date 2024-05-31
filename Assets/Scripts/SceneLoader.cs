using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private AsyncOperation asyncOperation;

    // Método para cargar una escena
    public void LoadScene(string sceneName)
    {
        asyncOperation = SceneManager.LoadSceneAsync(sceneName);
    }

    // Método para descargar una escena
    public void UnloadScene(string sceneName)
    {
        StartCoroutine(UnloadSceneWhenReady(sceneName));
    }

    private IEnumerator UnloadSceneWhenReady(string sceneName)
    {
        while (!asyncOperation.isDone)
        {
            yield return null;
        }

        SceneManager.UnloadSceneAsync(sceneName);
    }
}
