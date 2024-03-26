using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;

[System.Serializable]
public class HighScore
{
    public string Name;
    public int Score;
}

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager Instance { get; private set; }
    public List<HighScore> HighScores = new List<HighScore>();

    public string PlayerName { get; set; }
    public bool GameIsWon { get; set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void AddHighScore(int score)
    {
        HighScores.Add(new HighScore { Name = PlayerName, Score = score });
        HighScores = HighScores.OrderByDescending(hs => hs.Score).ToList();
        if (HighScores.Count > 5)
        {
            HighScores.RemoveAt(HighScores.Count - 1);
        }
        SaveHighScores();
    }

    void Start()
    {
        LoadHighScores();
    }

    private void SaveHighScores()
    {
        string json = JsonUtility.ToJson(new { HighScores = HighScores }, true);
        File.WriteAllText(Application.persistentDataPath + "/highscores.json", json);
    }

    private void LoadHighScores()
    {
        string path = Application.persistentDataPath + "/highscores.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            HighScores = JsonUtility.FromJson<List<HighScore>>(json);
        }
    }
}
