using UnityEngine;
using UnityEngine.UIElements; // Gebruik deze namespace voor UI Toolkit.
using UnityEngine.SceneManagement;

public class IntroMenu : MonoBehaviour
{
    private Button playButtonIntro;
    private Button quitButtonIntro;
    private TextField nameFillerText;

    void Start()
    {
        // Verkrijg de root visual element van de UIDocument component.
        var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

        // Vind de UI elementen.
        playButtonIntro = rootVisualElement.Q<Button>("playButtonIntro");
        quitButtonIntro = rootVisualElement.Q<Button>("quitButtonIntro");
        nameFillerText = rootVisualElement.Q<TextField>("nameFillerText");

        // Disable de start knop bij het openen van het menu.
        playButtonIntro.SetEnabled(false);

        // Voeg de callbackfuncties toe aan de knoppen.
        playButtonIntro.clicked += OnStartButtonClicked;
        quitButtonIntro.clicked += OnQuitButtonClicked;

        // Voeg de callbackfunctie toe voor de TextField.
        nameFillerText.RegisterValueChangedCallback(evt =>
        {
            OnNameValueChanged(evt);
        });
    }

    void OnDestroy()
    {
        // Verwijder event listeners.
        playButtonIntro.clicked -= OnStartButtonClicked;
        quitButtonIntro.clicked -= OnQuitButtonClicked;
        // Verwijder de callback voor de TextField.
        if (nameFillerText != null)
        {
            nameFillerText.UnregisterValueChangedCallback(evt =>
            {
                OnNameValueChanged(evt);
            });
        }
    }

    private void OnStartButtonClicked()
    {
        // Laad de GameScene.
        SceneManager.LoadScene("GameScene");
    }

    private void OnQuitButtonClicked()
    {
        // Sluit de game af. Werkt niet in de Unity Editor.
        Application.Quit();
    }

    private void OnNameValueChanged(ChangeEvent<string> evt)
    {
        // Start knop wordt actief als de lengte van de naam minstens 3 tekens is.
        playButtonIntro.SetEnabled(evt.newValue.Length >= 3);

        // Update de PlayerName property in de HighScoreManager singleton.
        if (HighScoreManager.Instance != null)
        {
            HighScoreManager.Instance.PlayerName = evt.newValue;
        }
    }
}
