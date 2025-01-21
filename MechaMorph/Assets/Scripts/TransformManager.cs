using UnityEngine;

public class TransformManager : MonoBehaviour
{
    public GameObject ballPrefab;    // Prefab for the ball form
    public GameObject robotPrefab;   // Prefab for the robot form
    public CameraController cameraController; // Reference to the CameraController script
    private  GameObject currentForm;  // The currently active form
    private bool isBallForm = true;  // Tracks if the active form is the ball

    void Start()
    {
        SpawnBallForm(); // Start with the ball form
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) // Press 'T' to transform
        {
            if (isBallForm)
            {
                SpawnRobotForm();
              
            }
            else
            {
                SpawnBallForm();
                

            }
        }
    }

    private void SpawnBallForm()
    {
        Vector3 spawnPosition = transform.position; 
        Quaternion spawnRotation = Quaternion.identity;

        if (currentForm != null)
        {
            spawnPosition = currentForm.transform.position;
            spawnRotation = currentForm.transform.rotation;
            Destroy(currentForm); // Destroy the previous form
        }

        currentForm = Instantiate(ballPrefab, spawnPosition, spawnRotation);
       

        if (cameraController != null)
        {
            cameraController.target = currentForm.transform; // Set the camera to follow the ball
        }

        isBallForm = true;
    }

    private void SpawnRobotForm()
    {
        Vector3 spawnPosition = transform.position; 
        Quaternion spawnRotation = Quaternion.identity;

        if (currentForm != null)
        {
            spawnPosition = currentForm.transform.position;
            spawnRotation = currentForm.transform.rotation;
            Destroy(currentForm); // Destroy the previous form
        }

        currentForm = Instantiate(robotPrefab, spawnPosition, spawnRotation);
       

        if (cameraController != null)
        {
            cameraController.target = currentForm.transform; // Set the camera to follow the robot
        }

        isBallForm = false;
    }
}
