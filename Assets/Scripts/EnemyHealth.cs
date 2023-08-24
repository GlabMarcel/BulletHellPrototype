using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 1;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Add any death effects or sounds here
        Destroy(gameObject);
    }
}
