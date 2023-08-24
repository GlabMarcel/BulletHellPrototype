using UnityEngine;

public class MouseControlledMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed at which the player moves
    public float deadZoneRadius = 1f; // Radius of the dead zone around the player

    private Rigidbody2D rb; // Reference to the Rigidbody2D component

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
    }

    private void FixedUpdate()
    {
        // Get the mouse position in world coordinates
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the direction to the mouse position
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;

        if (Vector2.Distance(transform.position, mousePosition) > deadZoneRadius)
        {
            Vector2 targetPosition = Vector2.Lerp(rb.position, rb.position + direction * moveSpeed, Time.deltaTime);
            rb.MovePosition(targetPosition);
        }

    }
}
