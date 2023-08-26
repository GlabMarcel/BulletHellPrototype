using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Reference to the player
    public float smoothSpeed = 0.125f; // Speed of camera movement
    public Vector3 offset; // Offset from the player

    public float minX, maxX, minY, maxY;

    private float halfWidth;
    private float halfHeight;

    private void Start()
    {
        halfWidth = Camera.main.orthographicSize * Camera.main.aspect;
        halfHeight = Camera.main.orthographicSize;

        // Adjust the boundaries based on the camera's half-width and half-height
        minX += halfWidth;
        maxX -= halfWidth;
        minY += halfHeight;
        maxY -= halfHeight;
    }

    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Clamp the camera's position to the adjusted boundaries
        smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, minX, maxX);
        smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, minY, maxY); // Adjust this line to allow vertical movement
        smoothedPosition.z = transform.position.z; // Keep the camera's depth constant

        transform.position = smoothedPosition;
    }
}
