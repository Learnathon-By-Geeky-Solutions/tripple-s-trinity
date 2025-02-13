using UnityEngine;
using System;

namespace TrippleTrinity.MechaMorph.Damage
{
    public class Damageable : MonoBehaviour
    {
        public enum PlayerForm { Ball, Robot }

        [SerializeField] private float maxHealth = 100f;
        private float _currentHealth;
        private PlayerForm _currentForm = PlayerForm.Ball;

        public event Action OnDamageTaken; // Event for taking damage
        public event Action OnHealed; // Event for healing
        public event Action OnDeath; // Event for death

        public float CurrentHealth => _currentHealth;
        public float MaxHealth => maxHealth;

        private void Start()
        {
            _currentHealth = maxHealth;
        }

        public void TakeDamage(float amount)
        {
            if (_currentForm == PlayerForm.Ball)
            {
                amount *= 0.5f; // Ball form takes half damage
            }

            _currentHealth -= amount;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth);

            OnDamageTaken?.Invoke(); // Trigger event when damaged

            if (_currentHealth <= 0)
            {
                OnDeath?.Invoke(); // Trigger death event
            }
        }

        public void Heal(float amount)
        {
            _currentHealth += amount;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth);

            OnHealed?.Invoke(); // Trigger heal event
        }

        public void SwitchForm(PlayerForm newForm)
        {
            _currentForm = newForm;
        }
    }
}