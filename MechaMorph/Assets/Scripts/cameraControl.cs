using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;      // Reference to the player (ball)
    public Vector3 offset = new Vector3(0, 10, -10); // Default offset from the player
    public float followSpeed = 5f; // Speed at which the camera follows the player
    public float zoomOutFactor = 1f; // Factor by which the camera zooms out based on speed
    public float maxZoomOut = 15f;  // Maximum distance the camera can zoom out
    public float minZoomOut = 5f;   // Minimum distance the camera can zoom in

    public  Rigidbody playerRigidbody;

    void Start()
    {
        // Ensure the player Rigidbody is assigned
        if (player != null)
            playerRigidbody = player.GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        if (player == null || playerRigidbody == null) return;

        // Get the player's speed
        float playerSpeed = playerRigidbody.velocity.magnitude;

        // Calculate dynamic zoom based on speed
        float zoomOutDistance = Mathf.Clamp(minZoomOut + playerSpeed * zoomOutFactor, minZoomOut, maxZoomOut);

        // Calculate the new camera position
        Vector3 targetPosition = player.position + offset.normalized * zoomOutDistance;

        // Smoothly move the camera to the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // Always look at the player
        transform.LookAt(player.position);
    }
}