using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;              // Target to follow (ball or robot)
    public Vector3 offset = new Vector3(0, 10, -10); // Offset from the target
    public float followSpeed = 5f;       // Speed of following
    public float zoomOutFactor = 1f;     // Zoom-out factor based on speed
    public float maxZoomOut = 15f;       // Maximum zoom-out distance
    public float minZoomOut = 5f;        // Minimum zoom-out distance

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null) return;

        // Calculate target position
        Vector3 targetPosition = target.position + offset;
        
        // Smoothly move the camera to the target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 1f / followSpeed);

        // Look at the target
        transform.LookAt(target.position);
    }
}