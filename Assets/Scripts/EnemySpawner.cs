using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    public List<GameObject> Path1;
    public List<GameObject> Path2;
    public List<GameObject> Enemies;

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

    private void SpawnEnemy(int type, Path path)
    {
        List<GameObject> selectedPath = path == Path.Path1 ? Path1 : Path2;

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
        SpawnEnemy(0, Path.Path1);
    }

    public GameObject RequestTarget(Path path, int index)
    {
        List<GameObject> selectedPath = path == Path.Path1 ? Path1 : Path2;

        if (index < selectedPath.Count)
            return selectedPath[index];
        else
            return null;
    }
}