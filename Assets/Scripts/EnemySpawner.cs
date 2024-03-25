using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    public List<GameObject> Path1;
    public List<GameObject> Path2;
    public List<GameObject> Enemies;

    private int currentWave = 0;

    private int ufoCounter = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        InvokeRepeating("SpawnTester", 1f, 1f);
    }

    private void SpawnEnemy(int type, Enums.Path path)
    {
        List<GameObject> selectedPath = path == Enums.Path.Path1 ? Path1 : Path2;

        if (selectedPath.Count < 2)
        {
            Debug.LogError("Path doesn't have enough waypoints.");
            return;
        }

        var newEnemy = Instantiate(Enemies[type], selectedPath[0].transform.position, selectedPath[0].transform.rotation);
        var script = newEnemy.GetComponent<Enemy>();
        script.path = path;
        script.target = selectedPath[1];
    }

    private void SpawnTester()
    {
        SpawnEnemy(0, Enums.Path.Path1);
    }

    public GameObject RequestTarget(Enums.Path path, int index)
    {
        List<GameObject> selectedPath = path == Enums.Path.Path1 ? Path1 : Path2;

        if (index < selectedPath.Count)
            return selectedPath[index];
        else
            return null;
    }
    public void StartNextWave()
    {
        // Increment the current wave
        currentWave++;
        // Call a method to spawn the enemies for this wave
        SpawnEnemiesForWave(currentWave);
    }

    // Define the SpawnEnemiesForWave method
    private void SpawnEnemiesForWave(int waveNumber)
    {
        // Logic to spawn enemies based on the wave number
        // This is just a placeholder and needs actual implementation
        Debug.Log($"Spawning enemies for wave {waveNumber}.");
    }
    public void StartWave(int number)
    {
        ufoCounter = 0;
        switch (number)
        {
            case 1:
                InvokeRepeating("StartWave1", 1f, 1.5f);
                break;
                // Voeg hier meer cases toe voor aanvullende waves
        }
    }

    public void StartWave1()
    {
        ufoCounter++;
        if (ufoCounter % 6 <= 1) return;
        if (ufoCounter < 30)
        {
            SpawnEnemy(0, Enums.Path.Path1);
        }
        else
        {
            SpawnEnemy(1, Enums.Path.Path1); // Het laatste vijand zal niveau 2 zijn
        }
        if (ufoCounter > 30)
        {
            CancelInvoke("StartWave1");
            GameManager.Instance.EndWave(); // Laat GameManager weten dat de wave is geëindigd.
        }
    }



}