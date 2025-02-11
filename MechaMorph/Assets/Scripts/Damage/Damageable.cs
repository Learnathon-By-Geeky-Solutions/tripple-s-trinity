using UnityEngine;
using TrippleTrinity.MechaMorph.Ui;

namespace TrippleTrinity.MechaMorph.Damage
{
    public class Damageable : MonoBehaviour
    {
        public enum PlayerForm { Ball, Robot }

        [SerializeField] private float maxHealth = 100f;
        private float _currentHealth;
        private PlayerForm _currentForm = PlayerForm.Ball; // Start as Ball

        private HealthBar _healthBar;

        public float CurrentHealth => _currentHealth;
        public float MaxHealth => maxHealth;

        private void Start()
        {
            _currentHealth = maxHealth;
            _healthBar = FindObjectOfType<HealthBar>(); // Auto-assign HealthBar
        }

        public void TakeDamage(float amount)
        {
            // If in Ball form, take half damage
            if (_currentForm == PlayerForm.Ball)
            {
                amount *= 0.5f;
            }

            _currentHealth -= amount;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth);

            _healthBar?.UpdateHealthUI(); // Update UI
        }

        public void Heal(float amount)
        {
            _currentHealth += amount;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth);

            _healthBar?.UpdateHealthUI(); // Update UI
        }

        public void SwitchForm(PlayerForm newForm)
        {
            _currentForm = newForm;
        }
    }
}