using TrippleTrinity.MechaMorph.Ability;
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

        private bool _isBallForm = true; // Track the current form

        [Header("Abilities")]
        [SerializeField] private AreaDamageAbility areaDamageAbility;

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
            // Switch to ball visuals
            ballVisual.transform.position = robotVisual.transform.position;
            ballVisual.transform.rotation = robotVisual.transform.rotation;

            ballVisual.SetActive(true);
            robotVisual.SetActive(false);
            ballController.enabled = true;
            ballController.OnTransformToBall(); // Notify BallController

            robotController.enabled = false;

            _isBallForm = true;

            // Notify the AreaDamageAbility
            if (areaDamageAbility != null)
            {
                areaDamageAbility.SetRobotForm(false);
            }
        }

        private void SetRobotForm()
        {
            // Switch to robot visuals
            robotVisual.transform.position = ballVisual.transform.position;
            robotVisual.transform.rotation = ballVisual.transform.rotation;

            ballVisual.SetActive(false);
            robotVisual.SetActive(true);
            ballController.enabled = false;
            robotController.enabled = true;

            _isBallForm = false;

            // Notify the AreaDamageAbility
            if (areaDamageAbility != null)
            {
                areaDamageAbility.SetRobotForm(true);
            }
        }

        public bool IsBallForm()
        {
            return _isBallForm;
        }
    }
}

