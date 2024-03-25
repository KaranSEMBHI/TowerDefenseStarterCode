using UnityEngine;
using UnityEngine.UIElements;

public class TopMenu : MonoBehaviour
{
    private Label waveLabel;
    private Label creditsLabel;
    private Label gateHealthLabel;
    private Button playButton;

    private UIDocument uiDocument;

    private VisualElement root;


    void Start()
    {
        uiDocument = GetComponent<UIDocument>();
        if (uiDocument == null)
        {
            Debug.LogError("UIDocument component not found on the GameObject.");
            return;
        }

        root = uiDocument.rootVisualElement;

        waveLabel = root.Q<Label>("Wave");
        if (waveLabel == null)
        {
            Debug.LogError("Wave label not found in the UI Document.");
        }

        creditsLabel = root.Q<Label>("Credits");
        if (creditsLabel == null)
        {
            Debug.LogError("Credits label not found in the UI Document.");
        }

        gateHealthLabel = root.Q<Label>("GateHealth");
        if (gateHealthLabel == null)
        {
            Debug.LogError("Gate Health label not found in the UI Document.");
        }

        playButton = root.Q<Button>("Play");
        if (playButton == null)
        {
            Debug.LogError("Play button not found in the UI Document.");
        }
        else
        {
            playButton.clicked += OnPlayButtonClicked;
        }
    }

    private void OnPlayButtonClicked()
    {
        GameManager.Instance.StartWave();
        playButton.SetEnabled(false); // Schakel de playButton uit
    }

    public void EnableWaveButton()
    {
        if (playButton != null)
        {
            playButton.SetEnabled(true);
        }
    }


    public void SetWaveLabel(string text)
    {
        if (waveLabel != null)
        {
            waveLabel.text = text;
        }
    }

    public void SetCreditsLabel(string text)
    {
        if (creditsLabel != null)
        {
            creditsLabel.text = text;
        }
    }

    public void SetGateHealthLabel(string text)
    {
        if (gateHealthLabel != null)
        {
            gateHealthLabel.text = text;
        }
    }

    private void OnDestroy()
    {
        if (playButton != null)
        {
            playButton.clicked -= OnPlayButtonClicked;
        }
    }
}