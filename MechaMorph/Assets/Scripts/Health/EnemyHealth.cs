using UnityEngine;
using TrippleTrinity.MechaMorph.Ability;
using TrippleTrinity.MechaMorph.Token;
using TrippleTrinity.MechaMorph.Ui;
using TrippleTrinity.MechaMorph.Damage;

namespace TrippleTrinity.MechaMorph.Health
{
    public class EnemyHealth : Damageable
    {
        [SerializeField] private int scoreValue = 10; // Points for enemy kill
        [SerializeField] private bool shouldDropToken; // Some enemies drop tokens

        private TokenSpawner _tokenSpawner;
        private AreaDamageAbility _areaDamageAbility;

        protected override void Start()
        {
            base.Start();
            _tokenSpawner = GetComponent<TokenSpawner>(); // TokenSpawner attached to enemy
            _areaDamageAbility = FindObjectOfType<AreaDamageAbility>(); // AreaDamageAbility in the scene
        }

        protected override void HandleDeath()
        {
            Vector3 deathPosition = transform.position; // Store position before destroying

            if (shouldDropToken && _tokenSpawner != null)
            {
                Debug.Log($"Enemy {gameObject.name} died, spawning token at {deathPosition}");
                _tokenSpawner.SpawnToken(deathPosition);
            }

            _areaDamageAbility?.RegisterEnemyKill(); // Notify ability about enemy kill
            ScoreManager.Instance?.AddScore(scoreValue); // Add score to the ScoreManager

            base.HandleDeath(); // Call base class to destroy the object
        }
    }
}