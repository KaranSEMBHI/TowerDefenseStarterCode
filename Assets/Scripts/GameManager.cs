using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    // Referentie naar het TowerMenu GameObject
    public GameObject TowerMenuGameObject;

    // Referentie naar het TowerMenu script
    private TowerMenu _towerMenuScript;

    // Lijsten voor de verschillende types torens
    public List<GameObject> Archers;
    public List<GameObject> Swords;
    public List<GameObject> Wizards;

    // Geselecteerde bouwplaats
    private ConstructionSite selectedSite;

    // Methode om de singleton instantie te initialiseren
    private void Awake()
    {
        // Controleer of er al een instantie van GameManager is
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

        // Haal het TowerMenu-script op van het bijbehorende GameObject
        _towerMenuScript = TowerMenuGameObject.GetComponent<TowerMenu>();
    }

    // Voorbeeldmethode om te communiceren met het TowerMenu
    public void ExampleMethod()
    {
        // Roep een methode aan in het TowerMenu-script
        _towerMenuScript.NotifyGameManagerOfMenuUpdate();
    }

    // Functie om een bouwplaats te selecteren
    public void SelectSite(ConstructionSite site)
    {
        // Onthoud de geselecteerde site
        selectedSite = site;

        // Geef de geselecteerde site door aan het TowerMenu
        _towerMenuScript.SetSite(selectedSite);
    }
}
