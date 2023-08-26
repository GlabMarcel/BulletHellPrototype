using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 5f;

    private void Start()
    {
        Destroy(gameObject, lifetime); // Destroy the bullet after a certain time to prevent memory issues
    }

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime); // Move the bullet in the direction it's facing
    }
}
