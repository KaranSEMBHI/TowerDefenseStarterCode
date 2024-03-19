using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 1f;
    public float health = 10f;
    public int points = 1;
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

        if (Vector2.Distance(transform.position, target.transform.position) < 0.1f)
        {
            target = EnemySpawner.Instance.RequestTarget(path, pathIndex);
            pathIndex++;

            if (target == null)
            {
                Destroy(gameObject);
            }
        }
    }
    public void Damage(int damage)
    {
        // Verminder de gezondheidswaarde
        health -= damage;

        // Controleer of de gezondheid kleiner is dan of gelijk is aan nul
        if (health <= 0)
        {
            // Vernietig het spelobject
            Destroy(gameObject);
        }
    }

}