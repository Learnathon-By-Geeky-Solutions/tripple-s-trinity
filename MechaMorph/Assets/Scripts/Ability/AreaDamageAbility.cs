using TrippleTrinity.MechaMorph.Weapons;
using TrippleTrinity.MechaMorph.Ui;
using UnityEngine;
using UnityEngine.InputSystem; // Import the new Input System namespace

namespace TrippleTrinity.MechaMorph.Ability
{
    public class AreaDamageAbility : MonoBehaviour
    {
        [Header("Ability Settings")]
        private readonly float _maxCooldown = 100f;  // Full cooldown bar value
        private float _currentCooldown;

        [Header("Cooldown Contributions")]
        private readonly float _tokenValue = 34f;   // Cooldown % from a token
        private readonly float _killValue = 15f;    // Cooldown % from a kill

        [Header("Area Damage Settings")]
        [SerializeField] private float damageRadius = 5f;  // Radius of the area damage
        [SerializeField] private float damageAmount = 50f; // Damage dealt by the ability
        [SerializeField] private int maxTargets = 10; // Maximum number of targets that can be hit

        [Header("Form Settings")]
        [SerializeField] private bool isRobotForm; // Backing field for the property

        private AreaCooldownBar _cooldownBar; // Reference to the cooldown bar script
        private Collider[] _hitResults; // Pre-allocated array for storing hit results

        // Reference to your Input Action Asset
        [SerializeField] private InputActionAsset inputActions; // Reference to the InputActionAsset
        private InputAction _activateAbilityAction;

        private void Awake()
        {
            _currentCooldown = 0f; // Start with an empty cooldown bar
            _cooldownBar = FindObjectOfType<AreaCooldownBar>(); // Automatically find the cooldown bar in the scene
            _hitResults = new Collider[maxTargets]; // Pre-allocate the array for hit results

            // Check if _inputActions is assigned, else initialize manually (only if not assigned)
            if (inputActions == null)
            {
                Debug.LogError("RobotInputActions Asset not assigned!");
                return;
            }

            // Get the 'ActivateAbility' action from your input action map
            _activateAbilityAction = inputActions.FindActionMap("Abilities")?.FindAction("ActivateAbility");
            if (_activateAbilityAction == null)
            {
                Debug.LogError("ActivateAbility action not found in input action asset!");
            }
        }

        private void OnEnable()
        {
            // Enable the action when the script is enabled
            _activateAbilityAction?.Enable();
        }

        private void OnDisable()
        {
            // Disable the action when the script is disabled
            _activateAbilityAction?.Disable();
        }

        private void Update()
        {
            // Check for ability activation only in robot form and if the ability is ready
            if (isRobotForm && _activateAbilityAction.triggered && IsAbilityReady())
            {
                ActivateAbility();
            }
        }

        public void CollectToken()
        {
            // Add cooldown value from a token
            _currentCooldown = Mathf.Clamp(_currentCooldown + _tokenValue, 0f, _maxCooldown);
            UpdateCooldownUI();
            Debug.Log("Token collected! Cooldown: " + _currentCooldown);
        }

        public void RegisterEnemyKill()
        {
            // Add cooldown value from an enemy kill
            _currentCooldown = Mathf.Clamp(_currentCooldown + _killValue, 0f, _maxCooldown);
            UpdateCooldownUI();
            Debug.Log("Enemy killed! Cooldown: " + _currentCooldown);
        }

        private bool IsAbilityReady()
        {
            return _currentCooldown >= _maxCooldown;
        }

        private void ActivateAbility()
        {
            Debug.Log("Area Damage Ability Activated!");
            _currentCooldown = 0f; // Reset cooldown
            UpdateCooldownUI();

            // Use Physics.OverlapSphereNonAlloc for efficiency
            int hitCount = Physics.OverlapSphereNonAlloc(transform.position, damageRadius, _hitResults);
            for (int i = 0; i < hitCount; i++)
            {
                TakeDamage targetHealth = _hitResults[i].GetComponent<TakeDamage>();
                if (targetHealth != null)
                {
                    targetHealth.Damage(damageAmount);
                }
            }
        }

        private void UpdateCooldownUI()
        {
            if (_cooldownBar != null)
            {
                _cooldownBar.SetFillAmount(_currentCooldown / _maxCooldown);
            }
        }

        public void SetRobotForm(bool isRobot)
        {
            isRobotForm = isRobot;
        }
    }
}
