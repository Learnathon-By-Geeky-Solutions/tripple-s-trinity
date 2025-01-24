using TrippleTrinity.MechaMorph.Control;
using UnityEngine;

namespace TrippleTrinity.MechaMorph
{
    public class TransformManager : MonoBehaviour
    {
        [Header("Form Settings")]
        [SerializeField] private GameObject ballVisual;    // Child object for the ball visuals
        [SerializeField] private GameObject robotVisual;   // Child object for the robot visuals
        [SerializeField] private BallControllerWithDash ballController; // Ball movement script
        [SerializeField] private RobotController robotController; // Robot movement script

        [Header("Camera Settings")]
        [SerializeField] private CameraController cameraController; // Reference to the CameraController script

        private bool _isBallForm = true; // Track the current form

        private void Start()
        {
            SetBallForm(); // Start in ball form
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T)) // Switch form on 'T'
            {
                if (_isBallForm)
                {
                    SetRobotForm();
                }
                else
                {
                    SetBallForm();
                }
            }
        }

        private void SetBallForm()
        {
            // Transfer position and rotation to Ball Form
            ballVisual.transform.position = robotVisual.transform.position;
            ballVisual.transform.rotation = robotVisual.transform.rotation;

            ballVisual.SetActive(true);
            robotVisual.SetActive(false);
            ballController.enabled = true;
            robotController.enabled = false;

            // Call OnTransformToBall to reset dash-related variables
            if (ballController != null)
            {
                ballController.OnTransformToBall();
            }

            if (cameraController != null)
            {
                cameraController.Target = ballVisual.transform; // Set the camera to follow the ball
            }

            _isBallForm = true;
        }

        private void SetRobotForm()
        {
            // Transfer position and rotation to Robot Form
            robotVisual.transform.position = ballVisual.transform.position;
            robotVisual.transform.rotation = ballVisual.transform.rotation;

            ballVisual.SetActive(false);
            robotVisual.SetActive(true);
            ballController.enabled = false;
            robotController.enabled = true;

            if (cameraController != null)
            {
                cameraController.Target = robotVisual.transform; // Set the camera to follow the robot
            }

            _isBallForm = false;
        }

        public bool IsBallForm()
        {
            return _isBallForm;
        }
    }
}
