using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 1;
    private WaveManager waveManager;

    private void Start()
    {
        waveManager = FindObjectOfType<WaveManager>();
    }

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
        waveManager.EnemyDied();

        // Add any death effects or sounds here
        Destroy(gameObject);
    }
}
