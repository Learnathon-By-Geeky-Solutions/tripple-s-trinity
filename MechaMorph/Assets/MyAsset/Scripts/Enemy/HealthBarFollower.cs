using UnityEngine;

public class HealthBarFollower : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0, 50f, 0); // adjust offset

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 worldPos = target.position + offset;
            Vector3 screenPos = mainCamera.WorldToScreenPoint(worldPos);

            // Check if the target is in front of the camera
            if (screenPos.z > 0)
            {
                transform.position = screenPos;
            }
        }
    }
}