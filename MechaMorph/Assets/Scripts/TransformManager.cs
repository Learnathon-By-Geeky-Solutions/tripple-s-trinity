using TrippleTrinity.MechaMorph.Ability;
using TrippleTrinity.MechaMorph.Control;
using TrippleTrinity.MechaMorph.Health;
using UnityEngine;

namespace TrippleTrinity.MechaMorph
{
    public class TransformManager : MonoBehaviour
    {
        [Header("Form Settings")]
        [SerializeField] private GameObject ballVisual;
        [SerializeField] private GameObject robotVisual;
        [SerializeField] private NewBallControllerWithDash ballController;
        [SerializeField] private NewRobotController robotController;

        [Header("Abilities")]
        [SerializeField] private AreaDamageAbility areaDamageAbility;

        [Header("Health System")]
        [SerializeField] private PlayerHealth playerHealth;  // Reference to PlayerHealth

        private bool _isBallForm = true;

        private void Start()
        {
            SetBallForm();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T)) 
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
            ballVisual.transform.position = robotVisual.transform.position;
            ballVisual.transform.rotation = robotVisual.transform.rotation;

            ballVisual.SetActive(true);  // Activate Ball form
            robotVisual.SetActive(false); // Deactivate Robot form

            if (ballController != null)
            {
                ballController.enabled = true;
                var method = ballController.GetType().GetMethod("OnTransformToBall");
                method?.Invoke(ballController, null);  // Call method if it exists (on transformation)
            }

            if (robotController != null)
            {
                robotController.enabled = false; // Disable Robot controller
            }

            _isBallForm = true; // Mark player in Ball form

            // Notify other systems
            areaDamageAbility?.SetRobotForm(false);  // Disable robot-specific abilities
            playerHealth?.SwitchForm(PlayerHealth.PlayerForm.Ball);  // Update PlayerHealth form to Ball
        }

        private void SetRobotForm()
        {
            robotVisual.transform.position = ballVisual.transform.position;
            robotVisual.transform.rotation = ballVisual.transform.rotation;

            ballVisual.SetActive(false); // Deactivate Ball form
            robotVisual.SetActive(true); // Activate Robot form

            if (ballController != null)
            {
                ballController.enabled = false; // Disable Ball controller
            }

            if (robotController != null)
            {
                robotController.enabled = true; // Enable Robot controller
            }

            _isBallForm = false; // Mark player in Robot form

            // Notify other systems
            areaDamageAbility?.SetRobotForm(true);  // Enable robot-specific abilities
            playerHealth?.SwitchForm(PlayerHealth.PlayerForm.Robot);  // Update PlayerHealth form to Robot
        }

        public bool IsBallForm()
        {
            return _isBallForm;
        }
    }
}
