using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LanguageCarousel : MonoBehaviour
{
    public TextMeshProUGUI languageText;  // Referencia al TextMeshProUGUI que muestra el idioma
    public Button previousButton;         // Botón para ir al idioma anterior
    public Button nextButton;             // Botón para ir al siguiente idioma

    private string[] languages = { "Español", "English"};
    private int currentIndex = 0;

    void Start()
    {
        // Configura los listeners de los botones
        previousButton.onClick.AddListener(ShowPreviousLanguage);
        nextButton.onClick.AddListener(ShowNextLanguage);

        // Muestra el idioma inicial
        UpdateLanguageText();
    }

    void ShowPreviousLanguage()
    {
        // Muestra el idioma anterior en la lista
        currentIndex = (currentIndex - 1 + languages.Length) % languages.Length;
        UpdateLanguageText();
    }

    void ShowNextLanguage()
    {
        // Muestra el siguiente idioma en la lista
        currentIndex = (currentIndex + 1) % languages.Length;
        UpdateLanguageText();
    }

    void UpdateLanguageText()
    {
        // Actualiza el texto del TextMeshProUGUI
        languageText.text = languages[currentIndex];
    }
}
