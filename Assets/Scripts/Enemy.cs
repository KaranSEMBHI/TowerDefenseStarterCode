using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 1f;
    public float health = 10f;
    public int points = 5;
    public Enums.Path path { get; set; }
    public GameObject target { get; set; }
    private int pathIndex = 1;

    void Start()
    {
        //..
    }

    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, step);

        // Check if the enemy has reached its current target waypoint
        if (Vector2.Distance(transform.position, target.transform.position) < 0.1f)
        {
            // Get the next target waypoint
            target = EnemySpawner.Instance.RequestTarget(path, pathIndex);
            pathIndex++;

            // If there's no next target, the enemy has reached the end of the path
            if (target == null)
            {
                // Attack the gate before being destroyed
                GameManager.Instance.AttackGate();

                // Destroy the enemy game object
                Destroy(gameObject);
            }
        }
    }

    public void Damage(int damage)
    {
        // Verminder de gezondheidswaarde
        health -= damage;

        if (health <= 0)
        {
            GameManager.Instance.AddCredits(points);
            Destroy(gameObject);
        }
    }

}