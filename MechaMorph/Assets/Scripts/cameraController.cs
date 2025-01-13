using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;                  // Target to follow (robot)
    public Vector3 offset = new Vector3(0, 10, -10); // Fixed offset from the target
    public float followSpeed = 5f;            // Speed of following
    public float rotationSmoothTime = 0.1f;   // Time to smooth rotation

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null) return;

        // Calculate the desired position based on the offset
        Vector3 targetPosition = target.position + offset;

        // Smoothly move the camera to the target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 1f / followSpeed);

        // Smoothly rotate the camera to look at the target
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSmoothTime);
    }
}