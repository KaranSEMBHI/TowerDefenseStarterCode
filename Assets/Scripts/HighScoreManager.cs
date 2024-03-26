using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    // Static variable voor de singleton instance.
    public static HighScoreManager Instance { get; private set; }

    // Publieke properties.
    public string PlayerName { get; set; }
    public bool GameIsWon { get; set; }

    // Zorg dat dit een singleton is.
    private void Awake()
    {
        // Als er al een instantie bestaat en het is niet deze, vernietig dan deze.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            // Zet deze als de singleton instance.
            Instance = this;
            // Optioneel: Als je wilt dat de manager blijft bestaan tussen scenes, ont-comment deze regel.
            // DontDestroyOnLoad(gameObject);
        }
    }

    // Hier kun je andere methoden voor je HighScoreManager toevoegen.
}
