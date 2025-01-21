using Health;
using Ui_Scripts;
using UnityEngine;

namespace Ability
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

        [Header("Form Settings")]
        public bool isRobotForm; // Set to true when in robot form

        private AreaCooldownBar _cooldownBar; // Reference to the cooldown bar script

        private void Start()
        {
            _currentCooldown = 0f; // Start with an empty cooldown bar
            _cooldownBar = FindObjectOfType<AreaCooldownBar>(); // Automatically find the cooldown bar in the scene
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
            _currentCooldown = Mathf.Clamp(_currentCooldown + +_killValue, 0f, _maxCooldown);
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

            // Deal damage to all enemies within the radius
            Collider[] hits = Physics.OverlapSphere(transform.position, damageRadius);
            foreach (Collider hit in hits)
            {
                CharacterHealth targetHealth = hit.GetComponent<CharacterHealth>();
                if (targetHealth != null)
                {
                    targetHealth.TakeDamage(damageAmount);
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
    }
}
