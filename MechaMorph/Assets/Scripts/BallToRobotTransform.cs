using UnityEngine;

public class PrefabTransformation : MonoBehaviour
{
    public GameObject ballPrefab;  // Reference to the ball prefab
    public GameObject robotPrefab; // Reference to the robot prefab

    public Transform spawnPoint;   // The position where the transformed object should appear

    private GameObject currentForm; // The currently active form (ball or robot)
    private bool isBall = true;     // Track whether the current form is a ball

    void Start()
    {
        // Spawn the ball form at the start
        currentForm = Instantiate(ballPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    void Update()
    {
        // Check for the transformation key press
        if (Input.GetKeyDown(KeyCode.T))
        {
            TransformBetweenForms();
        }
    }

    void TransformBetweenForms()
    {
        // Save the current position and rotation
        Vector3 position = currentForm.transform.position;
        Quaternion rotation = currentForm.transform.rotation;

        // Destroy the current form
        Destroy(currentForm);

        // Switch to the other form
        if (isBall)
        {
            // Transform to robot
            currentForm = Instantiate(robotPrefab, position, rotation);
        }
        else
        {
            // Transform to ball
            currentForm = Instantiate(ballPrefab, position, rotation);
        }

        // Toggle the state
        isBall = !isBall;
    }
}