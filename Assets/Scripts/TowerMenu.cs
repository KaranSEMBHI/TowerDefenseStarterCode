using UnityEngine;
using UnityEngine.UIElements;
using System;

public class TowerMenu : MonoBehaviour
{
    public event Action<ConstructionSite> SiteSelected;
    public event Action MenuUpdated;

    private Button archerButton;
    private Button swordButton;
    private Button wizardButton;
    private Button updateButton;
    private Button destroyButton;

    private VisualElement root;

    private ConstructionSite selectedSite;
    private GameManager gameManager;

    // Singleton instance
    private static TowerMenu _instance;
    public static TowerMenu Instance => _instance;

    // Methode om de singleton instantie te initialiseren
    private void Awake()
    {
        // Controleer of er al een instantie van TowerMenu is
        if (_instance != null && _instance != this)
        {
            // Als dat zo is, vernietig deze instantie
            Destroy(gameObject);
            return;
        }

        // Als er geen instantie is, maak deze instantie de singleton
        _instance = this;

        // Zorg ervoor dat dit GameObject niet wordt vernietigd bij het laden van een nieuwe scene
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        archerButton = root.Q<Button>("archer-button");
        swordButton = root.Q<Button>("sword-button");
        wizardButton = root.Q<Button>("wizard-button");
        updateButton = root.Q<Button>("button-upgrade");
        destroyButton = root.Q<Button>("button-destroy");

        if (archerButton != null)
        {
            archerButton.clicked += OnArcherButtonClicked;
        }

        if (swordButton != null)
        {
            swordButton.clicked += OnSwordButtonClicked;
        }

        if (wizardButton != null)
        {
            wizardButton.clicked += OnWizardButtonClicked;
        }

        if (updateButton != null)
        {
            updateButton.clicked += OnUpdateButtonClicked;
        }

        if (destroyButton != null)
        {
            destroyButton.clicked += OnDestroyButtonClicked;
        }

        root.visible = false;
    }

    private void OnArcherButtonClicked()
    {
        // Implementeer de logica voor de boogschutter knop
    }

    private void OnSwordButtonClicked()
    {
        // Implementeer de logica voor de zwaard knop
    }

    private void OnWizardButtonClicked()
    {
        // Implementeer de logica voor de tovenaar knop
    }

    private void OnUpdateButtonClicked()
    {
        // Implementeer de logica voor de update knop
    }

    private void OnDestroyButtonClicked()
    {
        // Implementeer de logica voor de vernietig knop
    }

    private void OnDestroy()
    {
        if (archerButton != null)
        {
            archerButton.clicked -= OnArcherButtonClicked;
        }

        if (swordButton != null)
        {
            swordButton.clicked -= OnSwordButtonClicked;
        }

        if (wizardButton != null)
        {
            wizardButton.clicked -= OnWizardButtonClicked;
        }

        if (updateButton != null)
        {
            updateButton.clicked -= OnUpdateButtonClicked;
        }

        if (destroyButton != null)
        {
            destroyButton.clicked -= OnArcherButtonClicked;
        }
    }

    // Functie om het menu te evalueren op basis van de geselecteerde bouwplaats
    public void EvaluateMenu()
    {
        if (selectedSite == null)
            return;

        // Implementeer de logica om het menu te evalueren

        MenuUpdated?.Invoke(); // Event aanroepen om de GameManager te informeren over de menu-update
    }

    // Functie om een bouwplaats in te stellen en het menu te updaten
    public void SetSite(ConstructionSite site)
    {
        selectedSite = site;

        if (selectedSite == null)
        {
            root.visible = false;
            return;
        }
        root.visible = true;
        EvaluateMenu();

        SiteSelected?.Invoke(selectedSite); // Event aanroepen om de GameManager te informeren over de geselecteerde bouwplaats
    }

    // Functie om een referentie naar de GameManager in te stellen
    public void SetGameManager(GameManager manager)
    {
        gameManager = manager;
    }

    // Voorbeeldmethode om de GameManager te informeren over een menu-update
    public void NotifyGameManagerOfMenuUpdate()
    {
        Debug.Log("TowerMenu informs GameManager of menu update.");
    }
}
