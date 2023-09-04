using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public float speed = 3.0f;
    private Transform player;

    private void Start()
    {
        // Find the player object by its tag
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position = (Vector2)transform.position + direction * speed * Time.deltaTime;
    }
}
