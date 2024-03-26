using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public TowerMenu towerMenu;
    public TopMenu topMenu;

    public List<GameObject> Archers;
    public List<GameObject> Swords;
    public List<GameObject> Wizards;

    private ConstructionSite selectedSite;
    private int credits = 200;
    private int health = 10;
    private int currentWave = 0;

    private bool waveActive = false;
    private int enemiesInGameCounter = 0;
    public int totalWaves;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        credits = 200;
        health = 10;
        currentWave = 0;
        waveActive = false;
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (topMenu != null)
        {
            topMenu.SetCreditsLabel("Credits: " + credits);
            topMenu.SetGateHealthLabel("Gate Health: " + health);
            topMenu.SetWaveLabel("Wave: " + currentWave);
        }
        else
        {
            Debug.LogError("TopMenu reference not set in the GameManager.");
        }
    }

    // Veranderd om de Tower eigenschap te gebruiken.
    public void DestroyTower()
    {
        if (selectedSite != null && selectedSite.Tower != null) // Gebruik de Tower eigenschap in plaats van de tower variabele
        {
            // Mogelijk moet je ook credits teruggeven aan de speler, afhankelijk van je spelregels
            int refundAmount = GetCost(selectedSite.TowerType, selectedSite.Level, true); // true omdat het verkopen is
            AddCredits(refundAmount);

            selectedSite.RemoveTower(); // Deze methode bestaat al in je ConstructionSite class 
            UpdateUI(); // Update de gebruikersinterface om veranderingen te reflecteren
        }
        else
        {
            Debug.LogError("Geen toren geselecteerd om te vernietigen.");
        }
    }
    public int GetCost(Enums.TowerType type, Enums.SiteLevel level, bool selling = false)
    {
        // Placeholder logic, replace with your actual cost calculation
        int cost = 100; // Replace this with your cost logic
        switch (type)
        {
            case Enums.TowerType.Archer:
                cost = level == Enums.SiteLevel.Level1 ? 50 : level == Enums.SiteLevel.Level2 ? 75 : 100;
                break;
            case Enums.TowerType.Sword:
                // Define costs for Sword towers based on level
                break;
            case Enums.TowerType.Wizard:
                // Define costs for Wizard towers based on level
                break;
        }
        return selling ? cost / 2 : cost;
    }


    public void Build(Enums.TowerType type, Enums.SiteLevel level)
    {
        // Step 1: Check if a construction site is selected
        if (selectedSite == null)
        {
            Debug.LogError("No construction site selected. Cannot build tower.");
            return; // Stop the method here if there's no site selected
        }

        // Calculate the cost for the desired tower type and level
        int cost = GetCost(type, level);

        // Step 2: Check if there are enough credits to build the tower
        if (credits < cost)
        {
            Debug.LogError("Not enough credits to build.");
            return;
        }

        // Deduct the cost from the player's credits
        RemoveCredits(cost);

        // Determine the correct prefab based on the tower type and its level
        GameObject towerPrefab = null;
        int prefabIndex = (int)level - 1; // Adjust level to zero-based index

        switch (type)
        {
            case Enums.TowerType.Archer:
                towerPrefab = Archers[prefabIndex];
                break;
            case Enums.TowerType.Sword:
                towerPrefab = Swords[prefabIndex];
                break;
            case Enums.TowerType.Wizard:
                towerPrefab = Wizards[prefabIndex];
                break;
        }

        // Step 3: Verify the towerPrefab and instantiate the tower
        if (towerPrefab == null)
        {
            Debug.LogError("No tower prefab found for the selected type and level.");
            return;
        }

        GameObject tower = Instantiate(towerPrefab, selectedSite.WorldPosition, Quaternion.identity);

        // Configure the newly instantiated tower
        selectedSite.SetTower(tower, level, type);

        // Step 4: Handle final adjustments, such as UI updates
        if (towerMenu != null)
        {
            towerMenu.SetSite(null); // Assuming this hides the tower menu
        }
    }


    public void SelectSite(ConstructionSite site)
    {
        selectedSite = site;
        towerMenu?.SetSite(site);
    }
    public void StartWave()
    {
        enemiesInGameCounter = 0;
        currentWave++;
        topMenu.SetWaveLabel("Wave: " + currentWave);
        waveActive = true;
        EnemySpawner.Instance.StartWave(currentWave);
    }

    public void EndWave()
    {
        waveActive = false;
        topMenu.EnableWaveButton();
    }

    public void AttackGate()
    {
        health -= 1;
        UpdateUI();
    }

    public void AddCredits(int amount)
    {
        credits += amount;
        UpdateUI();
    }

    public void RemoveCredits(int amount)
    {
        credits -= amount;
        UpdateUI();
    }

    public void AddInGameEnemy()
    {
        enemiesInGameCounter++;
    }

    public void RemoveInGameEnemy()
    {
        enemiesInGameCounter--;

        if (!waveActive && enemiesInGameCounter <= 0)
        {
            if (currentWave == totalWaves)
            {
                HandleGameEnd();
            }
            else
            {
                topMenu.EnableWaveButton();
            }
        }
    }

    private void HandleGameEnd()
    {
        Debug.Log("Congratulations! You've defeated all waves!");
        // Implement your game end logic here
    }
}
