using UnityEngine;
using System;
using TrippleTrinity.MechaMorph.Ui;
using TrippleTrinity.MechaMorph.Ability;
using TrippleTrinity.MechaMorph.Token;

namespace TrippleTrinity.MechaMorph.Damage
{
    public class Damageable : MonoBehaviour
    {
        public enum PlayerForm { Ball, Robot }

        [SerializeField] private float maxHealth = 100f;
        [SerializeField] private int scoreValue = 10; // Points for enemy kill
        [SerializeField] private bool shouldDropToken; //  Only specific enemies drop tokens

        private float _currentHealth;
        private PlayerForm _currentForm = PlayerForm.Ball;
        private AreaDamageAbility _areaDamageAbility;
        private TokenSpawner _tokenSpawner;

        public event Action OnDamageTaken;
        public event Action OnHealed;
        public event Action OnDeath;

        public float CurrentHealth => _currentHealth;
        public float MaxHealth => maxHealth;

        private void Start()
        {
            _currentHealth = maxHealth;
            _areaDamageAbility = FindObjectOfType<AreaDamageAbility>();
            _tokenSpawner = GetComponent<TokenSpawner>(); //  Uses TokenSpawner attached to enemy
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

            Vector3 deathPosition = transform.position; //  Store death position before destroying enemy

            if (shouldDropToken && _tokenSpawner != null)
            {
                Debug.Log($"Enemy {gameObject.name} died, spawning token at {deathPosition}");
                _tokenSpawner.SpawnToken(deathPosition);
            }

            _areaDamageAbility?.RegisterEnemyKill(); 

            ScoreManager.Instance?.AddScore(scoreValue);

            Destroy(gameObject);
        }

        public void Heal(float amount)
        {
            if (amount <= 0) return;

            _currentHealth += amount;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth);

            Debug.Log($"Player Healed! Current Health: {_currentHealth}/{maxHealth}");

            OnHealed?.Invoke();
        }


        public void SwitchForm(PlayerForm newForm)
        {
            _currentForm = newForm;
        }
    }
}
