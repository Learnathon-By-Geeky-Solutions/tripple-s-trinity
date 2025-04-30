using UnityEngine;
using TrippleTrinity.MechaMorph.Damage;
using TrippleTrinity.MechaMorph.InputHandling;
using TrippleTrinity.MechaMorph.MyAsset.Scripts.Ui;

namespace TrippleTrinity.MechaMorph.Ability
{
    public class AreaDamageAbility : MonoBehaviour
    {
        [Header("Ability Settings")]
        private float _maxCooldown = 100f;
        private float _currentCooldown;

        [Header("Cooldown Contributions")]
        private readonly float _tokenValue = 34f;
        private readonly float _killValue = 15f;

        [Header("Area Damage Settings")]
        [SerializeField] private float damageRadius = 5f;
        [SerializeField] private float damageAmount = 50f;
        [SerializeField] private int maxTargets = 5;

        [Header("Form Settings")]
        [SerializeField] private bool isRobotForm;

        [SerializeField] private GameObject areaDamageEffect;

        private AreaCooldownBar _cooldownBar;
        private Collider[] _hitResults;

        private void Awake()
        {
            _currentCooldown = 0f;
            _cooldownBar = FindObjectOfType<AreaCooldownBar>();
            _hitResults = new Collider[maxTargets];
        }

        private void Update()
        {
            if (!isRobotForm) return;
            if (!InputHandler.Instance.IsAbilityActivated()) return;
            if (!IsAbilityReady()) return;

            ActivateAbility();
        }

        public void ApplyUpgrades(int upgradeLevel)
        {
            _maxCooldown -= 5 * upgradeLevel;
            damageAmount += 10 * upgradeLevel;
            damageRadius += 0.05f * upgradeLevel;
        }

        public void CollectToken()
        {
            _currentCooldown = Mathf.Clamp(_currentCooldown + _tokenValue, 0f, _maxCooldown);
            UpdateCooldownUI();
        }

        public void RegisterEnemyKill()
        {
            _currentCooldown = Mathf.Clamp(_currentCooldown + _killValue, 0f, _maxCooldown);
            UpdateCooldownUI();
        }

        private bool IsAbilityReady()
        {
            return _currentCooldown >= _maxCooldown;
        }

        private void ActivateAbility()
        {
            Debug.Log("Area Damage Ability Activated!");
            _currentCooldown = 0f;
            UpdateCooldownUI();
            SpawnEffect();
            ApplyAreaDamage();
        }

        private void SpawnEffect()
        {
            if (areaDamageEffect != null)
            {
                Instantiate(areaDamageEffect, transform.position, Quaternion.identity);
            }
        }

        private void ApplyAreaDamage()
        {
            int hitCount = Physics.OverlapSphereNonAlloc(transform.position, damageRadius, _hitResults);
            int targetsHit = 0;

            for (int i = 0; i < hitCount && targetsHit < maxTargets; i++)
            {
                if (_hitResults[i].CompareTag("Player")) continue;

                Damageable damageable = _hitResults[i].GetComponent<Damageable>();
                if (damageable != null)
                {
                    damageable.TakeDamage(damageAmount);
                    targetsHit++;
                }
            }

            Debug.Log($"Area Damage hit {targetsHit} enemies.");
        }

        private void UpdateCooldownUI()
        {
            _cooldownBar?.SetFillAmount(_currentCooldown / _maxCooldown);
        }

        public void SetRobotForm(bool isRobot)
        {
            isRobotForm = isRobot;
        }
    }
}
