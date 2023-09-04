using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    //public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public float zoomSpeed = 10f;
    public float minZoom = 5f;
    public float maxZoom = 15f;

    private Camera myCamera;

    private void Start()
    {
        myCamera = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        // Basic Camera Follow
        Vector3 desiredPosition = target.position + offset;
        // Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = desiredPosition;

        // Dynamic Zoom (based on player's speed)
        float scrollData = Input.GetAxis("Mouse ScrollWheel");
        myCamera.orthographicSize -= scrollData * zoomSpeed;
        myCamera.orthographicSize = Mathf.Clamp(myCamera.orthographicSize, minZoom, maxZoom);
    }
}
