using TrippleTrinity.MechaMorph.Weapons;
using TrippleTrinity.MechaMorph.Ui_Scripts;
using UnityEngine;

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

        private void Start()
        {
            _currentCooldown = 0f; // Start with an empty cooldown bar
            _cooldownBar = FindObjectOfType<AreaCooldownBar>(); // Automatically find the cooldown bar in the scene
            _hitResults = new Collider[maxTargets]; // Pre-allocate the array for hit results
            UpdateCooldownUI();
        }

        private void Update()
        {
            // Check for ability activation only in robot form
            if (isRobotForm && Input.GetKeyDown(KeyCode.E) && IsAbilityReady())
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
