using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform target;
    public float speed;
    public int damage;

    void Start()
    {
        // Rotate the projectile towards the target
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        }
    }

    void Update()
    {
        // When target is null, it no longer exists and this  
        // object has to be removed
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // Move the projectile towards the target
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // finally, check if the distance between this object and 

        // the target is smaller than 0.2. If so, destroy this object. 

        if (Vector2.Distance(transform.position, target.position) < 0.2f)

        {
            target.GetComponent<Enemy>().Damage(damage);
            Destroy(gameObject);

        }
    }
}