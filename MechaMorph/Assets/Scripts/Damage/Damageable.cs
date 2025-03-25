using UnityEngine;
using System;
using TrippleTrinity.MechaMorph.Ui;
namespace TrippleTrinity.MechaMorph.Damage
{
    public class Damageable : MonoBehaviour
    {
        [SerializeField] private float maxHealth = 100f;
        private float _currentHealth;

        public event Action OnDamageTaken;
        public event Action OnHealed;
        public event Action OnDeath;

        public float CurrentHealth => _currentHealth;
        public float MaxHealth => maxHealth;

        protected virtual void Start()
        {
            _currentHealth = maxHealth;
        }

        public virtual void TakeDamage(float amount)
        {
            _currentHealth -= amount;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth);
            OnDamageTaken?.Invoke();

            if (_currentHealth <= 0)
            {
                HandleDeath();
            }
        }

        protected virtual void HandleDeath()
        {
            //save game progress
            ScoreManager.Instance.SaveScore();
            OnDeath?.Invoke();
            Destroy(gameObject);
        }

        public virtual void Heal(float amount)
        {
            if (amount <= 0) return;

            _currentHealth += amount;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth);
            OnHealed?.Invoke();
        }
    }
}