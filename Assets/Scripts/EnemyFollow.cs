using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed = 3.0f; // Speed at which the enemy moves towards the player
    private Transform player; // Reference to the player's transform

    private void Start()
    {
        // Find the player object by its tag (make sure your player object has the tag "Player")
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // Calculate the direction to the player
        Vector2 direction = (player.position - transform.position).normalized;

        // Move the enemy towards the player
        transform.position = (Vector2)transform.position + direction * speed * Time.deltaTime;
    }

    // This function can be called when the enemy is defeated (e.g., by a weapon or other game mechanics)
    public void Die()
    {
        // Here you can add any effects or sounds related to the enemy's death
        Destroy(gameObject); // Destroy the enemy object
    }
}
