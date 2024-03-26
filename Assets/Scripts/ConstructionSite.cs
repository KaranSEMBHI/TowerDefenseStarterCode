using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionSite
{
    public Vector3Int TilePosition { get; private set; }
    public Vector3 WorldPosition { get; private set; }
    public Enums.SiteLevel Level { get; private set; }
    public Enums.TowerType TowerType { get; private set; }

    private GameObject tower; // Dit is de enige declaratie die je nodig hebt.

    // Een publieke eigenschap om de toren veilig te benaderen.
    public GameObject Tower
    {
        get { return tower; }
        set { tower = value; }
    }

    // Constructor voor de ConstructionSite.
    public ConstructionSite(Vector3Int tilePosition, Vector3 worldPosition)
    {
        TilePosition = tilePosition;
        // Pas de Y waarde aan zoals gespecificeerd.
        WorldPosition = new Vector3(worldPosition.x, worldPosition.y + 0.5f, worldPosition.z);
        tower = null; // Stel de toren in op null.
    }

    // Methode om een toren te plaatsen.
    public void SetTower(GameObject newTower, Enums.SiteLevel level, Enums.TowerType type)
    {
        // Controleer of er al een toren aanwezig is.
        if (this.tower != null)
        {
            // Verwijder het huidige toren GameObject.
            GameObject.Destroy(this.tower);
        }

        // Update de toren naar de nieuwe toren.
        this.tower = newTower;
        Level = level;
        TowerType = type;

        // Stel de positie van de nieuwe toren in.
        if (this.tower != null)
        {
            this.tower.transform.position = WorldPosition;
        }
    }

    // Methode om een toren te verwijderen.
    public void RemoveTower()
    {
        if (this.tower != null)
        {
            GameObject.Destroy(this.tower);
            this.tower = null; // Verwijder de referentie naar de toren.
        }

        // Reset de eigenschappen van de bouwplaats.
        Level = Enums.SiteLevel.Unbuilt;
        TowerType = default; // Gebruik 'default' om de standaardwaarde voor de enum in te stellen.
    }
}
