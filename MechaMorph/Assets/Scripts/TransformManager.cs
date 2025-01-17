using UnityEngine;

public class TransformManager : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;    // Prefab for the ball form
    [SerializeField] GameObject robotPrefab;   // Prefab for the robot form
    [SerializeField] CameraController cameraController; // Reference to the CameraController script

    private GameObject _currentForm;  // The currently active form
    private bool _isBallForm = true;  // Tracks if the active form is the ball

    void Start()
    {
        SpawnBallForm(); // Start with the ball form
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) // Press 'T' to transform
        {
            if (_isBallForm)
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

        if (_currentForm != null)
        {
            spawnPosition = _currentForm.transform.position;
            spawnRotation = _currentForm.transform.rotation;
            Destroy(_currentForm); // Destroy the previous form
        }

        _currentForm = Instantiate(ballPrefab, spawnPosition, spawnRotation);

        if (cameraController != null)
        {
            cameraController.target = _currentForm.transform; // Set the camera to follow the ball
        }

        _isBallForm = true;
    }

    private void SpawnRobotForm()
    {
        Vector3 spawnPosition = transform.position; 
        Quaternion spawnRotation = Quaternion.identity;

        if (_currentForm != null)
        {
            spawnPosition = _currentForm.transform.position;
            spawnRotation = _currentForm.transform.rotation;
            Destroy(_currentForm); // Destroy the previous form
        }

        _currentForm = Instantiate(robotPrefab, spawnPosition, spawnRotation);

        if (cameraController != null)
        {
            cameraController.target = _currentForm.transform; // Set the camera to follow the robot
        }

        _isBallForm = false;
    }
}
