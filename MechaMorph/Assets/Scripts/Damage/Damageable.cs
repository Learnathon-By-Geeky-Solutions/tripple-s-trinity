using UnityEngine;
using System;
using TrippleTrinity.MechaMorph.Ui;
using TrippleTrinity.MechaMorph.Ability;

namespace TrippleTrinity.MechaMorph.Damage
{
    public class Damageable : MonoBehaviour
    {
        public enum PlayerForm { Ball, Robot }

        [SerializeField] private float maxHealth = 100f;
        [SerializeField] private int scoreValue = 10; // Points for enemy kill

        private float _currentHealth;
        private PlayerForm _currentForm = PlayerForm.Ball;
        private AreaDamageAbility _areaDamageAbility;

        public event Action OnDamageTaken;
        public event Action OnHealed;
        public event Action OnDeath;

        public float CurrentHealth => _currentHealth;
        public float MaxHealth => maxHealth;

        private void Start()
        {
            _currentHealth = maxHealth;
            _areaDamageAbility = FindObjectOfType<AreaDamageAbility>();
        }

        public void TakeDamage(float amount)
        {
            if (_currentForm == PlayerForm.Ball)
            {
                amount *= 0.5f; // Ball form takes reduced damage
            }

            _currentHealth -= amount;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth);

            OnDamageTaken?.Invoke();

            if (_currentHealth <= 0)
            {
                HandleDeath();
            }
        }

        private void HandleDeath()
        {
            OnDeath?.Invoke();

            // Register enemy kill for ability cooldown
            _areaDamageAbility?.RegisterEnemyKill();

            // Add score if enemy
            ScoreManager.Instance?.AddScore(scoreValue);

            Destroy(gameObject);
        }

        public void Heal(float amount)
        {
            _currentHealth += amount;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth);

            OnHealed?.Invoke();
        }
        
        

        public void SwitchForm(PlayerForm newForm)
        {
            _currentForm = newForm;
        }
    }
}
