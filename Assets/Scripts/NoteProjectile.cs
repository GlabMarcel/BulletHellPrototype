using UnityEngine;

public class NoteProjectile : MonoBehaviour
{
    public float projectileSpeed = 10f;
    public float lifetime = 5f; // Time after which the projectile will be destroyed
    public int damage = 1; // Amount of damage the projectile deals to enemies

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // Movement logic here, if any
    }

    // Optional: Destroy the projectile if it goes off-screen
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")) // Assuming enemies have the tag "Enemy"
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
            Destroy(gameObject); // Destroy the projectile
        }
    }
}
