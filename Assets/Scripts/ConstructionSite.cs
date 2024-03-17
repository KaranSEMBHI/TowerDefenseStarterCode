using UnityEngine;

public enum SiteLevel
{
    Unbuilt,
    Level1,
    Level2,
    Level3
}

public class ConstructionSite
{
    public Vector3Int TilePosition { get; private set; }
    public Vector3 WorldPosition { get; private set; }
    public SiteLevel Level { get; private set; }
    public TowerType TowerType { get; private set; }
    private GameObject tower;

    public ConstructionSite(Vector3Int tilePosition, Vector3 worldPosition)
    {
        TilePosition = tilePosition;
        WorldPosition = worldPosition + new Vector3(0, 0.5f, 0);
        Level = SiteLevel.Unbuilt;
        tower = null;
    }

    public void SetTower(GameObject newTower, SiteLevel newLevel, TowerType newType)
    {
        if (tower != null)
        {
            Object.Destroy(tower);
        }

        tower = newTower;
        Level = newLevel;
        TowerType = newType;
    }
}