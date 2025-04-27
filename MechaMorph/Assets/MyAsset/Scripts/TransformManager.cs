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
        [SerializeField] private PlayerHealth playerHealth;

        private bool _isBallForm = true;

        private void Start() => SwitchToBallForm();

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                if (_isBallForm) SwitchToRobotForm();
                else SwitchToBallForm();
            }
        }

        private void SwitchToBallForm()
        {
            CopyTransform(robotVisual.transform, ballVisual.transform);
            ToggleVisuals(ballVisual, robotVisual);
            ToggleControllers(ballController, robotController);

            InvokeBallTransformCallback();
            _isBallForm = true;

            NotifySystems(isRobot: false);
        }

        private void SwitchToRobotForm()
        {
            CopyTransform(ballVisual.transform, robotVisual.transform);
            ToggleVisuals(robotVisual, ballVisual);
            ToggleControllers(robotController, ballController);

            _isBallForm = false;

            NotifySystems(isRobot: true);
        }

        private void CopyTransform(Transform from, Transform to)
        {
            to.position = from.position;
            to.rotation = from.rotation;
        }

        private void ToggleVisuals(GameObject enableObj, GameObject disableObj)
        {
            enableObj.SetActive(true);
            disableObj.SetActive(false);
        }

        private void ToggleControllers(MonoBehaviour enableCtrl, MonoBehaviour disableCtrl)
        {
            if (disableCtrl != null) disableCtrl.enabled = false;
            if (enableCtrl != null) enableCtrl.enabled = true;
        }

        private void InvokeBallTransformCallback()
        {
            var method = ballController?.GetType().GetMethod("OnTransformToBall");
            method?.Invoke(ballController, null);
        }

        private void NotifySystems(bool isRobot)
        {
            areaDamageAbility?.SetRobotForm(isRobot);
            playerHealth?.SwitchForm(isRobot ? PlayerHealth.PlayerForm.Robot : PlayerHealth.PlayerForm.Ball);
        }

        public bool IsBallForm() => _isBallForm;
    }
}
