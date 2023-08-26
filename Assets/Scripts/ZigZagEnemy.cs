using UnityEngine;

public class ZigZagEnemy : MonoBehaviour
{
    public float speed = 5f; // Forward movement speed
    public float zigzagSpeed = 5f; // Sideways movement speed
    public float zigzagFrequency = 2f; // How often the enemy changes direction

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        Vector2 direction = (player.position - transform.position).normalized;

        // Calculate the zigzag offset
        float xOffset = Mathf.Sin(Time.time * zigzagFrequency) * zigzagSpeed;

        // Calculate the new position
        Vector2 forwardMovement = direction * speed * Time.deltaTime;
        Vector2 zigzagMovement = new Vector2(direction.y, -direction.x) * xOffset * Time.deltaTime; // Perpendicular to the direction

        transform.position = (Vector2)transform.position + forwardMovement + zigzagMovement;
    }
}
