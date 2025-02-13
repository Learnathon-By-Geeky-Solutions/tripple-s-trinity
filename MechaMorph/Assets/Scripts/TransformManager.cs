using TrippleTrinity.MechaMorph.Ability;
using TrippleTrinity.MechaMorph.Control;

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
        [SerializeField] private Damage.Damageable damageable;

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

            ballVisual.SetActive(true);
            robotVisual.SetActive(false);

            if (ballController != null)
            {
                ballController.enabled = true;
                var method = ballController.GetType().GetMethod("OnTransformToBall");
                method?.Invoke(ballController, null);
            }

            if (robotController != null)
            {
                robotController.enabled = false;
            }

            _isBallForm = true;

            // Notify other systems
            areaDamageAbility?.SetRobotForm(false);
            damageable?.SwitchForm(Damage.Damageable.PlayerForm.Ball);
        }

        private void SetRobotForm()
        {
            robotVisual.transform.position = ballVisual.transform.position;
            robotVisual.transform.rotation = ballVisual.transform.rotation;

            ballVisual.SetActive(false);
            robotVisual.SetActive(true);

            if (ballController != null)
            {
                ballController.enabled = false;
            }

            if (robotController != null)
            {
                robotController.enabled = true;
            }

            _isBallForm = false;

            // Notify other systems
            areaDamageAbility?.SetRobotForm(true);
            damageable?.SwitchForm(Damage.Damageable.PlayerForm.Robot);
        }

        public bool IsBallForm()
        {
            return _isBallForm;
        }
    }
}

