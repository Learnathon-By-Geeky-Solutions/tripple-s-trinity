using TrippleTrinity.MechaMorph.Ui;
using UnityEngine;
using UnityEngine.InputSystem; 
using TrippleTrinity.MechaMorph.Damage;

namespace TrippleTrinity.MechaMorph.Ability
{
    public class AreaDamageAbility : MonoBehaviour
    {
        [Header("Ability Settings")]
        private readonly float _maxCooldown = 100f;  // Full cooldown bar value
        private float _currentCooldown;

        [Header("Cooldown Contributions")]
        private readonly float _tokenValue = 34f;   // Adjusted Token contribution
        private readonly float _killValue = 15f;    // Adjusted Kill contribution (4 Kills = Full Cooldown)

        [Header("Area Damage Settings")]
        [SerializeField] private float damageRadius = 5f;  // Radius of the area damage
        [SerializeField] private float damageAmount = 50f; // Damage dealt by the ability
        [SerializeField] private int maxTargets = 5; // Maximum number of targets that can be hit

        [Header("Form Settings")]
        [SerializeField] private bool isRobotForm; // Determines if player is in robot mode

        private AreaCooldownBar _cooldownBar; // Reference to the cooldown bar script
        private Collider[] _hitResults; // Pre-allocated array for storing hit results

        // Input Action Reference
        [SerializeField] private InputActionAsset inputActions; 
        private InputAction _activateAbilityAction;

        private void Awake()
        {
            _currentCooldown = 0f; // Start with an empty cooldown bar
            _cooldownBar = FindObjectOfType<AreaCooldownBar>(); // Automatically find the cooldown bar in the scene
            _hitResults = new Collider[maxTargets]; // Pre-allocate the array for hit results

            // Check if inputActions is assigned
            if (inputActions == null)
            {
                Debug.LogError("Input Actions Asset not assigned!");
                return;
            }

            // Get the 'ActivateAbility' action from input action map
            _activateAbilityAction = inputActions.FindActionMap("Abilities")?.FindAction("ActivateAbility");
            if (_activateAbilityAction == null)
            {
                Debug.LogError("ActivateAbility action not found in input action asset!");
            }
        }

        private void OnEnable()
        {
            _activateAbilityAction?.Enable();
        }

        private void OnDisable()
        {
            _activateAbilityAction?.Disable();
        }

        private void Update()
        {
            if (isRobotForm && _activateAbilityAction.triggered && IsAbilityReady())
            {
                ActivateAbility();
            }
        }

        public void CollectToken()
        {
            // Fixed cooldown increment logic
            _currentCooldown = Mathf.Clamp(_currentCooldown + _tokenValue, 0f, _maxCooldown);
            UpdateCooldownUI();
            Debug.Log("Token collected! Cooldown: " + _currentCooldown);
        }

        public void RegisterEnemyKill()
        {
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
            _currentCooldown = 0f; // Reset cooldown after use
            UpdateCooldownUI();
        
        
            int hitCount = Physics.OverlapSphereNonAlloc(transform.position, damageRadius, _hitResults);
            int targetsHit = 0;

            for (int i = 0; i < hitCount; i++)
            {
                if (targetsHit >= maxTargets) break; //Stop once max targets reached

                // Ensure Player is NOT damaged
                if (_hitResults[i].CompareTag("Player")) 
                    continue;
        
                Damageable damageable = _hitResults[i].GetComponent<Damageable>();
                if (damageable != null)
                {
                    damageable.TakeDamage(damageAmount);
                    targetsHit++; // Track how many enemies are hit
                }
            }

            Debug.Log($"Area Damage hit {targetsHit} enemies.");
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
